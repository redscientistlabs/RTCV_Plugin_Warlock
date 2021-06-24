using LunarBind;
using LunarBind.Standards;
using System.Text.Json;
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
    internal static class WarlockCore
    {
        internal static HookedStateScriptRunner Runner { get; private set; } = null;
        internal static LuaScriptStandard Standard { get; private set; } = null;
        internal static ScriptBindings Bindings { get; set; } = null;
        internal static ScriptedStockpile ScriptedStockpile = new ScriptedStockpile();
        private static bool _isRunning = false;
        public static bool IsRunning {
            get { return _isRunning; }
            private set {
                _isRunning = value;
                RunningStatusChanged?.Invoke(value);
            } }
        public static event Action<bool> RunningStatusChanged;

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
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                Standard = new LuaScriptStandard(
                    new LuaFuncStandard("StepStart", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("StepEnd", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("StepPreCorrupt", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("StepPostCorrupt", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    //new LuaFuncStandard("LoadGameDone", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("BeforeLoadState", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("AfterLoadState", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false)
                    );
            }
            else
            {
                Standard = new LuaScriptStandard(
                    //new LuaFuncStandard("LoadGameDone", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("BeforeLoadState", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("AfterLoadState", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false)
                    );
            }
            
            Runner = new HookedStateScriptRunner(Standard, Bindings);
        }

        public static void Run()
        {
            Reset();
            ScriptedStockpile.Run();
            RunThisSide();
            LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.RUN, true);
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
            if (IsRunning) return;
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                StepActions.StepStart += StepActions_StepStart;
                StepActions.StepEnd += StepActions_StepEnd;
                StepActions.StepPreCorrupt += StepActions_StepPreCorrupt;
                StepActions.StepPostCorrupt += StepActions_StepPostCorrupt;
            }
            IsRunning = true;
        }

        [LunarBindFunction("Warlock.StopScripts")]
        public static void StopScripts()
        {
            if (!IsRunning) return;
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client)
            {
                StepActions.StepStart -= StepActions_StepStart;
                StepActions.StepEnd -= StepActions_StepEnd;
                StepActions.StepPreCorrupt -= StepActions_StepPreCorrupt;
                StepActions.StepPostCorrupt -= StepActions_StepPostCorrupt;
            }
            IsRunning = false;
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
                try
                {
                    Runner.Execute(key);
                }
                catch(Exception ex)
                {
                    WarlockCore.Reset();
                    SyncObjectSingleton.FormExecute(() =>
                    {
                        MessageBox.Show(ex.Message, "Warlock Script Error", MessageBoxButtons.OK);
                    });
                }
            }
            InterruptCheck();
        }

        private static void ExecuteNoInterrupt(string key)
        {
            try
            {
                Runner.Execute(key);
            }
            catch (Exception ex)
            {
                WarlockCore.Reset();
                SyncObjectSingleton.FormExecute(() =>
                {
                    MessageBox.Show(ex.Message, "Warlock Script Error", MessageBoxButtons.OK);
                });
            }
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
                    ExecuteNoInterrupt("BeforeLoadState");
                    LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_INTERNAL_SAVESTATE, new object[] { SavestateToLoad, clearScripts }, true);
                    ExecuteNoInterrupt("AfterLoadState");
                }
                ResetInterrupts();
                return true;
            }
            else
            {
                return false;
            }
        }
        
        [LunarBindDocumentation("Loads a temporary savestate by id")]
        [LunarBindFunction("Warlock.LoadState")]
        public static void LoadSavestateFromLua(string id, bool clearScript = false)
        {
            interrupt = true;
            clearScripts = clearScript;
            SavestateToLoad = id;
        }

        [LunarBindDocumentation("Loads a StashKey from the stockpile by name, and starts its associated scripts.\r\nStops current stashkey script if clearScript is set to true, default is true")]
        [LunarBindFunction("Warlock.LoadStashkey")]
        public static void LoadStashkeyFromLua(string name, bool clearScript = true)
        {
            interrupt = true;
            clearScripts = clearScript;
            StashkeyToLoad = name; 
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
                ScriptedStockpile = JsonSerializer.Deserialize<ScriptedStockpile>(json, new JsonSerializerOptions() { IncludeFields = true });
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
                            var sk = ScriptedStockpile.GetStashKey(key.Alias);
                            sk.StashKeyRef = key;
                            sk.StashKeyAlias = key.Alias;
                        }
                        else
                        {
                            //Create new scripted sk
                            var sk = new ScriptedStashKey();
                            ScriptedStockpile.ScriptedStashKeys.Add(sk);
                            sk.StashKeyRef = key;
                            sk.StashKeyAlias = key.Alias;
                        }
                    }
                    foreach (var sk in sks.StashKeys)
                    {
                        StockpileManagerUISide.CheckAndFixMissingReference(sk, false, sks.StashKeys);
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
