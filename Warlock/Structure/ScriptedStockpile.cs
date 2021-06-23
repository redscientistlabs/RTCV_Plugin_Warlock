using Ceras;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace Warlock.Structure
{
    [Serializable]
    public class ScriptedStockpile
    {
        public Dictionary<string,string> GlobalScriptsEMU = new Dictionary<string, string>();
        public Dictionary<string,string> GlobalScriptsRTC = new Dictionary<string, string>();
        public List<ScriptedStashKey> ScriptedStashKeys = new List<ScriptedStashKey>();
        //public string StockpilePath = "";
        public string InitialStashkey = "";
        public ScriptedStockpile() { }

        public ScriptedStashKey GetStashKey(string key)
        {
            return ScriptedStashKeys.FirstOrDefault(x => x.StashKeyAlias == key);
        }

        //Will be called on the UI side only
        public void Run()
        {
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                throw new Exception("Cannot start a scripted stockpile from the Emulator side");
            }

            //WarlockCore.Reset();

            //Load global scripts on RTC
            foreach (var script in GlobalScriptsRTC)
            {
                WarlockCore.Runner.LoadGlobalScript(script.Key, script.Value);
            }
            //Load global scripts on EMU
            LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.LOAD_GLOBAL_SCRIPTS, GlobalScriptsEMU, true);

            //GetStashKey(InitialStashkey).Run();
            //Load initial ScriptedStashKey'
            var sk = GetStashKey(InitialStashkey);
            if(sk == null)
            {
                SyncObjectSingleton.FormExecute(() =>
                {
                    MessageBox.Show("Author did not set initial stash key");
                });
            }
            else
            {
                sk.Run();
            }
        }
    }


    //class ScriptedStockpileConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return (objectType == typeof(ScriptedStockpile));
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
            
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
