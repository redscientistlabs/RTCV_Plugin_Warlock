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
using Newtonsoft.Json;
using System.IO;

namespace Warlock.UI
{
    public partial class WarlockEditor : ComponentForm, IColorize
    {
        //public static BindingList<ScriptedStashKey> bindingList = new BindingList<ScriptedStashKey>();
        ScriptedStashKey currentStashKey = null;
        string CurrentFile = null;
        public WarlockEditor()
        {
            InitializeComponent();
            
            //dgvStockpile.ColumnCount = 3;
            //dgvStockpile.Columns[0].Name = "Name";
            //dgvStockpile.Columns[1].Name = "Game";
            //dgvStockpile.Columns[2].Name = "Note";
            dgvStockpile.ScrollBars = ScrollBars.Vertical;
            tbLuaEMU.Enabled = false;
            tbLuaRTC.Enabled = false;
            tbLuaEMU.TextChanged += TbLuaEMU_TextChanged;
            tbLuaRTC.TextChanged += TbLuaRTC_TextChanged;
            //dgvStockpile.DataSource = 
        }

        private void TbLuaRTC_TextChanged(object sender, EventArgs e)
        {
            if (currentStashKey != null) { currentStashKey.ScriptRTC = tbLuaRTC.Text; }
        }

        private void TbLuaEMU_TextChanged(object sender, EventArgs e)
        {
            if (currentStashKey != null) { currentStashKey.ScriptEMU = tbLuaEMU.Text; }
        }

        public async Task LoadStockpile(string fileName)
        {

            await WarlockCore.LoadStockpile(fileName);
            foreach (var key in WarlockCore.ScriptedStockpile.ScriptedStashKeys)
            {
                dgvStockpile?.Rows.Add(key, key.StashKeyRef.GameName, key.StashKeyRef.Note, "ᐅ");
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
                            S.SET(new NoteEditorForm(sk.StashKeyRef, senderGrid.Rows[e.RowIndex].Cells["Note"]));
                            S.GET<NoteEditorForm>().Show();
                            return;
                        }
                        else if (senderGrid.Columns[e.ColumnIndex].Name == "Play")
                        {
                            ScriptedStashKey sk = (ScriptedStashKey)senderGrid.Rows[e.RowIndex].Cells["SKName"].Value;
                            if (sk != null)
                            {
                                WarlockCore.ScriptedStockpile.InitialStashkey = sk.StashKeyRef.Alias;
                                WarlockCore.Run();
                            }

                            //S.SET(new NoteEditorForm(sk.StashKeyRef, senderGrid.Rows[e.RowIndex].Cells["Note"]));
                            //S.GET<NoteEditorForm>().Show();
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
                        // SyncObjectSingleton.FormExecute(() =>
                        //{
                        //    MessageBox.Show($"Clicked on {sk.ToString()}");
                        //});
                        currentStashKey = sk;
                        //TODO: SHOW SCRIPTS
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
                    dgvStockpile.ClearSelection();
                    internalClear = false;
                }
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
        private void Save(string filename)
        {
            try
            {
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
                if (Stockpile.Save(stockpile, sskFile, false, true))
                {
                    string json = JsonConvert.SerializeObject(WarlockCore.ScriptedStockpile);
                    File.WriteAllText(filename, json);
                    CurrentFile = filename;
                }
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WarlockCore.ScriptedStockpile = new ScriptedStockpile();
            currentStashKey = null;
            tbLuaEMU.Enabled = false;
            tbLuaRTC.Enabled = false;
            dgvStockpile.Rows.Clear();
            CurrentFile = null;
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
                        this.Enabled = false;
                        WarlockCore.ScriptedStockpile = new ScriptedStockpile();
                        currentStashKey = null;
                        tbLuaEMU.Enabled = false;
                        tbLuaRTC.Enabled = false;
                        dgvStockpile.Rows.Clear();
                        CurrentFile = null;
                        await WarlockCore.LoadScriptedStockpile(ofd.FileName);
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }
    }
}
