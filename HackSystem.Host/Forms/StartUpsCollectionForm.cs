using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace HackSystem.Host
{
    public partial class StartUpsCollectionForm : Form
    {
        private CardControl _lastActived =null;
        private CardControl LastActived
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
                                    CardControl startUp = new CardControl(StartupInstance.FileName, StartupInstance.GetType().Name, StartupInstance.Name, StartupInstance.Description, StartupInstance.Preview.Clone() as Image);
                                    startUp.Click += ActiveStartUp;
                                    if (startUp.FileName == ActivedFileName && startUp.ClassName == ActivedClassName)
                                    {
                                        LastActived = startUp;
                                    }
                                    StartUpsLayoutPanel.Controls.Add(startUp);
                                }
                                catch (Exception ex)
                                {
                                    LogController.Error("StartUp集合窗口扫描时遇到异常：{0}", ex.Message);
                                }
                            }));
                            (StartupInstance as IDisposable).Dispose();
                        }
                        catch { }
                    }
                }));
        }

        private void ActiveStartUp(object sender, EventArgs e)
        {
            if (!(sender is CardControl)) return;

            //预览
            //StartUpController.GetStartUpPlugin(FileController.PathCombine(UnityModule.StartUpDirectory, (sender as StartUpCardControl).FileName), (sender as StartUpCardControl).ClassName).StartUpForm.Show(this);

            if (MessageBox.Show(string.Format("是否使用启动画面 {0} ？", (sender as CardControl).Name), "使用启动画面？", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                ConfigController.SetConfig("StartUpFile", (sender as CardControl).FileName);
                ConfigController.SetConfig("StartUpName", (sender as CardControl).ClassName);
            }
            catch (Exception ex)
            {
                LogController.Warn("更新 StartUp 配置遇到异常：{0}", ex.Message);
                MessageBox.Show(string.Format("更新 StartUp 配置遇到异常：{0}", ex.Message));
                return;
            }
            LastActived = (sender as CardControl);
            MessageBox.Show(string.Format("更新 StartUp 配置成功，重启即可查看效果"));
            //Application.Restart();
        }

    }
}
