using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LunarBind;
using LunarBind.Standards;
using System.ComponentModel;
using Warlock.Structure;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Json;
using System.Drawing;

namespace Warlock.UI
{
    public partial class WarlockEditor : ComponentForm, IColorize
    {
        //public static BindingList<ScriptedStashKey> bindingList = new BindingList<ScriptedStashKey>();
        ScriptedStashKey currentStashKey = null;
        DgvScriptItem currentGlobalScriptItem = null;
        string CurrentFile = null;
        public WarlockEditor()
        {
            InitializeComponent();

            //dgvStockpile.ColumnCount = 3;
            //dgvStockpile.Columns[0].Name = "Name";
            //dgvStockpile.Columns[1].Name = "Game";
            //dgvStockpile.Columns[2].Name = "Note";

            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;

            dgvGlobalScripts.KeyDown += Dgv_KeyDown;
            dgvStockpile.KeyDown += Dgv_KeyDown;
            dgvStockpile.ScrollBars = ScrollBars.Vertical;
            tbLuaRTC.Enabled = false;
            tbLuaEMU.Enabled = false;
            tbLuaRTC.TextChanged += TbLuaRTC_TextChanged;
            tbLuaEMU.TextChanged += TbLuaEMU_TextChanged;
            var paths = GlobalScriptBindings.GetAllRegisteredPaths();
            paths.AddRange(WarlockCore.Bindings.GetAllRegisteredPaths());
            //paths.Distinct()
            string externalAutoCompleteStr = string.Join(" ", paths);

            tbLuaRTC.ExternalAutocompleteString = externalAutoCompleteStr;
            tbLuaEMU.ExternalAutocompleteString = externalAutoCompleteStr;

            tbLuaRTC.KeyDown += TbLua_KeyDown;
            tbLuaEMU.KeyDown += TbLua_KeyDown;

            //tbLuaEMU.PreviewKeyDown += TbLuaEMU_PreviewKeyDown;
            //tbLuaEMU.ExternalStartAutocompleteShowStr = "Warlock Emulator"

            FormClosing += WarlockEditor_FormClosing;
            WarlockCore.RunningStatusChanged += WarlockCore_RunningStatusChanged;
            //dgvStockpile.DataSource = 
        }

        private void WarlockCore_RunningStatusChanged(bool obj)
        {
            SyncObjectSingleton.FormExecute(() =>
            {
                lblStatus.Text = (obj ? "Running" : "Stopped");
            });
        }

        bool rtcTabHasCode = false;
        bool emuTabHasCode = false;

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            // This event is called once for each tab button in your tab control

            // First paint the background with a color based on the current tab

            // e.Index is the index of the tab in the TabPages collection.
            bool tabHasCode = false;
            switch (e.Index)
            {
                case 0:
                    tabHasCode = emuTabHasCode;
                    break;
                case 1:
                    tabHasCode = rtcTabHasCode;
                    break;
                default:
                    break;
            }

            if (tabHasCode)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 158, 32)), e.Bounds);//Green
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(51, 158, 32)), .5f), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(191, 155, 34)), e.Bounds);//Orange
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(173, 141, 33)), .5f), e.Bounds);
            }

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-4, -2);
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, tabControl.TabPages[e.Index].Font, new SolidBrush(Color.Black), paddedBounds);
        }

        //Ignore adding certain special characters, as is the default for some reason
        private void TbLua_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Modifiers == Keys.Control)
            {
                if(!(e.KeyCode == Keys.C 
                    || e.KeyCode == Keys.V
                    || e.KeyCode == Keys.X
                    || e.KeyCode == Keys.Z
                    || e.KeyCode == Keys.Y
                    || e.KeyCode == Keys.L
                    || e.KeyCode == Keys.D
                    || e.KeyCode == Keys.A
                    || e.KeyCode == Keys.Back
                    || e.KeyCode == Keys.Up
                    || e.KeyCode == Keys.Down 
                    || e.KeyCode == Keys.Left
                    || e.KeyCode == Keys.Right
                    || e.KeyCode == Keys.PageUp
                    || e.KeyCode == Keys.PageDown
                    || e.KeyCode == Keys.Shift
                    || e.KeyCode == Keys.ShiftKey
                    || e.KeyCode == Keys.LShiftKey
                    || e.KeyCode == Keys.RShiftKey
                    ))
                {
                    if (e.KeyCode == Keys.S)
                    {
                        if (CurrentFile == null)
                        {
                            SaveAs();
                        }
                        else
                        {
                            Save(CurrentFile);
                        }
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void Dgv_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void WarlockEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            var drs = MessageBox.Show("Save Changes?", "Editor Closing", MessageBoxButtons.YesNoCancel);

            if(drs == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

            if (drs == DialogResult.Yes)
            {
                if (CurrentFile == null)
                {
                    SaveAs();
                }
                else
                {
                    Save(CurrentFile);
                }
            }

            WarlockCore.RunningStatusChanged -= WarlockCore_RunningStatusChanged;
            WarlockCore.Reset();

        }

        private void TbLuaRTC_TextChanged(object sender, EventArgs e)
        {
            if (currentStashKey != null) { currentStashKey.ScriptRTC = tbLuaRTC.Text; }
            else if(currentGlobalScriptItem != null) { currentGlobalScriptItem.ScriptRTC = tbLuaRTC.Text; }

            bool oldStatus = rtcTabHasCode;
            rtcTabHasCode = !string.IsNullOrWhiteSpace(tbLuaRTC.Text);
            if (oldStatus != rtcTabHasCode)
            {
                tabControl.Invalidate();
            }
        }

        private void TbLuaEMU_TextChanged(object sender, EventArgs e)
        {
            if (currentStashKey != null) { currentStashKey.ScriptEMU = tbLuaEMU.Text; }
            else if(currentGlobalScriptItem != null) { currentGlobalScriptItem.ScriptEMU = tbLuaEMU.Text; }

            bool oldStatus = emuTabHasCode;
            emuTabHasCode = !string.IsNullOrWhiteSpace(tbLuaEMU.Text);
            if (oldStatus != emuTabHasCode)
            {
                tabControl.Invalidate();
            }
        }



        public async Task LoadStockpile(string fileName)
        {

            await WarlockCore.LoadStockpile(fileName);
            foreach (var key in WarlockCore.ScriptedStockpile.ScriptedStashKeys)
            {
                dgvStockpile?.Rows.Add(key, key.StashKeyRef.GameName, key.StashKeyRef.Note, WarlockCore.ScriptedStockpile.InitialStashkey == key.StashKeyAlias);
            }
            RefreshNoteIcons();
        }


        public void RefreshNoteIcons()
        {
            foreach (DataGridViewRow dataRow in dgvStockpile.Rows)
            {
                StashKey sk = ((ScriptedStashKey)dataRow.Cells["SKName"].Value)?.StashKeyRef;
                if (sk == null)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(sk.Note))
                {
                    dataRow.Cells["Note"].Value = string.Empty;
                }
                else
                {
                    dataRow.Cells["Note"].Value = "📝";
                }
            }
        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "*.sks|*.sks" })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    await LoadStockpile(ofd.FileName);
                }
            }
        }

        bool internalClear = false;
        internal void HandleCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex == -1)
            {
                return;
            }
            

            try
            {
                //S.GET<StashHistoryForm>().btnAddStashToStockpile.Enabled = false;
                dgvStockpile.Enabled = false;

                //// Stockpile BUTTON handling
                if (e != null)
                {
                    var senderGrid = (DataGridView)sender;
                    if (e.RowIndex >= 0 && e.ColumnIndex > -1)
                    {
                        if (senderGrid.Columns[e.ColumnIndex].Name == "Note")
                        {
                            ScriptedStashKey sk = (ScriptedStashKey)senderGrid.Rows[e.RowIndex].Cells["SKName"].Value;
                            if (sk != null)
                            {
                                S.SET(new NoteEditorForm(sk.StashKeyRef, senderGrid.Rows[e.RowIndex].Cells["Note"]));
                                S.GET<NoteEditorForm>().Show();
                            }
                            return;
                        }
                        else if (senderGrid.Columns[e.ColumnIndex].Name == "Play")
                        {
                            ScriptedStashKey sk = (ScriptedStashKey)senderGrid.Rows[e.RowIndex].Cells["SKName"].Value;
                            if (sk != null)
                            {
                                WarlockCore.ScriptedStockpile.InitialStashkey = sk.StashKeyRef.Alias;

                                for (int j = 0; j < senderGrid.Rows.Count; j++)
                                {
                                    senderGrid.Rows[j].Cells[3].Value = j == e.RowIndex;
                                }
                            }
                            return;
                        }
                    }
                }

                if (dgvStockpile.SelectedCells.Count > 0)
                {

                    var sk = (ScriptedStashKey)dgvStockpile.SelectedCells[0].OwningRow.Cells[0].Value;
                    if (sk != null)
                    {
                        tbLuaEMU.Enabled = true;
                        tbLuaRTC.Enabled = true;
                        currentGlobalScriptItem = null;
                        currentStashKey = sk;
                        lblEditing.Text = $"Currently Editing: {currentStashKey?.StashKeyAlias ?? "ERROR"}";
                        tbLuaEMU.Text = sk.ScriptEMU;
                        tbLuaRTC.Text = sk.ScriptRTC;
                    }
                    else
                    {
                        tbLuaEMU.Enabled = false;
                        tbLuaRTC.Enabled = false;
                    }
                }
            }
            finally
            {
                dgvStockpile.Enabled = true;
                if (!internalClear)
                {
                    internalClear = true;
                    dgvGlobalScripts.ClearSelection();
                    //dgvStockpile.ClearSelection();
                    internalClear = false;
                }
                tabControl.Invalidate();
            }
        }

        private void SaveAs() 
        { 
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "*.wlk|*.wlk" })
            {
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    Save(sfd.FileName);
                }
            }
        
        }
        private void Save(string filename, bool forceStockpileSave = false)
        {
            try
            {
                forceStockpileSave = forceStockpileSave || forceOverwriteStockpileToolStripMenuItem.Checked;

                this.Enabled = false;
                string sskFile = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".sks");
                var stockpile = new Stockpile();
                foreach (var sk in WarlockCore.ScriptedStockpile.ScriptedStashKeys)
                {
                    if (sk.StashKeyRef != null)
                    {
                        stockpile.StashKeys.Add(sk.StashKeyRef);
                    }
                }

                var wc = WarlockCore.ScriptedStockpile;

                string json = JsonSerializer.Serialize(wc, typeof(ScriptedStockpile), new JsonSerializerOptions() { IncludeFields = true });
                File.WriteAllText(filename, json);
                CurrentFile = filename;

                if (forceStockpileSave)
                {
                    Stockpile.Save(stockpile, sskFile, false, true);
                }
                else if (!File.Exists(sskFile))
                {
                    var res = MessageBox.Show($"Stockpile with name {Path.GetFileName(sskFile)} does not exist in location.\r\nSave a copy of the loaded stockpile?", "Stockpile File Not Available", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        Stockpile.Save(stockpile, sskFile, false, true);
                    }
                } 
                //Else skip saving stockpile

            }
            finally
            {
                this.Enabled = true;
            }
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void New()
        {
            WarlockCore.ScriptedStockpile = new ScriptedStockpile();
            currentStashKey = null;
            currentGlobalScriptItem = null;
            lblEditing.Text = "Currently Editing: N/A";
            tbLuaEMU.Enabled = false;
            tbLuaRTC.Enabled = false;
            dgvStockpile.Rows.Clear();
            dgvGlobalScripts.Rows.Clear();
            CurrentFile = null;
            tbLuaRTC.Text = "";
            tbLuaEMU.Text = "";
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) 
            {
                SaveAs();
            }
            else
            {
                Save(CurrentFile);
            }
        }

        private async void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "*.wlk|*.wlk" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            this.Enabled = false;
                            WarlockCore.ScriptedStockpile = new ScriptedStockpile();
                            currentStashKey = null;
                            currentGlobalScriptItem = null;
                            lblEditing.Text = "Currently Editing: N/A";
                            tbLuaEMU.Enabled = false;
                            tbLuaRTC.Enabled = false;
                            tbLuaRTC.Text = "";
                            tbLuaEMU.Text = "";
                            dgvStockpile.Rows.Clear();
                            dgvGlobalScripts.Rows.Clear();
                            CurrentFile = null;
                            await WarlockCore.LoadScriptedStockpile(ofd.FileName);
                            foreach (var key in WarlockCore.ScriptedStockpile.ScriptedStashKeys)
                            {
                                dgvStockpile?.Rows.Add(key, key.StashKeyRef.GameName, key.StashKeyRef.Note, WarlockCore.ScriptedStockpile.InitialStashkey == key.StashKeyAlias);
                            }
                            HashSet<string> globals = new HashSet<string>();
                            foreach (var item in WarlockCore.ScriptedStockpile.GlobalScriptsRTC)
                            {
                                globals.Add(item.Key);
                            }
                            foreach (var item in WarlockCore.ScriptedStockpile.GlobalScriptsEMU)
                            {
                                globals.Add(item.Key);
                            }

                            foreach (var item in globals)
                            {
                                dgvGlobalScripts.Rows.Add(item, "🗑");
                            }
                            CurrentFile = ofd.FileName;
                            RefreshNoteIcons();
                        }
                        catch
                        {
                            New();
                        }
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void bAddGlobalScript_Click(object sender, EventArgs e)
        {
            MyTextPrompt prompt = new MyTextPrompt("Global Script Name", "Enter Global Script Name");
            if(prompt.ShowDialog() == DialogResult.OK)
            {
                var globalScriptName = prompt.PromptText;
                if (WarlockCore.ScriptedStockpile.GlobalScriptsEMU.ContainsKey(globalScriptName) || WarlockCore.ScriptedStockpile.GlobalScriptsRTC.ContainsKey(globalScriptName))
                {
                    MessageBox.Show("Cannot add duplicate Global Script");
                }
                else
                {
                    dgvGlobalScripts.Rows.Add(globalScriptName, "🗑");
                    dgvGlobalScripts.ClearSelection();
                    currentGlobalScriptItem = null;
                    currentStashKey = null;
                    lblEditing.Text = "Currently Editing: N/A";
                    tbLuaRTC.Text = "";
                    tbLuaEMU.Text = "";
                }
            }
        }

        private void dgvGlobalScripts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex == -1)
            {
                return;
            }


            try
            {
                //S.GET<StashHistoryForm>().btnAddStashToStockpile.Enabled = false;
                dgvGlobalScripts.Enabled = false;

                //// Stockpile BUTTON handling
                if (e != null)
                {
                    var senderGrid = (DataGridView)sender;
                    if (e.RowIndex >= 0 && e.ColumnIndex > -1)
                    {
                        if (senderGrid.Columns[e.ColumnIndex].Name == "DeleteGlobal")
                        {
                            string gsn = (string)senderGrid.Rows[e.RowIndex].Cells["GlobalScriptName"].Value;
                            if (!string.IsNullOrWhiteSpace(gsn))
                            {
                                WarlockCore.ScriptedStockpile.GlobalScriptsRTC.Remove(gsn);
                                WarlockCore.ScriptedStockpile.GlobalScriptsEMU.Remove(gsn);
                                senderGrid.Rows.RemoveAt(e.RowIndex);

                                currentStashKey = null;
                                currentGlobalScriptItem = null;
                                lblEditing.Text = "Currently Editing: N/A";
                                tbLuaEMU.Enabled = false;
                                tbLuaRTC.Enabled = false;
                                tbLuaRTC.Text = "";
                                tbLuaEMU.Text = "";
                            }
                            return;
                        }
                    }
                }

                if (dgvGlobalScripts.SelectedCells.Count > 0)
                {
                    var globalScriptName = (string)dgvGlobalScripts.SelectedCells[0].OwningRow.Cells[0].Value;
                    if (!string.IsNullOrWhiteSpace(globalScriptName))
                    {
                        tbLuaEMU.Enabled = true;
                        tbLuaRTC.Enabled = true;
                        var gsrtc = WarlockCore.ScriptedStockpile.GlobalScriptsRTC;
                        var gsemu = WarlockCore.ScriptedStockpile.GlobalScriptsEMU;
                        gsrtc.TryGetValue(globalScriptName, out string rtc);
                        gsemu.TryGetValue(globalScriptName, out string emu);
                        currentStashKey = null;
                        currentGlobalScriptItem = new DgvScriptItem(globalScriptName, rtc, emu);
                        lblEditing.Text = $"Currently Editing: {globalScriptName}";
                        tbLuaRTC.Text = currentGlobalScriptItem.ScriptRTC;
                        tbLuaEMU.Text = currentGlobalScriptItem.ScriptEMU;
                    }
                    else
                    {
                        currentStashKey = null;
                        currentGlobalScriptItem = null;
                        lblEditing.Text = "Currently Editing: N/A";
                        tbLuaEMU.Enabled = false;
                        tbLuaRTC.Enabled = false;
                        tbLuaRTC.Text = "";
                        tbLuaEMU.Text = "";
                    }
                }
            }
            finally
            {
                dgvGlobalScripts.Enabled = true;
                //if (!internalClear)
                //{
                //    internalClear = true;
                //    //dgvStockpile.ClearSelection();
                //    //dgvGlobalScripts.ClearSelection();
                //    internalClear = false;
                //}
                dgvStockpile.ClearSelection();
                tabControl.Invalidate();
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            WarlockCore.Reset();
        }

        private void bRun_Click(object sender, EventArgs e)
        {
            WarlockCore.Run();
        }

        private void forceOverwriteStockpileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tsm = sender as ToolStripMenuItem;
            tsm.Checked = !tsm.Checked;
        }
    }

    class DgvScriptItem
    {
        public string Name;

        public string ScriptRTC
        {
            get
            {
                if (WarlockCore.ScriptedStockpile.GlobalScriptsRTC.TryGetValue(Name, out string value))
                {
                    return value;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    WarlockCore.ScriptedStockpile.GlobalScriptsRTC.Remove(Name);
                }
                else
                {
                    WarlockCore.ScriptedStockpile.GlobalScriptsRTC[Name] = value;
                }
            }
        }

        public string ScriptEMU
        {
            get
            {
                if (WarlockCore.ScriptedStockpile.GlobalScriptsEMU.TryGetValue(Name, out string value))
                {
                    return value;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    WarlockCore.ScriptedStockpile.GlobalScriptsEMU.Remove(Name);
                }
                else
                {
                    WarlockCore.ScriptedStockpile.GlobalScriptsEMU[Name] = value;
                }
            }
        }

        public DgvScriptItem(string name, string scriptRTC, string scriptEMU)
        {
            Name = name;
            ScriptEMU = scriptEMU;
            ScriptRTC = scriptRTC;
        }
    }


}
