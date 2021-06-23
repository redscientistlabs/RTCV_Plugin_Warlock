
namespace Warlock.UI
{
    partial class MyTextPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyTextPrompt));
            this.tbData = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(12, 46);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(162, 20);
            this.tbData.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(198, 44);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 1;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.ForeColor = System.Drawing.Color.White;
            this.lblPrompt.Location = new System.Drawing.Point(12, 21);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(34, 13);
            this.lblPrompt.TabIndex = 2;
            this.lblPrompt.Text = "Label";
            // 
            // MyTextPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ClientSize = new System.Drawing.Size(285, 122);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.tbData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(301, 161);
            this.MinimumSize = new System.Drawing.Size(301, 161);
            this.Name = "MyTextPrompt";
            this.Text = "MyTextPrompt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lblPrompt;
    }
}