using System;
using System.IO;
using System.Windows.Forms;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;

namespace GreenshotDemoPlugin
{
    public class DemoDestination: AbstractDestination
    {
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(DemoDestination));
        private DemoPlugin demoPlugin = null;
        private static DemoConfiguration config = IniConfig.GetIniSection<DemoConfiguration>();

        public DemoDestination(DemoPlugin demoPlugin)
        {
            this.demoPlugin = demoPlugin;
        }

        public override string Designation
        {
            get { return "Demo"; }
        }

        public override string Description
        {
            get { return "Upload to Demo system"; }
        }

        public override ExportInformation ExportCapture(bool manuallyInitiated, ISurface surface, ICaptureDetails captureDetails)
        {
            var exportInformation = new ExportInformation(this.Designation, this.Description);
            string filename = Path.GetFileName(FilenameHelper.GetFilename(config.UploadFormat, captureDetails));
            var outputSettings = new SurfaceOutputSettings(config.UploadFormat, config.UploadJpegQuality, config.UploadReduceColors);

            try
            {
                // Run upload in the background
                new PleaseWaitForm().ShowAndWait(Description, "Uploading",
                    delegate()
                    {
                        demoPlugin.DemoConnector.AddAttachment(filename, new SurfaceContainer(surface, outputSettings, filename));
                    }
                );
                LOG.Debug("Uploaded to Jira.");
                exportInformation.ExportMade = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Upload failure");
            }
            return exportInformation;
        }
    }
}
