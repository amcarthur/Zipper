using System.Collections.Generic;
using System.ComponentModel.Composition;
using GitUIPluginInterfaces;
using ResourceManager;

namespace Zipper
{
    [Export(typeof(IGitPlugin))]
    public class ZipperPlugin : GitPluginBase
    {
        public readonly ChoiceSetting CompressionMethod = new ChoiceSetting("Compression Method", new List<string>
        {
            "Deflate",
            "BZip2",
            "None"
        }, "Deflate");

        public readonly ChoiceSetting CompressionLevel = new ChoiceSetting("Compression Level", new List<string>
        {
            "Default",
            "Best Compression",
            "Best Speed",
            "None",
            "Level 0",
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5",
            "Level 6",
            "Level 7",
            "Level 8",
            "Level 9",
        }, "Default");

        public ZipperPlugin()
        {
            SetNameAndDescription("Zipper");
            Translate();
        }

        public override System.Collections.Generic.IEnumerable<ISetting> GetSettings()
        {
            yield return CompressionMethod;
            yield return CompressionLevel;
        }

        public override bool Execute(GitUIEventArgs args)
        {
            using (var form = new ZipperForm(this, Settings, args))
            {
                form.ShowDialog(args.OwnerForm);
            }

            return false;
        }

        public Ionic.Zip.CompressionMethod GetCompressionMethodSetting()
        {
            var compressionMethodString = CompressionMethod.ValueOrDefault(Settings);

            if (string.IsNullOrEmpty(compressionMethodString))
            {
                return Ionic.Zip.CompressionMethod.Deflate;
            }
            else if (compressionMethodString == "Deflate")
            {
                return Ionic.Zip.CompressionMethod.Deflate;
            }
            else if (compressionMethodString == "BZip2")
            {
                return Ionic.Zip.CompressionMethod.BZip2;
            }
            else if (compressionMethodString == "None")
            {
                return Ionic.Zip.CompressionMethod.None;
            }

            return Ionic.Zip.CompressionMethod.Deflate;
        }

        public Ionic.Zlib.CompressionLevel GetCompressionLevelSetting()
        {
            var compressionLevelString = CompressionLevel.ValueOrDefault(Settings);

            if (string.IsNullOrEmpty(compressionLevelString))
            {
                return Ionic.Zlib.CompressionLevel.Default;
            }
            else if (compressionLevelString == "Default")
            {
                return Ionic.Zlib.CompressionLevel.Default;
            }
            else if (compressionLevelString == "Best Compression")
            {
                return Ionic.Zlib.CompressionLevel.BestCompression;
            }
            else if (compressionLevelString == "Best Speed")
            {
                return Ionic.Zlib.CompressionLevel.BestSpeed;
            }
            else if (compressionLevelString == "None")
            {
                return Ionic.Zlib.CompressionLevel.None;
            }
            else if (compressionLevelString == "Level 0")
            {
                return Ionic.Zlib.CompressionLevel.Level0;
            }
            else if (compressionLevelString == "Level 1")
            {
                return Ionic.Zlib.CompressionLevel.Level1;
            }
            else if (compressionLevelString == "Level 2")
            {
                return Ionic.Zlib.CompressionLevel.Level2;
            }
            else if (compressionLevelString == "Level 3")
            {
                return Ionic.Zlib.CompressionLevel.Level3;
            }
            else if (compressionLevelString == "Level 4")
            {
                return Ionic.Zlib.CompressionLevel.Level4;
            }
            else if (compressionLevelString == "Level 5")
            {
                return Ionic.Zlib.CompressionLevel.Level5;
            }
            else if (compressionLevelString == "Level 6")
            {
                return Ionic.Zlib.CompressionLevel.Level6;
            }
            else if (compressionLevelString == "Level 7")
            {
                return Ionic.Zlib.CompressionLevel.Level7;
            }
            else if (compressionLevelString == "Level 8")
            {
                return Ionic.Zlib.CompressionLevel.Level8;
            }
            else if (compressionLevelString == "Level 9")
            {
                return Ionic.Zlib.CompressionLevel.Level9;
            }

            return Ionic.Zlib.CompressionLevel.Default;
        }
    }
}
