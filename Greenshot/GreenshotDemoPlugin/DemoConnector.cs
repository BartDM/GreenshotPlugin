using System;
using System.Collections.Specialized;
using System.Net;
using Greenshot.IniFile;
using GreenshotPlugin.Core;

namespace GreenshotDemoPlugin
{
    public class DemoConnector: IDisposable
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(DemoConnector));
        private static DemoConfiguration config = IniConfig.GetIniSection<DemoConfiguration>();
        private string url;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public DemoConnector()
        {
            this.url = config.Url;
        }

        public void AddAttachment(string fileName, IBinaryContainer attachment)
        {
            var file = attachment.ToBase64String(Base64FormattingOptions.InsertLineBreaks);
            LOG.DebugFormat("adding image {0}", file);

            using (var client = new WebClient())
            {
                client.UploadValues(url,
                    new NameValueCollection()
                    {
                        {"imageBase64String",file},
                        {"fileName",fileName},
                        {"ticketNr","Test ticket"}
                    });
            }
        }
    }
}
