namespace Zipper
{
    partial class ZipperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Branch_Label = new System.Windows.Forms.Label();
            this.Branch_ComboBox = new System.Windows.Forms.ComboBox();
            this.OutputPath_Label = new System.Windows.Forms.Label();
            this.OutputPath_TextBox = new System.Windows.Forms.TextBox();
            this.OutputPath_Button = new System.Windows.Forms.Button();
            this.Zip_Button = new System.Windows.Forms.Button();
            this.Zip_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ZipStatus_Label = new System.Windows.Forms.Label();
            this.CreateZip_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.Branch_Label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Branch_ComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.OutputPath_Label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.OutputPath_TextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.OutputPath_Button, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Zip_Button, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.Zip_ProgressBar, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ZipStatus_Label, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.42857F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 106);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Branch_Label
            // 
            this.Branch_Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Branch_Label.AutoSize = true;
            this.Branch_Label.Location = new System.Drawing.Point(31, 11);
            this.Branch_Label.Name = "Branch_Label";
            this.Branch_Label.Size = new System.Drawing.Size(44, 13);
            this.Branch_Label.TabIndex = 0;
            this.Branch_Label.Text = "Branch:";
            // 
            // Branch_ComboBox
            // 
            this.Branch_ComboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Branch_ComboBox.DisplayMember = "Text";
            this.Branch_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Branch_ComboBox.FormattingEnabled = true;
            this.Branch_ComboBox.Location = new System.Drawing.Point(110, 7);
            this.Branch_ComboBox.Name = "Branch_ComboBox";
            this.Branch_ComboBox.Size = new System.Drawing.Size(179, 21);
            this.Branch_ComboBox.TabIndex = 3;
            this.Branch_ComboBox.ValueMember = "Value";
            // 
            // OutputPath_Label
            // 
            this.OutputPath_Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OutputPath_Label.AutoSize = true;
            this.OutputPath_Label.Location = new System.Drawing.Point(18, 47);
            this.OutputPath_Label.Name = "OutputPath_Label";
            this.OutputPath_Label.Size = new System.Drawing.Size(70, 13);
            this.OutputPath_Label.TabIndex = 2;
            this.OutputPath_Label.Text = "Output Path:";
            // 
            // OutputPath_TextBox
            // 
            this.OutputPath_TextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OutputPath_TextBox.Location = new System.Drawing.Point(110, 43);
            this.OutputPath_TextBox.Name = "OutputPath_TextBox";
            this.OutputPath_TextBox.Size = new System.Drawing.Size(179, 21);
            this.OutputPath_TextBox.TabIndex = 5;
            this.OutputPath_TextBox.TextChanged += new System.EventHandler(this.OutputPath_TextBox_TextChanged);
            // 
            // OutputPath_Button
            // 
            this.OutputPath_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OutputPath_Button.Location = new System.Drawing.Point(295, 42);
            this.OutputPath_Button.Name = "OutputPath_Button";
            this.OutputPath_Button.Size = new System.Drawing.Size(118, 23);
            this.OutputPath_Button.TabIndex = 7;
            this.OutputPath_Button.Text = "Browse";
            this.OutputPath_Button.UseVisualStyleBackColor = true;
            this.OutputPath_Button.Click += new System.EventHandler(this.OutputPath_Button_Click);
            // 
            // Zip_Button
            // 
            this.Zip_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Zip_Button.Enabled = false;
            this.Zip_Button.Location = new System.Drawing.Point(295, 77);
            this.Zip_Button.Name = "Zip_Button";
            this.Zip_Button.Size = new System.Drawing.Size(118, 23);
            this.Zip_Button.TabIndex = 8;
            this.Zip_Button.Text = "Zip";
            this.Zip_Button.UseVisualStyleBackColor = true;
            this.Zip_Button.Click += new System.EventHandler(this.Zip_Button_Click);
            // 
            // Zip_ProgressBar
            // 
            this.Zip_ProgressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Zip_ProgressBar.Location = new System.Drawing.Point(110, 77);
            this.Zip_ProgressBar.Name = "Zip_ProgressBar";
            this.Zip_ProgressBar.Size = new System.Drawing.Size(179, 23);
            this.Zip_ProgressBar.TabIndex = 9;
            // 
            // ZipStatus_Label
            // 
            this.ZipStatus_Label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ZipStatus_Label.AutoSize = true;
            this.ZipStatus_Label.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ZipStatus_Label.Location = new System.Drawing.Point(34, 82);
            this.ZipStatus_Label.Name = "ZipStatus_Label";
            this.ZipStatus_Label.Size = new System.Drawing.Size(38, 13);
            this.ZipStatus_Label.TabIndex = 10;
            this.ZipStatus_Label.Text = "Ready";
            // 
            // CreateZip_BackgroundWorker
            // 
            this.CreateZip_BackgroundWorker.WorkerReportsProgress = true;
            this.CreateZip_BackgroundWorker.WorkerSupportsCancellation = true;
            this.CreateZip_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CreateZip_BackgroundWorker_DoWork);
            this.CreateZip_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CreateZip_BackgroundWorker_ProgressChanged);
            this.CreateZip_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CreateZip_BackgroundWorker_RunWorkerCompleted);
            // 
            // ZipperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(416, 106);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(357, 144);
            this.Name = "ZipperForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zipper";
            this.Load += new System.EventHandler(this.ZipperForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button OutputPath_Button;
        private System.Windows.Forms.Label Branch_Label;
        private System.Windows.Forms.Label OutputPath_Label;
        private System.Windows.Forms.ComboBox Branch_ComboBox;
        private System.Windows.Forms.TextBox OutputPath_TextBox;
        private System.Windows.Forms.Button Zip_Button;
        private System.Windows.Forms.ProgressBar Zip_ProgressBar;
        private System.Windows.Forms.Label ZipStatus_Label;
        private System.ComponentModel.BackgroundWorker CreateZip_BackgroundWorker;
    }
}