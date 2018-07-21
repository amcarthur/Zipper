using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GitUIPluginInterfaces;
using Ionic;
using Ionic.Zip;
using ResourceManager;
using Settings = GitCommands.AppSettings;

namespace Zipper
{
    public partial class ZipperForm : GitExtensionsFormBase
    {
        private class ZipperBackgroundWorkerArg
        {
            public string OutputPath { get; set; }
            public string SourcePath { get; set; }
            public Ionic.Zlib.CompressionLevel CompressionLevel { get; set; }
            public Ionic.Zip.CompressionMethod CompressionMethod { get; set; }
        }

        private enum ZipperBackgroundWorkerProgressEntryAction
        {
            None,
            Add,
            Reset
        }

        private enum ZipperBackgroundWorkerProgressType
        {
            Add,
            Save
        }

        private class ZipperBackgroundWorkerProgressState
        {
            public ZipperBackgroundWorkerProgressType Type { get; set; }
            public string Text { get; set; }
            public ZipperBackgroundWorkerProgressEntryAction AddEntryAction { get; set; }
            public string Error { get; set; }
        }

        private readonly ZipperPlugin _plugin;
        private readonly ISettingsSource _settings;
        private readonly IGitModule _gitCommands;
        private readonly string _originalBranch;
        private int _entriesAdded;

        #region Translation
        private readonly TranslationString _pluginDescription = new TranslationString("Zipper");
        #endregion

        /// <summary>
        /// Default constructor added to register all strings to be translated
        /// Use the other constructor:
        /// ZipperForm(IGitPluginSettingsContainer settings, GitUIBaseEventArgs gitUiCommands)
        /// </summary>
        public ZipperForm()
        {
            InitializeComponent();
        }

        public ZipperForm(ZipperPlugin plugin, ISettingsSource settings, GitUIEventArgs gitUiCommands)
        {
            InitializeComponent();
            Translate();

            Text = _pluginDescription.Text;
            _plugin = plugin;
            _settings = settings;
            _gitCommands = gitUiCommands.GitModule;
            _originalBranch = _gitCommands.GetSelectedBranch();
            _entriesAdded = 0;
        }

        private void PopulateBranches()
        {
            Branch_ComboBox.Items.Clear();

            var branches = _gitCommands.GetRefs(false);
            string[] branchNames;
            branchNames = branches.Select(b => b.Name).Where(name => name.IsNotNullOrWhitespace()).ToArray();

            Branch_ComboBox.Items.AddRange(branches.Select(b => b.Name).Where(name => name.IsNotNullOrWhitespace()).ToArray());

            for (int i = 0; i < branches.Count; ++i)
            {
                if (branches[i].Name == _gitCommands.GetSelectedBranch())
                {
                    Branch_ComboBox.SelectedIndex = i;
                }
            }
        }

        private void OutputPath_Button_Click(object sender, EventArgs e)
        {
            string selectedName = new DirectoryInfo(_gitCommands.WorkingDir).Name;
            var dialog = new SaveFileDialog
            {
                Title = "Select the location to save the Zip file...",
                InitialDirectory = _gitCommands.WorkingDir,
                FileName = selectedName,
                Filter = "Zip File (*.zip)|*.zip"
            };
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                OutputPath_TextBox.Text = dialog.FileName;
            }
        }

        private void Zip_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OutputPath_TextBox.Text))
            {
                MessageBox.Show("You must enter an output path location for the Zip file.", "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(OutputPath_TextBox.Text))
            {
                MessageBox.Show("A file already exists at the specified output path location.", "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedBranch = Branch_ComboBox.SelectedItem.ToString();
            if (_originalBranch != selectedBranch)
            {
                var result = _gitCommands.RunGitCmdResult("checkout " + selectedBranch);
                if (!result.ExitedSuccessfully)
                {
                    MessageBox.Show("An error occurred checking out the selected branch: " + result.StdError, "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Zip_Button.Enabled = false;

            CreateZip_BackgroundWorker.RunWorkerAsync(new ZipperBackgroundWorkerArg
            {
                OutputPath = OutputPath_TextBox.Text,
                SourcePath = _gitCommands.WorkingDir,
                CompressionLevel = _plugin.GetCompressionLevelSetting(),
                CompressionMethod = _plugin.GetCompressionMethodSetting()
            });
        }

        private void CreateZip_AddProgress(object sender, AddProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Adding_Started)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Add,
                    Text = "Adding files to Zip...",
                    AddEntryAction = ZipperBackgroundWorkerProgressEntryAction.Reset
                };

                CreateZip_BackgroundWorker.ReportProgress(0, progress);
            }
            else if (e.EventType == ZipProgressEventType.Adding_AfterAddEntry)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Add,
                    AddEntryAction = ZipperBackgroundWorkerProgressEntryAction.Add
                };

                CreateZip_BackgroundWorker.ReportProgress(e.EntriesTotal, progress);
            }
            else if (e.EventType == ZipProgressEventType.Adding_Completed)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Add
                };

                CreateZip_BackgroundWorker.ReportProgress(100, progress);
            }
        }

        private void CreateZip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Save,
                    Text = "Saving Zip...."
                };

                CreateZip_BackgroundWorker.ReportProgress(0, progress);
            }
            else if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Save
                };

                CreateZip_BackgroundWorker.ReportProgress(e.EntriesSaved * 100 / e.EntriesTotal, progress);
            }
            else if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Save,
                    Text = "Finished"
                };

                CreateZip_BackgroundWorker.ReportProgress(100, progress);
            }
            else if (e.EventType == ZipProgressEventType.Error_Saving)
            {
                var progress = new ZipperBackgroundWorkerProgressState
                {
                    Type = ZipperBackgroundWorkerProgressType.Save,
                    Text = "Failed",
                    Error = "An error occurred saving the Zip file."
                };

                CreateZip_BackgroundWorker.ReportProgress(0, progress);
            }
        }

        private void OutputPath_TextBox_TextChanged(object sender, EventArgs e)
        {
            Zip_Button.Enabled = !string.IsNullOrWhiteSpace(OutputPath_TextBox.Text);
        }

        private void CreateZip_BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var zipperArg = (ZipperBackgroundWorkerArg)e.Argument;
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionMethod = zipperArg.CompressionMethod;
                zip.CompressionLevel = zipperArg.CompressionLevel;
                zip.AddProgress += CreateZip_AddProgress;
                zip.SaveProgress += CreateZip_SaveProgress;
                zip.Comment = string.Format("This Zip archive was created by the GitExtensions plugin Zipper.");
                zip.AddDirectory(zipperArg.SourcePath);
                zip.Save(zipperArg.OutputPath);
            }
        }

        private void CreateZip_BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (CreateZip_BackgroundWorker.CancellationPending)
            {
                return;
            }

            var state = (ZipperBackgroundWorkerProgressState)e.UserState;

            if (!string.IsNullOrEmpty(state.Text))
            {
                ZipStatus_Label.Text = state.Text;
            }

            if (state.Type == ZipperBackgroundWorkerProgressType.Add)
            {
                if (state.AddEntryAction == ZipperBackgroundWorkerProgressEntryAction.Reset)
                {
                    _entriesAdded = 0;
                    Zip_ProgressBar.Value = 0;
                }
                else if (state.AddEntryAction == ZipperBackgroundWorkerProgressEntryAction.Add)
                {
                    _entriesAdded++;
                    Zip_ProgressBar.Value = _entriesAdded * 100 / e.ProgressPercentage;
                }
            }
            else
            {
                Zip_ProgressBar.Value = e.ProgressPercentage;
            }

            if (!string.IsNullOrEmpty(state.Error))
            {
                MessageBox.Show(state.Error, "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CreateZip_BackgroundWorker.CancelAsync();
            }
        }

        private void CreateZip_BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (_originalBranch != _gitCommands.GetSelectedBranch())
            {
                var result = _gitCommands.RunGitCmdResult("checkout " + _originalBranch);
                if (!result.ExitedSuccessfully)
                {
                    MessageBox.Show("An error occurred checking out the original branch: " + result.StdError, "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (e.Error != null)
            {
                MessageBox.Show(string.Format("An error occurred saving the Zip file: {0}", e.Error.Message), "Zipper Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Zip_ProgressBar.Value = 0;
                ZipStatus_Label.Text = "Ready";
                Zip_Button.Enabled = true;
                return;
            }

            if (!e.Cancelled)
            {
                MessageBox.Show("Finished", "Zipper", MessageBoxButtons.OK);
            }

            Zip_ProgressBar.Value = 0;
            ZipStatus_Label.Text = "Ready";
            Zip_Button.Enabled = true;
        }

        private void ZipperForm_Load(object sender, EventArgs e)
        {
            PopulateBranches();
        }
    }
}
