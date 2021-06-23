using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warlock.UI
{
    public partial class MyTextPrompt : Form
    {
        public string PromptText { get; private set; }
        public MyTextPrompt()
        {
            InitializeComponent();
        }

        public MyTextPrompt(string title, string prompt)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            Text = title;
            lblPrompt.Text = prompt;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbData.Text)) { DialogResult = DialogResult.OK; }
            PromptText = tbData.Text;
            Close();
        }
    }
}
