using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramTemplate;

namespace HackSystem
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

        private void button1_Click(object sender, EventArgs e)
        {
            new StartUpsCollectionForm().Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LoginsCollectionForm().Show(this);
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
            //TODO : 测试代码（扫描演示程序插件，并显示图标，点击图标将新建程序窗口，达到5个程序窗口时关闭全部程序窗口）
            foreach (ProgramTemplateClass ProgramInstance in ProgramController.ScanProgramPlugins(UnityModule.ProgramDirectory))
            {
                ProgramIconControl ProgramCard = new ProgramIconControl(ProgramInstance.Name, ProgramInstance.Icon, ProgramInstance);
                ProgramCard.Click += new EventHandler(
                    (s, e) => {
                        ProgramTemplateClass CurrentProgram = (s as ProgramIconControl).ParentProgram;
                        CurrentProgram.GetNewProgramForm().Show(this);
                    });

                ProgramLayoutPanel.Controls.Add(ProgramCard);
            }
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
