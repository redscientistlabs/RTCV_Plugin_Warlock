using Ceras;
using Newtonsoft.Json;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (StashKeyRef?.Run() ?? false)
            {
                LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_SCRIPT, ScriptRTC ?? "", true);
                LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.LOAD_SCRIPT, ScriptEMU ?? "", true);
            }
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
