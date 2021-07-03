using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LunarBind;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;

namespace Warlock
{

    //TODO: Need to send across the list of blast layers to the emulator side, for now using stashkey workaround
    class BlastLayerSystem
    {
        static StashKey sk = null;


        [LunarBindDocumentation("Applies a blast layer by name")]
        [LunarBindFunction("Warlock.ApplyBlastLayer")]
        public static void ApplyBlastLayer(string name)
        {
            LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.APPLY_BLAST_LAYER, name, true);
        }

        //Not yet supported
        [LunarBindHide]
        [LunarBindFunction("Warlock.UndoBlastLayer")]
        public static void UndoBlastLayer(string name)
        {
            LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.UNDO_BLAST_LAYER, name, true);
        }

        internal static void ApplyBlastLayerInternal(string name)
        {
            if(sk == null)
            {
                sk = (StashKey)WarlockCore.ScriptedStockpile.ScriptedStashKeys[0].StashKeyRef.Clone();
                
            }
            var bl = WarlockCore.ScriptedStockpile.BlastLayers[name];
            sk.BlastLayer = bl;
            var applied = StockpileManagerUISide.ApplyStashkey(sk, false, false);
        }

        //TODO: implement
        internal static void UndoBlastLayerInternal(string name)
        {
            throw new NotImplementedException();
            //WarlockCore.ScriptedStockpile.BlastLayers[name].GetBackup().Apply(true);
        }


        public static void Reset()
        {
            sk = null;
        }
    }
}
