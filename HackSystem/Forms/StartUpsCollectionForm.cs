using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using StartUpTemplate;

namespace HackSystem
{
    public partial class StartUpsCollectionForm : Form
    {
        public StartUpsCollectionForm()
        {
            InitializeComponent();
        }

        private void StartUpsCollectionForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (ILoveU) => {
                    foreach (StartUpTemplateClass StartupInstance in StartUpController.ScanStartUpPlugins(UnityModule.StartUpDirectory))
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                try
                                {
                                    StartUpCardControl startUp = new StartUpCardControl(StartupInstance.FileName, StartupInstance.GetType().Name, StartupInstance.Name, StartupInstance.Description, StartupInstance.Preview.Clone() as Image);
                                    flowLayoutPanel1.Controls.Add(startUp);
                                }
                                catch (Exception ex)
                                {
                                    UnityModule.DebugPrint("StartUp集合窗口扫描时遇到异常：{0}", ex.Message);
                                }
                            }));
                        }
                        catch { }
                    }
                }));
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
