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
                Label ProgramLabel = new Label()
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    ImageAlign = ContentAlignment.TopCenter,
                    Image = new Bitmap(this.Icon.ToBitmap(), 50, 50),
                    TextAlign = ContentAlignment.BottomCenter,
                    Text = ProgramInstance.Name,
                    ForeColor= Color.DimGray,
                    Location = new Point(100, 100),
                    Size = new Size(50,64),
                    Tag = ProgramInstance,
                };
                this.Controls.Add(ProgramLabel);
                ProgramLabel.Click += new EventHandler(
                    (s, e) => {
                        ProgramTemplateClass tag = (s as Label).Tag as ProgramTemplateClass;
                        tag.GetNewProgramForm().Show(this);
                        if (tag.ProgramForms.Count == 5)
                        {
                            while (tag.ProgramForms.Count > 0)
                            {
                                tag.ProgramForms[0]?.Close();
                            }
                        }
                    });
            }
        }
    }
}
