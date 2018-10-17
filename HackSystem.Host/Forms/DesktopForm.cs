using System;
using System.Threading;
using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace HackSystem.Host
{
    public partial class DesktopForm : Form
    {
        /// <summary>
        /// 是否允许退出
        /// </summary>
        protected static bool AllowToQuit = false;

        public DesktopForm()
        {
            InitializeComponent();
            this.Icon = UnityResource.HackSystemLogoIcon;
        }

        private void ShowStartUpsButton_Click(object sender, EventArgs e)
        {
            new StartUpsCollectionForm().Show(this);
        }

        private void ShowLogonsButton_Click(object sender, EventArgs e)
        {
            new LogonsCollectionForm().Show(this);
        }

        private void DesktopForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            LoadProgram();
        }

        /// <summary>
        /// 加载Program插件集合
        /// </summary>
        private void LoadProgram()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate {
                foreach (ProgramTemplateClass ProgramInstance in ProgramController.ScanProgramPlugins(UnityModule.ProgramDirectory))
                {
                    try
                    {
                        ProgramIconControl ProgramCard = new ProgramIconControl(ProgramInstance.Name, ProgramInstance.Icon, ProgramInstance);
                        ProgramCard.Click += new EventHandler(
                            (s, e) =>
                            {
                                ProgramTemplateClass CurrentProgram = (s as ProgramIconControl).ParentProgram;
                                CurrentProgram.GetNewProgramForm().Show(this);
                            });
                        this.Invoke(new Action(delegate
                        {
                            ProgramLayoutPanel.Controls.Add(ProgramCard);
                        }));
                    }
                    catch (Exception ex)
                    {
                        LogController.Error("加载程序插件遇到异常：{0}", ex.Message);
                    }
                }
            }));
        }

        private void DesktopForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowToQuit)
            {
                e.Cancel = true;
                if (MessageBox.Show("真的要退出 HackSystem 吗？", "真的要退出吗？", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    AllowToQuit = false;
                }
                else
                {
                    AllowToQuit = true;
                    this.Close();
                }
            }
        }
    }
}
