using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DefaultStartUp
{
    public partial class DefaultStartUpForm : Form
    {
        public DefaultStartUpClass ParentStartUp = null;
        private int FrameIndex = 0;
        private const byte FrameCount = 60;

        public DefaultStartUpForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler((Leon, Mathilda) => { ParentStartUp?.OnStartUpFinished(EventArgs.Empty); });
        }

        private void DefaultStartUpForm_Load(object sender, EventArgs e)
        {
            
        }

        private void DefaultStartUpForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            FrameTimer.Start();
        }
        
        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            FrameLabel.Image = StartUpResource.ResourceManager.GetObject("StartingUp_" + FrameIndex.ToString()) as Image;
            ProgressLabel.Text = string.Format("Hack System Loading ... {0}%", 100 * FrameIndex / FrameCount);

            FrameIndex = (FrameIndex + 1) % FrameCount;
            if (FrameIndex == 0)
            {
                ProgressLabel.Text = "Hack System Loaded !\n Welcome. (〃'▽'〃)";
                FrameTimer.Stop();
                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (ILoveU) => {
                        while (this.Opacity > 0)
                        {
                            Thread.Sleep(100);
                            this.Opacity -= 0.1;
                        }

                        this.Close();
                    }));
            }
        }

    }
}
