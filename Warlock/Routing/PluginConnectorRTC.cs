using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warlock;
using Warlock.UI;

namespace Warlock
{
    /// <summary>
    /// This lies on the RTC(Server) side
    /// </summary>
    class PluginConnectorRTC : IRoutable
    {
        public PluginConnectorRTC()
        {
            LocalNetCoreRouter.registerEndpoint(this, Routing.Endpoints.RTC_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;
            switch (message.Type)
            {
                case Routing.Commands.UPDATE_SPEC:
                    PluginSpec.UpdateSpec(message.objectValue as Dictionary<string, object>);
                    break;
                case Routing.Commands.SAVE_INTERNAL_SAVESTATE:
                    SavestateSystem.SaveInternalSaveState((string)message.objectValue);

                    break;
                case Routing.Commands.LOAD_INTERNAL_SAVESTATE:
                    var data = message.objectValue as object[];
                    var key = data[0] as string;
                    bool resetScripts = (bool)data[1];
                    SavestateSystem.LoadInternalSaveState(key);// key,resetScripts);
                    break;
                case Routing.Commands.LOAD_STASHKEY:
                    LoadAndRunStashKey(((string)message.objectValue ?? ""));
                    break;
                case Routing.Commands.LOAD_SCRIPT:
                    WarlockCore.Runner.LoadScript(message.objectValue.ToString());
                    //WarlockCore.Runner.LoadScript((string)message.objectValue);
                    break;
                case Routing.Commands.LOAD_STASHKEY_SAVESTATE:
                    LoadAndRunStashKeySavestate((string)message.objectValue);
                    break;
                case Routing.Commands.RUN:
                    WarlockCore.RunThisSide();
                    break;
                case Routing.Commands.STOP:
                    WarlockCore.StopScripts();
                    break;
                case Routing.Commands.RESET:
                    WarlockCore.ResetThisSide();
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }

        void LoadAndRunStashKey(string stashkeyName)
        {
            var sk = WarlockCore.ScriptedStockpile.GetStashKey(stashkeyName);
            if (sk == null)
            {
                throw new Exception("Stashkey does not exist!!");
            }
            else
            {
                sk.Run();
            }
        }

        void LoadAndRunStashKeySavestate(string stashkeyName)
        {
            var sk = WarlockCore.ScriptedStockpile.GetStashKey(stashkeyName);
            if (sk == null)
            {
                throw new Exception("Stashkey does not exist!!");
            }
            else
            {
                sk.StashKeyRef.Run();
            }
        }

    }
}
