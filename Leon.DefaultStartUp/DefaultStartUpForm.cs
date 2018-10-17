using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.DefaultStartUp
{
    public partial class DefaultStartUpForm : Form
    {
        public StartUpTemplateClass ParentStartUp = null;

        private int FrameIndex = 0;
        private const byte FrameCount = 60;

        public DefaultStartUpForm()
        {
            this.InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler((Leon, Mathilda) => { this.ParentStartUp?.OnStartUpFinished(Mathilda); });
        }

        private void DefaultStartUpForm_Load(object sender, EventArgs e)
        {
            
        }

        private void DefaultStartUpForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            this.FrameTimer.Start();
        }
        
        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            this.FrameLabel.Image = DefaultStartUpResource.ResourceManager.GetObject("StartingUp_" + this.FrameIndex.ToString()) as Image;
            this.ProgressLabel.Text = string.Format("Hack System Loading ... {0}%", 100 * this.FrameIndex / FrameCount);

            this.FrameIndex = (this.FrameIndex + 1) % FrameCount;
            if (this.FrameIndex == 0)
            {
                this.FrameTimer.Stop();
                this.ProgressLabel.Text = "Hack System Loaded !\n Welcome. (〃'▽'〃)";
                Application.DoEvents();

                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (ILoveU) => {
                        try
                        {
                            while (this.Opacity > 0)
                            {
                                Thread.Sleep(100);
                                this.Opacity -= 0.1;
                            }

                            this.Close();
                        }
                        catch
                        { }
                    }));
            }
        }

    }
}
