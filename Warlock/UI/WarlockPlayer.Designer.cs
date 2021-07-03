
namespace Warlock.UI
{
    partial class WarlockPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarlockPlayer));
            this.btnLoadStockpile = new System.Windows.Forms.Button();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.bPlay = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bMyEarsHurt = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoadStockpile
            // 
            this.btnLoadStockpile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadStockpile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.btnLoadStockpile.FlatAppearance.BorderSize = 0;
            this.btnLoadStockpile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadStockpile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLoadStockpile.ForeColor = System.Drawing.Color.Black;
            this.btnLoadStockpile.Location = new System.Drawing.Point(12, 12);
            this.btnLoadStockpile.Name = "btnLoadStockpile";
            this.btnLoadStockpile.Size = new System.Drawing.Size(78, 32);
            this.btnLoadStockpile.TabIndex = 124;
            this.btnLoadStockpile.Text = "Load";
            this.btnLoadStockpile.UseVisualStyleBackColor = false;
            this.btnLoadStockpile.Click += new System.EventHandler(this.btnLoadStockpile_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.White;
            this.lblCurrent.Location = new System.Drawing.Point(111, 12);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(96, 26);
            this.lblCurrent.TabIndex = 125;
            this.lblCurrent.Text = "Current Grimoire:\r\nNone";
            // 
            // bPlay
            // 
            this.bPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPlay.BackColor = System.Drawing.Color.LimeGreen;
            this.bPlay.Enabled = false;
            this.bPlay.FlatAppearance.BorderSize = 0;
            this.bPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlay.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.bPlay.ForeColor = System.Drawing.Color.Black;
            this.bPlay.Location = new System.Drawing.Point(12, 63);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(78, 70);
            this.bPlay.TabIndex = 127;
            this.bPlay.Text = "Play";
            this.bPlay.UseVisualStyleBackColor = false;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bStop.BackColor = System.Drawing.Color.Red;
            this.bStop.Enabled = false;
            this.bStop.FlatAppearance.BorderSize = 0;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.bStop.ForeColor = System.Drawing.Color.Black;
            this.bStop.Location = new System.Drawing.Point(114, 63);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(85, 70);
            this.bStop.TabIndex = 129;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = false;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bMyEarsHurt
            // 
            this.bMyEarsHurt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMyEarsHurt.BackColor = System.Drawing.Color.Yellow;
            this.bMyEarsHurt.Enabled = false;
            this.bMyEarsHurt.FlatAppearance.BorderSize = 0;
            this.bMyEarsHurt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMyEarsHurt.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.bMyEarsHurt.ForeColor = System.Drawing.Color.Black;
            this.bMyEarsHurt.Location = new System.Drawing.Point(224, 63);
            this.bMyEarsHurt.Name = "bMyEarsHurt";
            this.bMyEarsHurt.Size = new System.Drawing.Size(85, 70);
            this.bMyEarsHurt.TabIndex = 130;
            this.bMyEarsHurt.Text = "Emergency Stop";
            this.bMyEarsHurt.UseVisualStyleBackColor = false;
            this.bMyEarsHurt.Click += new System.EventHandler(this.bMyEarsHurt_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(213, 12);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 26);
            this.lblStatus.TabIndex = 131;
            this.lblStatus.Text = "Status:\r\nStopped";
            // 
            // WarlockPlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(321, 145);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.bMyEarsHurt);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bPlay);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.btnLoadStockpile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(337, 184);
            this.MinimumSize = new System.Drawing.Size(337, 184);
            this.Name = "WarlockPlayer";
            this.Tag = "color:normal";
            this.Text = "Warlock Player";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadStockpile;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bMyEarsHurt;
        private System.Windows.Forms.Label lblStatus;
    }
}