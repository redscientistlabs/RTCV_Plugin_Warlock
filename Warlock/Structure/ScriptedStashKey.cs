using Ceras;
using System.Text.Json;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Warlock.Structure
{
    [Serializable]
    public class ScriptedStashKey
    {
        public string ScriptEMU;
        public string ScriptRTC;
        public string StashKeyAlias;

        [JsonIgnore]
        internal StashKey StashKeyRef { get; set; }

        public ScriptedStashKey()
        {
            StashKeyRef = null;
        }
        
        public void Run()
        {
            if(PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                throw new Exception("Cannot call ScriptedStashKey.Run() from emulator side!");
            }

            StockpileManagerUISide.ApplyStashkey(StashKeyRef, true, true);
            //StashKeyRef.Run();
            WarlockCore.Runner.LoadScript(ScriptRTC ?? "");
            LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.LOAD_SCRIPT, ScriptEMU ?? "", true);
        }

        public void Update()
        {
            StashKeyAlias = StashKeyRef.Alias;
        }

        public override string ToString()
        {
            return StashKeyRef?.Alias ?? "REFERENCE ERROR";
        }

    }
}
