using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Warlock.UI;

namespace Warlock
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, Routing.Endpoints.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                case Routing.Commands.UPDATE_SPEC:
                    PluginSpec.UpdateSpec(message.objectValue as Dictionary<string, object>);
                    break;
                case Routing.Commands.LOAD_GLOBAL_SCRIPTS:
                    var scripts = message.objectValue as Dictionary<string, string>;
                    if(scripts != null)
                    {
                        foreach (var script in scripts)
                        {
                            WarlockCore.Runner.LoadGlobalScript(script.Key, script.Value);
                        }
                    }
                    break;
                case Routing.Commands.EXECUTE_HOOK_NO_INTERRUPT:
                    WarlockCore.ExecuteNoInterrupt(message.objectValue as string);
                    break;
                case Routing.Commands.EXECUTE_HOOK:
                    WarlockCore.Execute(message.objectValue as string);
                    break;
                case Routing.Commands.LOAD_SCRIPT:
                    WarlockCore.Runner.LoadScript(message.objectValue.ToString());
                    break;
                case Routing.Commands.RESET:
                    WarlockCore.ResetThisSide();
                    break;
                case Routing.Commands.RUN:
                    WarlockCore.RunThisSide();
                    break;
                case Routing.Commands.STOP:
                    WarlockCore.StopScripts();
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
