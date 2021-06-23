
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
            this.importStockpileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvStockpile = new System.Windows.Forms.DataGridView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageRTC = new System.Windows.Forms.TabPage();
            this.tabPageEMU = new System.Windows.Forms.TabPage();
            this.SKName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Game = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Play = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tbLuaRTC = new Lua.UI.ScriptSyntaxTextbox();
            this.tbLuaEMU = new Lua.UI.ScriptSyntaxTextbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bAddGlobalScript = new System.Windows.Forms.Button();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageRTC.SuspendLayout();
            this.tabPageEMU.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
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
            // importStockpileToolStripMenuItem
            // 
            this.importStockpileToolStripMenuItem.Name = "importStockpileToolStripMenuItem";
            this.importStockpileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importStockpileToolStripMenuItem.Text = "Import Stockpile";
            this.importStockpileToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // dgvStockpile
            // 
            this.dgvStockpile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStockpile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockpile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockpile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SKName,
            this.Game,
            this.Note,
            this.Play});
            this.dgvStockpile.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvStockpile.Location = new System.Drawing.Point(12, 49);
            this.dgvStockpile.Name = "dgvStockpile";
            this.dgvStockpile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvStockpile.Size = new System.Drawing.Size(384, 285);
            this.dgvStockpile.TabIndex = 2;
            this.dgvStockpile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HandleCellClick);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageRTC);
            this.tabControl.Controls.Add(this.tabPageEMU);
            this.tabControl.Location = new System.Drawing.Point(402, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(509, 443);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageRTC
            // 
            this.tabPageRTC.Controls.Add(this.tbLuaRTC);
            this.tabPageRTC.Location = new System.Drawing.Point(4, 22);
            this.tabPageRTC.Name = "tabPageRTC";
            this.tabPageRTC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRTC.Size = new System.Drawing.Size(501, 417);
            this.tabPageRTC.TabIndex = 0;
            this.tabPageRTC.Text = "RTC";
            this.tabPageRTC.UseVisualStyleBackColor = true;
            // 
            // tabPageEMU
            // 
            this.tabPageEMU.Controls.Add(this.tbLuaEMU);
            this.tabPageEMU.Location = new System.Drawing.Point(4, 22);
            this.tabPageEMU.Name = "tabPageEMU";
            this.tabPageEMU.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEMU.Size = new System.Drawing.Size(501, 417);
            this.tabPageEMU.TabIndex = 1;
            this.tabPageEMU.Text = "Emulator";
            this.tabPageEMU.UseVisualStyleBackColor = true;
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
            this.Game.FillWeight = 180F;
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
            this.Play.FillWeight = 50F;
            this.Play.HeaderText = "Play";
            this.Play.Name = "Play";
            // 
            // tbLuaRTC
            // 
            this.tbLuaRTC.AdditionalSelectionTyping = true;
            this.tbLuaRTC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLuaRTC.CaretForeColor = System.Drawing.Color.White;
            this.tbLuaRTC.CaretLineBackColor = System.Drawing.Color.Blue;
            this.tbLuaRTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLuaRTC.File = "";
            this.tbLuaRTC.Lines = new string[] {
        ""};
            this.tbLuaRTC.LinesNew = new string[] {
        ""};
            this.tbLuaRTC.Location = new System.Drawing.Point(3, 3);
            this.tbLuaRTC.MouseSelectionRectangularSwitch = true;
            this.tbLuaRTC.MultipleSelection = true;
            this.tbLuaRTC.Name = "tbLuaRTC";
            this.tbLuaRTC.Size = new System.Drawing.Size(495, 411);
            this.tbLuaRTC.TabIndex = 0;
            this.tbLuaRTC.VirtualSpaceOptions = ScintillaNET.VirtualSpace.RectangularSelection;
            // 
            // tbLuaEMU
            // 
            this.tbLuaEMU.AdditionalSelectionTyping = true;
            this.tbLuaEMU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLuaEMU.CaretForeColor = System.Drawing.Color.White;
            this.tbLuaEMU.CaretLineBackColor = System.Drawing.Color.Blue;
            this.tbLuaEMU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLuaEMU.File = "";
            this.tbLuaEMU.Lines = new string[] {
        ""};
            this.tbLuaEMU.LinesNew = new string[] {
        ""};
            this.tbLuaEMU.Location = new System.Drawing.Point(3, 3);
            this.tbLuaEMU.MouseSelectionRectangularSwitch = true;
            this.tbLuaEMU.MultipleSelection = true;
            this.tbLuaEMU.Name = "tbLuaEMU";
            this.tbLuaEMU.Size = new System.Drawing.Size(495, 411);
            this.tbLuaEMU.TabIndex = 3;
            this.tbLuaEMU.VirtualSpaceOptions = ScintillaNET.VirtualSpace.RectangularSelection;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(12, 366);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 104);
            this.panel1.TabIndex = 5;
            // 
            // bAddGlobalScript
            // 
            this.bAddGlobalScript.Location = new System.Drawing.Point(321, 340);
            this.bAddGlobalScript.Name = "bAddGlobalScript";
            this.bAddGlobalScript.Size = new System.Drawing.Size(75, 23);
            this.bAddGlobalScript.TabIndex = 6;
            this.bAddGlobalScript.Text = "Add";
            this.bAddGlobalScript.UseVisualStyleBackColor = true;
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As..";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // WarlockEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(923, 482);
            this.Controls.Add(this.bAddGlobalScript);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.dgvStockpile);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WarlockEditor";
            this.Tag = "color:normal";
            this.Text = "Warlock Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockpile)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageRTC.ResumeLayout(false);
            this.tabPageEMU.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvStockpile;
        private System.Windows.Forms.ToolStripMenuItem importStockpileToolStripMenuItem;
        private Lua.UI.ScriptSyntaxTextbox tbLuaEMU;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageRTC;
        private Lua.UI.ScriptSyntaxTextbox tbLuaRTC;
        private System.Windows.Forms.TabPage tabPageEMU;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Game;
        private System.Windows.Forms.DataGridViewButtonColumn Note;
        private System.Windows.Forms.DataGridViewButtonColumn Play;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bAddGlobalScript;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    }
}