using System;
using System.Collections.Generic;
using Greenshot.IniFile;
using Greenshot.Plugin;

namespace GreenshotDemoPlugin
{
    public class DemoPlugin: IGreenshotPlugin
    {
        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(DemoPlugin));
        private IGreenshotHost host;
        private DemoConnector connector = null;
        private static DemoPlugin instance = null;
        private PluginAttribute pluginAttribute;
        private DemoConfiguration config = null;



        public bool Initialize(IGreenshotHost host, PluginAttribute pluginAttribute)
        {
            this.host = host;
            this.pluginAttribute = pluginAttribute;
            config = IniConfig.GetIniSection<DemoConfiguration>();
            return true;
        }

        public void Shutdown()
        {
            LOG.Debug("Demo Plugin shutdown.");
            if (connector != null)
            {
                connector.Dispose();
            }
        }

        public void Configure()
        {
        }

        public IEnumerable<IDestination> Destinations()
        {
            yield return new DemoDestination(this);
        }

        public IEnumerable<IProcessor> Processors()
        {
            yield break;
        }

        public void Dispose()
        {
            if (connector != null)
            {
                connector.Dispose();
                connector = null;
            }
        }

        public DemoConnector DemoConnector
        {
            get
            {
                if (connector == null)
                {
                    connector = new DemoConnector();
                }
                return connector;
            }
        }

    }
}
