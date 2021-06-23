using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warlock.UI
{
    public partial class WarlockPlayer : ComponentForm, IColorize
    {
        public WarlockPlayer()
        {
            InitializeComponent();
            FormClosing += WarlockPlayer_FormClosing;
            WarlockCore.RunningStatusChanged += WarlockCore_RunningStatusChanged;
        }

        private void WarlockCore_RunningStatusChanged(bool obj)
        {
            SyncObjectSingleton.FormExecute(() =>
            {
                lblStatus.Text = "Status:\r\n" + (obj ? "Running" : "Stopped");
            });
        }

        private void WarlockPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            WarlockCore.RunningStatusChanged -= WarlockCore_RunningStatusChanged;
            WarlockCore.Reset();
        }

        private async void btnLoadStockpile_Click(object sender, EventArgs e)
        {
            WarlockCore.Reset();
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "*.wlk|*.wlk" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        this.Enabled = false;
                        await WarlockCore.LoadScriptedStockpile(ofd.FileName);
                        lblCurrent.Text = $"Current Grimoire:\r\n{System.IO.Path.GetFileNameWithoutExtension(ofd.FileName)}";
                        bPlay.Enabled = true;
                        bStop.Enabled = true;
                        bMyEarsHurt.Enabled = true;
                    }
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void bPlay_Click(object sender, EventArgs e)
        {
            WarlockCore.Reset();
            WarlockCore.Run();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            WarlockCore.Reset();
            //Load initial stashkey without a script for an escape from loud noises and whatnot
            //LocalNetCoreRouter.Route(Routing.Endpoints.RTC_SIDE, Routing.Commands.LOAD_STASHKEY_SAVESTATE, WarlockCore.ScriptedStockpile.InitialStashkey, true);
        }

        private async void bMyEarsHurt_Click(object sender, EventArgs e)
        {
            bMyEarsHurt.Enabled = false;
            try
            {
                WarlockCore.Reset();
                await Task.Delay(100);
                AutoKillSwitch.KillEmulator(true);
            }
            finally
            {
                bMyEarsHurt.Enabled = true;
            }
        }
    }
}
