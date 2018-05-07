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
            try
            {
                foreach (StartUpTemplateClass StartupInstance in StartUpController.ScanStartUpPlugins(UnityModule.StartUpDirectory))
                {
                    //TODO : 使用用户控件显示，记录DLL文件名称和TypeName，以方便写入Config;
                    Label StartUpLabel = new Label()
                    {
                        AutoEllipsis = true,
                        AutoSize = false,
                        BorderStyle = BorderStyle.FixedSingle,
                        //注意！这里需要Clone图像的精确副本，因为StartUpIntance使用后会Dispose，防止图像触发空对象引用
                        Image = StartupInstance.Preview.Clone() as Image,
                        Size=new Size(160,120),
                        ImageAlign = ContentAlignment.TopCenter,
                        Margin = new Padding(5),
                        TextAlign = ContentAlignment.BottomCenter,
                        Text = StartupInstance.Name + "\n" + StartupInstance.Description
                    };
                    flowLayoutPanel1.Controls.Add(StartUpLabel);
                            
                }
            }
            catch { }
        }

    }
}
