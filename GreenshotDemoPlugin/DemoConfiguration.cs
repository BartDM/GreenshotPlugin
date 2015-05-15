using Greenshot.IniFile;
using GreenshotPlugin.Core;

namespace GreenshotDemoPlugin
{
    [IniSection("Demo", Description = "Greenshot Demo Plugin configuration")]
    public class DemoConfiguration:IniSection
    {
        private const string DEFAULT_URL = "https://THE_URL_TO_YOUR_APPLICATION";

        [IniProperty("Url", Description = "Url to your application", DefaultValue = DEFAULT_URL)]
        public string Url;

        [IniProperty("Timeout", Description = "Session timeout in minutes", DefaultValue = "30")]
        public int TimeOut;

        [IniProperty("UploadFormat", Description = "What file type to use for uploading", DefaultValue = "png")]
        public OutputFormat UploadFormat;

        [IniProperty("UploadJpegQuality", Description = "JPEG file save quality in %.", DefaultValue = "80")]
        public int UploadJpegQuality;

        [IniProperty("UploadReduceColors", Description = "Reduce color amount of the uploaded image to 256", DefaultValue = "False")]
        public bool UploadReduceColors;
    }
}
