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
        private StartUpCardControl _lastActived =null;
        private StartUpCardControl LastActived
        {
            get => _lastActived;
            set
            {
                if (_lastActived != null)
                    _lastActived.IsActived = false;
                _lastActived = value;
                value.IsActived = true;
            }
        }

        public StartUpsCollectionForm()
        {
            InitializeComponent();
        }

        private void StartUpsCollectionForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (ILoveU) => {
                    string ActivedFileName = ConfigController.GetConfig("StartUpFile");
                    string ActivedClassName = ConfigController.GetConfig("StartUpName");
                    foreach (StartUpTemplateClass StartupInstance in StartUpController.ScanStartUpPlugins(UnityModule.StartUpDirectory))
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                try
                                {
                                    StartUpCardControl startUp = new StartUpCardControl(StartupInstance.FileName, StartupInstance.GetType().Name, StartupInstance.Name, StartupInstance.Description, StartupInstance.Preview.Clone() as Image);
                                    startUp.Click += ActiveStartUp;
                                    if (startUp.FileName == ActivedFileName && startUp.ClassName == ActivedClassName)
                                    {
                                        LastActived = startUp;
                                    }
                                    StartUpsLayoutPanel.Controls.Add(startUp);
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

        private void ActiveStartUp(object sender, EventArgs e)
        {
            if (!(sender is StartUpCardControl)) return;

            //预览
            //StartUpController.GetStartUpPlugin(FileController.PathCombine(UnityModule.StartUpDirectory, (sender as StartUpCardControl).FileName), (sender as StartUpCardControl).ClassName).StartUpForm.Show(this);

            if (MessageBox.Show(string.Format("是否使用启动画面 {0} ？", (sender as StartUpCardControl).Name), "使用启动画面？", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                ConfigController.SetConfig("StartUpFile", (sender as StartUpCardControl).FileName);
                ConfigController.SetConfig("StartUpName", (sender as StartUpCardControl).ClassName);
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("更新 StartUp 配置遇到异常：{0}", ex.Message);
                MessageBox.Show(string.Format("更新 StartUp 配置遇到异常：{0}", ex.Message));
                return;
            }
            LastActived = (sender as StartUpCardControl);
            MessageBox.Show(string.Format("更新 StartUp 配置成功，重启即可查看效果"));
            //Application.Restart();
        }

    }
}
