using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LunarBind;
using RTCV.NetCore;
using RTCV.Common;

namespace Warlock
{
    internal static class SavestateSystem
    {

        //public static Dictionary<string, StashKey> StashKeys = new Dictionary<string, StashKey>();
        public static Dictionary<string, StashKey> InternalSaveStates = new Dictionary<string, StashKey>();
        
        //[LunarBindFunction("Warlock.LoadState")]
        //public static void LoadSaveState(string key, bool stopScripts = false)
        //{
        //    LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_INTERNAL_SAVESTATE, new object[] { key, stopScripts }, true);
        //}

        [LunarBindFunction("Warlock.SaveState")]
        public static void SaveSaveState(string key)
        {
            LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.SAVE_INTERNAL_SAVESTATE, key, true);
        }

        /// <summary>
        /// DO NOT CALL, use <see cref="LoadSaveState(string)" instead/>
        /// </summary>
        /// <param name="key"></param>
        public static void LoadInternalSaveState(string key)
        {
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client) 
            {
                throw new Exception("Cannot call LoadInternal from Emulator side!"); 
            }
            else
            {               
                if (InternalSaveStates.TryGetValue(key, out StashKey val))
                {
                    val.Run();
                }
            }
        }

        /// <summary>
        /// DO NOT CALL, use <see cref="LoadSaveState(string)" instead/>
        /// </summary>
        /// <param name="key"></param>
        public static void SaveInternalSaveState(string key)
        {
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                throw new Exception("Cannot call LoadInternal from Emulator side!");
            }
            else
            {
                InternalSaveStates[key] = SaveState();
            }
        }

        public static StashKey SaveState(StashKey sk = null)
        {
            bool UseSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES];

            if (UseSavestates)
            {
                return LocalNetCoreRouter.QueryRoute<StashKey>(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.SaveState, sk, true);
            }
            else
            {
                return LocalNetCoreRouter.QueryRoute<StashKey>(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.SaveStateless, sk, true);
            }
        }



        //[LunarBindFunction("Reset")]
        public static void Reset()
        {
            InternalSaveStates.Clear();
        }
    }
}
