using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Warlock.UI;

namespace Warlock
{
    [Export(typeof(IPlugin))]
    public class PluginCore : IPlugin, IDisposable
    {
        public string Name => "Your Plugin Name";
        public string Description => "Description";

        public string Author => "Your Name";

        public Version Version => new Version(1, 0, 0);

        /// <summary>
        /// Defines which sides will call Start
        /// </summary>
        public RTCSide SupportedSide => RTCSide.Both;
        internal static RTCSide CurrentSide = RTCSide.Both;

        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;
        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            Lua.LuaManager.EnsureInitialized();
            
            if (side == RTCSide.Client)
            {
                connectorEMU = new PluginConnectorEMU();
            }
            else if (side == RTCSide.Server)
            {
                connectorRTC = new PluginConnectorRTC();
                S.GET<OpenToolsForm>().RegisterTool("Warlock", "Open Warlock Player", () =>
                {
                    var form = new WarlockEditor();
                    S.SET<WarlockEditor>(form);
                    form.Show();
                    form.Activate();
                });

                S.GET<OpenToolsForm>().RegisterTool("Warlock Editor", "Open Warlock Editor", () =>
                {
                    var form = new WarlockEditor();
                    S.SET<WarlockEditor>(form);
                    form.Show();
                    form.Activate();
                });
            }
            CurrentSide = side;

            WarlockCore.Initialize();
            return true;
        }

        public bool StopPlugin()
        {
            if (CurrentSide == RTCSide.Server && !S.ISNULL<WarlockEditor>() && !S.GET<WarlockEditor>().IsDisposed)
            {
                S.GET<WarlockEditor>().Close();
            }
            return true;
        }
    }
}
