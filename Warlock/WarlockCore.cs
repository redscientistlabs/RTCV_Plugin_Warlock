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
using System.Text.RegularExpressions;

namespace Warlock
{
    internal static class WarlockCore
    {
        internal static HookedStateScriptRunner Runner { get; private set; } = null;
        internal static LuaScriptStandard Standard { get; private set; } = null;
        internal static ScriptBindings Bindings { get; set; } = null;
        internal static ScriptedStockpile ScriptedStockpile = new ScriptedStockpile();

        internal static List<StashKey> OriginalStashKeys { get; private set; } = new List<StashKey>();

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
        static string ScriptToLoad = null;
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
                    new LuaFuncStandard("StepStart", LuaFuncType.AutoCoroutine, false),
                    new LuaFuncStandard("StepEnd", LuaFuncType.AutoCoroutine, false),
                    new LuaFuncStandard("StepPreCorrupt", LuaFuncType.AutoCoroutine, false),
                    new LuaFuncStandard("StepPostCorrupt", LuaFuncType.AutoCoroutine, false),
                    //new LuaFuncStandard("LoadGameDone", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("BeforeLoadState", LuaFuncType.AutoCoroutine, false),
                    new LuaFuncStandard("AfterLoadState", LuaFuncType.AutoCoroutine, false)
                    );
            }
            else
            {
                Standard = new LuaScriptStandard(
                    //new LuaFuncStandard("LoadGameDone", LuaFuncType.AutoCoroutine | LuaFuncType.AllowAny, false),
                    new LuaFuncStandard("BeforeLoadState", LuaFuncType.AutoCoroutine, false),
                    new LuaFuncStandard("AfterLoadState", LuaFuncType.AutoCoroutine, false)
                    );
            }
            
            Runner = new HookedStateScriptRunner(Standard, Bindings);
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client) { Lua.LuaManager.EmulatorOnlyBindings.InitializeRunner(Runner); }
            else { Lua.LuaManager.UIOnlyBindings.InitializeRunner(Runner); }
        }

        public static void Run()
        {
            Reset();
            //Set default savestates
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server)
            {
                foreach (var sk in ScriptedStockpile.ScriptedStashKeys)
                {
                    SavestateSystem.SetSaveState(sk.StashKeyAlias, sk.StashKeyRef);
                }
            }
            ScriptedStockpile.Run();
            RunThisSide();
            LocalNetCoreRouter.Route(Routing.Endpoints.EMU_SIDE, Routing.Commands.RUN, true);
        }

        public static void RunThisSide()
        {
            StartScripts();
        }

        internal static string GetOtherSide()
        {
            return (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server ? Routing.Endpoints.EMU_SIDE : Routing.Endpoints.RTC_SIDE);
        }

        /// <summary>
        /// Resets BOTH sides
        /// </summary>
        internal static void Reset()
        {
            ResetThisSide();
            LocalNetCoreRouter.Route(GetOtherSide(), Routing.Commands.RESET, true);
        }

        /// <summary>
        /// Resets THIS side
        /// </summary>
        internal static void ResetThisSide()
        {
            ResetInterrupts();
            StopScripts();
            BlastLayerSystem.Reset();
            SavestateSystem.Reset();
            //Replace with original on rtc side, should be in order (CHECK DATA GRID VIEW!!)
            if (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server)
            {
                for (int j = 0; j < ScriptedStockpile.ScriptedStashKeys.Count; j++)
                {
                    SavestateSystem.TryDeleteTemporarySavestate(ScriptedStockpile.ScriptedStashKeys[j].StashKeyRef);
                    ScriptedStockpile.ScriptedStashKeys[j].StashKeyRef = OriginalStashKeys[j];
                }
            }

            Runner = new HookedStateScriptRunner(Standard, Bindings); //fully reset runner
            if(PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Client) { Lua.LuaManager.EmulatorOnlyBindings.InitializeRunner(Runner); }
            else { Lua.LuaManager.UIOnlyBindings.InitializeRunner(Runner); }
        }

        private static void ResetInterrupts()
        {
            StashkeyToLoad = null;
            SavestateToLoad = null;
            ScriptToLoad = null;
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

        internal static void Execute(string key)
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

        internal static void ExecuteNoInterrupt(string key)
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
                    LocalNetCoreRouter.Route(GetOtherSide(), Routing.Commands.EXECUTE_HOOK_NO_INTERRUPT, "BeforeLoadState", true);

                    LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_INTERNAL_SAVESTATE, new object[] { SavestateToLoad, clearScripts }, true);
                    ExecuteNoInterrupt("AfterLoadState");
                    LocalNetCoreRouter.Route(GetOtherSide(), Routing.Commands.EXECUTE_HOOK_NO_INTERRUPT, "AfterLoadState", true);

                }
                else if(ScriptToLoad != null)
                {
                    ExecuteNoInterrupt("BeforeScriptLoad");
                    LocalNetCoreRouter.Route(GetOtherSide(), Routing.Commands.EXECUTE_HOOK_NO_INTERRUPT, "BeforeScriptLoad", true);

                    LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_NAMED_SCRIPT, ScriptToLoad, true);

                    ExecuteNoInterrupt("AfterScriptLoad");
                    LocalNetCoreRouter.Route(GetOtherSide(), Routing.Commands.EXECUTE_HOOK_NO_INTERRUPT, "AfterScriptLoad", true);
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

        [LunarBindDocumentation("Loads a Script from the stockpile by name, and starts it\r\nDoes not load a savestate")]
        [LunarBindFunction("Warlock.LoadScript")]
        public static void LoadScriptFromLua(string name)
        {
            interrupt = true;
            ScriptToLoad = name;
        }

        public static void SaveStashkeyState()
        {

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

                bool missingAny = WarlockCore.ScriptedStockpile.ScriptedStashKeys.Any(x => x.StashKeyRef == null);
                if (missingAny)
                {
                    var missing = WarlockCore.ScriptedStockpile.ScriptedStashKeys.Where(x => x.StashKeyRef == null).Select(y => y.StashKeyAlias);
                    SyncObjectSingleton.FormExecute(() =>
                    {
                        MessageBox.Show($"The following stash key references are missing and their scripts will be removed:\r\n{string.Join(", ", missing)}", "Stockpile Stashkeys Missing!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    });
                    WarlockCore.ScriptedStockpile.ScriptedStashKeys.RemoveAll(x => x.StashKeyRef == null);
                }

                if (string.IsNullOrWhiteSpace(ScriptedStockpile.InitialStashkey) || !ScriptedStockpile.ScriptedStashKeys.Any(x => x.StashKeyAlias == ScriptedStockpile.InitialStashkey))
                {
                    throw new Exception("Warlock: Loading the stockpile failed, InitialStashkey not set, or StashKey is missing from stockpile");
                }
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
                    bool needsSaving = false;
                    //TODO: detect if refs fixed and ask to save
                    foreach (var sk in sks.StashKeys)
                    {
                        needsSaving |= CheckAndFixMissingReference(sk, false, sks.StashKeys);
                    }

                    if (needsSaving)
                    {
                        var save = MessageBox.Show($"{nameof(Warlock)}: References for {fileName} have been updated. Save reference changes?", "References Updated", MessageBoxButtons.YesNo);
                        if(save == DialogResult.Yes)
                        {
                            if(!Stockpile.Save(sks, fileName, false, true))
                            {
                                MessageBox.Show($"{nameof(Warlock)}: Failed to save updated references {fileName}.\r\nReport to devs", "Save failed", MessageBoxButtons.OK);
                            }
                        }
                    }

                    //Add originals in
                    OriginalStashKeys.Clear();
                    foreach (var sk in ScriptedStockpile.ScriptedStashKeys)
                    {
                        OriginalStashKeys.Add(sk.StashKeyRef);
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


        //Modified from StockpileManagerUISide
        /// <summary>
        /// Takes a stashkey and a list of keys, fixing the path and if a list of keys is provided, it'll look for all shared references and update them
        /// </summary>
        /// <param name="psk"></param>
        /// <param name="force"></param>
        /// <param name="keys"></param>
        /// <returns>If the stockpile needs saving</returns>
        internal static bool CheckAndFixMissingReference(StashKey psk, bool force = false, List<StashKey> keys = null, string customTitle = null, string customMessage = null)
        {
            if (psk == null)
            {
                throw new ArgumentNullException(nameof(psk));
            }

            if (!(bool?)AllSpec.VanguardSpec[VSPEC.SUPPORTS_REFERENCES] ?? false)
            {
                //Hack hack hack
                //In pre-504, some stubs would save references. This results in a fun infinite loop
                //As such, delete the referenced file because it doesn't matter as the implementation doesn't support references
                //Only do this if we explicitly know that the references are not supported. If there's missing spec info, don't do it.
                if (!(bool?)AllSpec.VanguardSpec[VSPEC.SUPPORTS_REFERENCES] == true)
                {
                    try
                    {
                        File.Delete(psk.RomFilename);
                    }
                    catch (Exception ex)
                    {
                       throw new Exception( "Som-ething went terribly wrong when fixing missing references\n" +
                            "Your stockpile should be fine (might prompt you to fix it on load)" +
                            "Report this to the devs.", ex);
                    }
                    psk.RomFilename = "";
                    return true;
                }
            }

            string message = customMessage ?? $"Can't find file {psk.RomFilename}\nGame name: {psk.GameName}\nSystem name: {psk.SystemName}\n\n To continue loading, provide a new file for replacement.";
            string title = customTitle ?? "Error: File not found";

            if ((force || !File.Exists(psk.RomFilename)) && !psk.RomFilename.EndsWith("IGNORE"))
            {
                if (DialogResult.OK == MessageBox.Show(message, title, MessageBoxButtons.OKCancel))
                {
                    OpenFileDialog ofd = new OpenFileDialog
                    {
                        DefaultExt = "*",
                        Title = "Select Replacement File",
                        Filter = $"Any file|*.*",
                        RestoreDirectory = true
                    };
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string filename = ofd.FileName;
                        string oldFilename = psk.RomFilename;
                        if (Path.GetFileName(psk.RomFilename) != Path.GetFileName(filename))
                        {
                            var dialogRes = MessageBox.Show($"Selected file {Path.GetFileName(filename)} has a different name than the old file {Path.GetFileName(psk.RomFilename)}.\nIf you know this file is correct, you can ignore this warning.\nContinue?", title,
                                    MessageBoxButtons.OKCancel);
                            if (DialogResult.Cancel == dialogRes)
                            {
                                return false;
                            }
                        }

                        foreach (var sk in keys.Where(x => x.RomFilename == oldFilename))
                        {
                            sk.RomFilename = filename;
                            sk.RomShortFilename = Path.GetFileName(sk.RomFilename);
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


    }
}
