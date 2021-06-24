
namespace Warlock.UI
{
    partial class WarlockEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarlockEditor));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importStockpileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceOverwriteStockpileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvStockpile = new System.Windows.Forms.DataGridView();
            this.SKName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Game = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Play = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageEMU = new System.Windows.Forms.TabPage();
            this.tbLuaEMU = new Lua.UI.ScriptSyntaxTextbox();
            this.tabPageRTC = new System.Windows.Forms.TabPage();
            this.tbLuaRTC = new Lua.UI.ScriptSyntaxTextbox();
            this.panelGlobals = new System.Windows.Forms.Panel();
            this.dgvGlobalScripts = new System.Windows.Forms.DataGridView();
            this.GlobalScriptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteGlobal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bAddGlobalScript = new System.Windows.Forms.Button();
            this.lblGlobalScripts = new System.Windows.Forms.Label();
            this.lblStockpile = new System.Windows.Forms.Label();
            this.bStop = new System.Windows.Forms.Button();
            this.bRun = new System.Windows.Forms.Button();
            this.lblEditing = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageEMU.SuspendLayout();
            this.tabPageRTC.SuspendLayout();
            this.panelGlobals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalScripts)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.documentationToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(923, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "Menu Strip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.importStockpileToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // importStockpileToolStripMenuItem
            // 
            this.importStockpileToolStripMenuItem.Name = "importStockpileToolStripMenuItem";
            this.importStockpileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.importStockpileToolStripMenuItem.Text = "Import Stockpile";
            this.importStockpileToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadToolStripMenuItem.Text = "Open";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forceOverwriteStockpileToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // forceOverwriteStockpileToolStripMenuItem
            // 
            this.forceOverwriteStockpileToolStripMenuItem.Name = "forceOverwriteStockpileToolStripMenuItem";
            this.forceOverwriteStockpileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.forceOverwriteStockpileToolStripMenuItem.Text = "Overwrite Stockpile";
            this.forceOverwriteStockpileToolStripMenuItem.Click += new System.EventHandler(this.forceOverwriteStockpileToolStripMenuItem_Click);
            // 
            // dgvStockpile
            // 
            this.dgvStockpile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockpile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockpile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SKName,
            this.Game,
            this.Note,
            this.Play});
            this.dgvStockpile.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvStockpile.Location = new System.Drawing.Point(12, 60);
            this.dgvStockpile.Name = "dgvStockpile";
            this.dgvStockpile.ReadOnly = true;
            this.dgvStockpile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvStockpile.Size = new System.Drawing.Size(384, 274);
            this.dgvStockpile.TabIndex = 2;
            this.dgvStockpile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HandleCellClick);
            // 
            // SKName
            // 
            this.SKName.FillWeight = 200F;
            this.SKName.HeaderText = "Stashkey Name";
            this.SKName.Name = "SKName";
            this.SKName.ReadOnly = true;
            // 
            // Game
            // 
            this.Game.FillWeight = 160F;
            this.Game.HeaderText = "Game";
            this.Game.Name = "Game";
            this.Game.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.FillWeight = 50F;
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            // 
            // Play
            // 
            this.Play.FillWeight = 75F;
            this.Play.HeaderText = "Startup";
            this.Play.Name = "Play";
            this.Play.ReadOnly = true;
            this.Play.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageEMU);
            this.tabControl.Controls.Add(this.tabPageRTC);
            this.tabControl.Location = new System.Drawing.Point(402, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(509, 439);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageEMU
            // 
            this.tabPageEMU.Controls.Add(this.tbLuaEMU);
            this.tabPageEMU.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageEMU.Location = new System.Drawing.Point(4, 22);
            this.tabPageEMU.Name = "tabPageEMU";
            this.tabPageEMU.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEMU.Size = new System.Drawing.Size(501, 413);
            this.tabPageEMU.TabIndex = 1;
            this.tabPageEMU.Text = "EMU";
            this.tabPageEMU.UseVisualStyleBackColor = true;
            // 
            // tbLuaEMU
            // 
            this.tbLuaEMU.AdditionalSelectionTyping = true;
            this.tbLuaEMU.AutoCIgnoreCase = true;
            this.tbLuaEMU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLuaEMU.CaretForeColor = System.Drawing.Color.White;
            this.tbLuaEMU.CaretLineBackColor = System.Drawing.Color.Blue;
            this.tbLuaEMU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLuaEMU.ExternalAutocompleteString = "";
            this.tbLuaEMU.ExternalStartAutocompleteShowStr = "";
            this.tbLuaEMU.File = "";
            this.tbLuaEMU.Lexer = ScintillaNET.Lexer.Lua;
            this.tbLuaEMU.Lines = new string[] {
        ""};
            this.tbLuaEMU.LinesNew = new string[] {
        ""};
            this.tbLuaEMU.Location = new System.Drawing.Point(3, 3);
            this.tbLuaEMU.MouseSelectionRectangularSwitch = true;
            this.tbLuaEMU.MultipleSelection = true;
            this.tbLuaEMU.Name = "tbLuaEMU";
            this.tbLuaEMU.Size = new System.Drawing.Size(495, 407);
            this.tbLuaEMU.TabIndex = 3;
            this.tbLuaEMU.TabWidth = 2;
            this.tbLuaEMU.VirtualSpaceOptions = ScintillaNET.VirtualSpace.RectangularSelection;
            // 
            // tabPageRTC
            // 
            this.tabPageRTC.Controls.Add(this.tbLuaRTC);
            this.tabPageRTC.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageRTC.Location = new System.Drawing.Point(4, 22);
            this.tabPageRTC.Name = "tabPageRTC";
            this.tabPageRTC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRTC.Size = new System.Drawing.Size(501, 413);
            this.tabPageRTC.TabIndex = 0;
            this.tabPageRTC.Text = "RTC";
            this.tabPageRTC.UseVisualStyleBackColor = true;
            // 
            // tbLuaRTC
            // 
            this.tbLuaRTC.AdditionalSelectionTyping = true;
            this.tbLuaRTC.AutoCIgnoreCase = true;
            this.tbLuaRTC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLuaRTC.CaretForeColor = System.Drawing.Color.White;
            this.tbLuaRTC.CaretLineBackColor = System.Drawing.Color.Blue;
            this.tbLuaRTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLuaRTC.ExternalAutocompleteString = "";
            this.tbLuaRTC.ExternalStartAutocompleteShowStr = "";
            this.tbLuaRTC.File = "";
            this.tbLuaRTC.Lexer = ScintillaNET.Lexer.Lua;
            this.tbLuaRTC.Lines = new string[] {
        ""};
            this.tbLuaRTC.LinesNew = new string[] {
        ""};
            this.tbLuaRTC.Location = new System.Drawing.Point(3, 3);
            this.tbLuaRTC.MouseSelectionRectangularSwitch = true;
            this.tbLuaRTC.MultipleSelection = true;
            this.tbLuaRTC.Name = "tbLuaRTC";
            this.tbLuaRTC.Size = new System.Drawing.Size(495, 407);
            this.tbLuaRTC.TabIndex = 0;
            this.tbLuaRTC.TabWidth = 2;
            this.tbLuaRTC.VirtualSpaceOptions = ScintillaNET.VirtualSpace.RectangularSelection;
            // 
            // panelGlobals
            // 
            this.panelGlobals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelGlobals.AutoScroll = true;
            this.panelGlobals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelGlobals.Controls.Add(this.dgvGlobalScripts);
            this.panelGlobals.Location = new System.Drawing.Point(12, 366);
            this.panelGlobals.Name = "panelGlobals";
            this.panelGlobals.Size = new System.Drawing.Size(384, 117);
            this.panelGlobals.TabIndex = 5;
            // 
            // dgvGlobalScripts
            // 
            this.dgvGlobalScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvGlobalScripts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGlobalScripts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGlobalScripts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GlobalScriptName,
            this.DeleteGlobal});
            this.dgvGlobalScripts.Location = new System.Drawing.Point(0, 0);
            this.dgvGlobalScripts.Name = "dgvGlobalScripts";
            this.dgvGlobalScripts.ReadOnly = true;
            this.dgvGlobalScripts.Size = new System.Drawing.Size(384, 117);
            this.dgvGlobalScripts.TabIndex = 0;
            this.dgvGlobalScripts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGlobalScripts_CellClick);
            // 
            // GlobalScriptName
            // 
            this.GlobalScriptName.HeaderText = "Name";
            this.GlobalScriptName.Name = "GlobalScriptName";
            this.GlobalScriptName.ReadOnly = true;
            // 
            // DeleteGlobal
            // 
            this.DeleteGlobal.FillWeight = 10F;
            this.DeleteGlobal.HeaderText = "";
            this.DeleteGlobal.Name = "DeleteGlobal";
            this.DeleteGlobal.ReadOnly = true;
            // 
            // bAddGlobalScript
            // 
            this.bAddGlobalScript.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAddGlobalScript.Location = new System.Drawing.Point(321, 340);
            this.bAddGlobalScript.Name = "bAddGlobalScript";
            this.bAddGlobalScript.Size = new System.Drawing.Size(75, 23);
            this.bAddGlobalScript.TabIndex = 6;
            this.bAddGlobalScript.Text = "Add";
            this.bAddGlobalScript.UseVisualStyleBackColor = true;
            this.bAddGlobalScript.Click += new System.EventHandler(this.bAddGlobalScript_Click);
            // 
            // lblGlobalScripts
            // 
            this.lblGlobalScripts.AutoSize = true;
            this.lblGlobalScripts.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGlobalScripts.ForeColor = System.Drawing.Color.White;
            this.lblGlobalScripts.Location = new System.Drawing.Point(12, 345);
            this.lblGlobalScripts.Name = "lblGlobalScripts";
            this.lblGlobalScripts.Size = new System.Drawing.Size(78, 13);
            this.lblGlobalScripts.TabIndex = 7;
            this.lblGlobalScripts.Text = "Global Scripts";
            // 
            // lblStockpile
            // 
            this.lblStockpile.AutoSize = true;
            this.lblStockpile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockpile.ForeColor = System.Drawing.Color.White;
            this.lblStockpile.Location = new System.Drawing.Point(12, 44);
            this.lblStockpile.Name = "lblStockpile";
            this.lblStockpile.Size = new System.Drawing.Size(54, 13);
            this.lblStockpile.TabIndex = 8;
            this.lblStockpile.Text = "Stockpile";
            // 
            // bStop
            // 
            this.bStop.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStop.Location = new System.Drawing.Point(321, 31);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 23);
            this.bStop.TabIndex = 9;
            this.bStop.Text = "Stop Scripts";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bRun
            // 
            this.bRun.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRun.Location = new System.Drawing.Point(240, 31);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(75, 23);
            this.bRun.TabIndex = 10;
            this.bRun.Text = "Run";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // lblEditing
            // 
            this.lblEditing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEditing.AutoSize = true;
            this.lblEditing.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditing.ForeColor = System.Drawing.Color.White;
            this.lblEditing.Location = new System.Drawing.Point(403, 473);
            this.lblEditing.Name = "lblEditing";
            this.lblEditing.Size = new System.Drawing.Size(119, 13);
            this.lblEditing.TabIndex = 11;
            this.lblEditing.Text = "Currently Editing: N/A";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(180, 31);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Stopped";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // WarlockEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(923, 495);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblEditing);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.lblStockpile);
            this.Controls.Add(this.lblGlobalScripts);
            this.Controls.Add(this.bAddGlobalScript);
            this.Controls.Add(this.panelGlobals);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.dgvStockpile);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(939, 521);
            this.Name = "WarlockEditor";
            this.Tag = "color:normal";
            this.Text = "Warlock Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageEMU.ResumeLayout(false);
            this.tabPageRTC.ResumeLayout(false);
            this.panelGlobals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlobalScripts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvStockpile;
        private System.Windows.Forms.ToolStripMenuItem importStockpileToolStripMenuItem;
        private Lua.UI.ScriptSyntaxTextbox tbLuaEMU;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageRTC;
        private Lua.UI.ScriptSyntaxTextbox tbLuaRTC;
        private System.Windows.Forms.TabPage tabPageEMU;
        private System.Windows.Forms.Panel panelGlobals;
        private System.Windows.Forms.Button bAddGlobalScript;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvGlobalScripts;
        private System.Windows.Forms.DataGridViewTextBoxColumn GlobalScriptName;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteGlobal;
        private System.Windows.Forms.Label lblGlobalScripts;
        private System.Windows.Forms.Label lblStockpile;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Game;
        private System.Windows.Forms.DataGridViewButtonColumn Note;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Play;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.Label lblEditing;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceOverwriteStockpileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
    }
}