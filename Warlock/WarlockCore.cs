using LunarBind;
using LunarBind.Standards;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warlock.Structure;

namespace Warlock
{
    //Can be run on both sides
    internal static class WarlockCore
    {
        internal static HookedStateScriptRunner Runner { get; private set; } = null;
        internal static LuaScriptStandard Standard { get; private set; } = null;
        internal static ScriptBindings Bindings { get; set; } = null;
        internal static ScriptedStockpile ScriptedStockpile = new ScriptedStockpile();

        static string SavestateToLoad = null;
        static string StashkeyToLoad = null;
        static bool interrupt = false;
        static bool clearScripts = false;
        static bool forceStop = false;
        internal static void Initialize()
        {
            Bindings = new ScriptBindings();
            Bindings.BindAssemblyFuncs(Assembly.GetExecutingAssembly());
            //bindings.BindTypeFuncs(typeof(WarlockCore));
            //bindings.BindTypeFuncs(typeof(WarlockCore));

            Standard = new LuaScriptStandard(
                new LuaFuncStandard("StepStart", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                new LuaFuncStandard("StepEnd", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                new LuaFuncStandard("StepPreCorrupt", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                new LuaFuncStandard("StepPostCorrupt", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                new LuaFuncStandard("LoadGameDone", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false)
                );

            Runner = new HookedStateScriptRunner(Standard, Bindings);
        }

        public static void Run()
        {
            Reset();
            ScriptedStockpile.Run();
            RunThisSide();
            LocalNetCoreRouter.Route((PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server ? Routing.Endpoints.EMU_SIDE : Routing.Endpoints.RTC_SIDE), Routing.Commands.RUN, true);
        }

        public static void RunThisSide()
        {
            StartScripts();
        }

        /// <summary>
        /// Resets BOTH sides
        /// </summary>
        internal static void Reset()
        {
            ResetThisSide();
            LocalNetCoreRouter.Route((PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server ? Routing.Endpoints.EMU_SIDE : Routing.Endpoints.RTC_SIDE), Routing.Commands.RESET, true);
        }

        /// <summary>
        /// Resets THIS side
        /// </summary>
        internal static void ResetThisSide()
        {
            ResetInterrupts();
            StopScripts();
            //Runner.Reset();
            Runner = new HookedStateScriptRunner(Standard, Bindings);
            SavestateSystem.Reset();
        }

        private static void ResetInterrupts()
        {
            StashkeyToLoad = null;
            SavestateToLoad = null;
            interrupt = false;
            clearScripts = false;
            forceStop = false;
        }

        public static void StartScripts()
        {
            StepActions.StepStart += StepActions_StepStart;
            StepActions.StepEnd += StepActions_StepEnd;
            StepActions.StepPreCorrupt += StepActions_StepPreCorrupt;
            StepActions.StepPostCorrupt += StepActions_StepPostCorrupt;
        }

        [LunarBindFunction("Warlock.StopScripts")]
        public static void StopScripts()
        {
            StepActions.StepStart -= StepActions_StepStart;
            StepActions.StepEnd -= StepActions_StepEnd;
            StepActions.StepPreCorrupt -= StepActions_StepPreCorrupt;
            StepActions.StepPostCorrupt -= StepActions_StepPostCorrupt;
        }


        private static void RtcCore_LoadGameDone(object sender, EventArgs e)
        {
            Execute("LoadGameDone");
        }


        private static void StepActions_StepPostCorrupt(object sender, EventArgs e)
        {
            Execute("StepPostCorrupt");
        }

        private static void StepActions_StepPreCorrupt(object sender, EventArgs e)
        {
            Execute("StepPreCorrupt");
        }

        private static void StepActions_StepStart(object sender, EventArgs e)
        {
            Execute("StepStart");
        }

        private static void StepActions_StepEnd(object sender, EventArgs e)
        {
            Execute("StepEnd");
        }

        private static void Execute(string key)
        {
            if (!InterruptCheck())
            {
                if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
                {
                    new object();
                }

               Runner.Execute(key);
            }
            InterruptCheck();
        }


        static bool InterruptCheck()
        {
            if (interrupt)
            {
                if (forceStop)
                {
                    Reset();
                    return true;
                }

                if(StashkeyToLoad != null)
                {
                    if (clearScripts)
                    {
                        ClearCurrentScript();
                        LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_STASHKEY, StashkeyToLoad, true);
                    }
                    else
                    {
                        LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_STASHKEY_SAVESTATE, StashkeyToLoad, true);
                    }
                }
                else if (SavestateToLoad != null) 
                {
                    //if (clearScripts) ClearCurrentScript();
                    LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_INTERNAL_SAVESTATE, new object[] { SavestateToLoad, clearScripts }, true);
                }
                ResetInterrupts();
                return true;
            }
            else
            {
                return false;
            }
        }
        
        [LunarBindFunction("Warlock.LoadState")]
        public static void LoadSavestateFromLua(object key, bool clearScript = false)
        {
            interrupt = true;
            clearScripts = clearScript;
            SavestateToLoad = key?.ToString();
        }


        [LunarBindFunction("Warlock.LoadStashkey")]
        public static void LoadStashkeyFromLua(object key, bool clearScript = true)
        {
            interrupt = true;
            clearScripts = clearScript;
            StashkeyToLoad = key?.ToString();
        }

        
        internal static void ClearCurrentScript()
        {
            LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_SCRIPT, "", true);
            LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.LOAD_SCRIPT, "", true);
        }

        internal static async Task LoadScriptedStockpile(string filename)
        {
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                ScriptedStockpile = JsonConvert.DeserializeObject<ScriptedStockpile>(json);
                string sskFile = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".sks");
                await LoadStockpile(sskFile);
            }
        }
        
        /// <summary>
        /// Loads and links stockpile
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static async Task LoadStockpile(string fileName)
        {
            try
            {
                //We do this here and invoke because our unlock runs at the end of the awaited method, but there's a chance an error occurs
                //Thus, we want this to happen within the try block
                UICore.SetHotkeyTimer(false);
                UICore.LockInterface(false, true);
                S.GET<SaveProgressForm>().Dock = DockStyle.Fill;
                var cfForm = (CanvasForm)typeof(CoreForm).GetField("cfForm", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null);
                cfForm?.OpenSubForm(S.GET<SaveProgressForm>());

                StockpileManagerUISide.ClearCurrentStockpile();

                var r = await Task.Run(() => Stockpile.Load(fileName));

                if (r.Failed)
                {
                    MessageBox.Show($"Warlock: Loading the stockpile failed!\n" +
                                    $"{r.GetErrorsFormatted()}");
                    //IF PROBLEM LOOK HERE
                    throw new Exception("Warlock: Loading the stockpile failed!");
                }
                else
                {
                    var sks = r.Result;
                    foreach (StashKey key in sks.StashKeys) //Populate the dgv
                    {
                        if (ScriptedStockpile.ScriptedStashKeys.Any(x => x.StashKeyAlias == key.Alias))
                        {
                            //Set the ref
                            ScriptedStockpile.GetStashKey(key.Alias).StashKeyRef = key;
                        }
                        else
                        {
                            //Create new scripted sk
                            var sk = new ScriptedStashKey();
                            ScriptedStockpile.ScriptedStashKeys.Add(sk);
                            sk.StashKeyRef = key;
                            sk.StashKeyAlias = key.Alias;
                        }
                        //SavestateSystem.StashKeys[key.Alias] = key;
                    }
                    foreach (var sk in sks.StashKeys)
                    {
                        StockpileManagerUISide.CheckAndFixMissingReference(sk, false);
                    }
                }

            }
            finally
            {
                var cfForm = (CanvasForm)typeof(CoreForm).GetField("cfForm", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null);
                cfForm?.CloseSubForm();
                UICore.UnlockInterface();
                UICore.SetHotkeyTimer(true);
            }
        }

    }
}
