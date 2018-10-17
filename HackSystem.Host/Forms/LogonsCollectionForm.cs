using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace HackSystem.Host
{
    public partial class LogonsCollectionForm : Form
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

        public LogonsCollectionForm()
        {
            InitializeComponent();
        }

        private void LogonsCollectionForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (ILoveU) => {
                    string ActivedFileName = ConfigController.GetConfig("LogonFile");
                    string ActivedClassName = ConfigController.GetConfig("LogonName");
                    foreach (LogonTemplateClass LogonInstance in LogonController.ScanLogonPlugins(UnityModule.LogonDirectory))
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                try
                                {
                                    CardControl Logon = new CardControl(LogonInstance.FileName, LogonInstance.GetType().Name, LogonInstance.Name, LogonInstance.Description, LogonInstance.Preview.Clone() as Image);
                                    Logon.Click += ActiveLogon;
                                    if (Logon.FileName == ActivedFileName && Logon.ClassName == ActivedClassName)
                                    {
                                        LastActived = Logon;
                                    }
                                    LogonsLayoutPanel.Controls.Add(Logon);
                                }
                                catch (Exception ex)
                                {
                                    LogController.Error("Logon集合窗口扫描时遇到异常：{0}", ex.Message);
                                }
                            }));
                            (LogonInstance as IDisposable).Dispose();
                        }
                        catch { }
                    }
                }));
        }

        private void ActiveLogon(object sender, EventArgs e)
        {
            if (!(sender is CardControl)) return;

            //预览
            //LogonController.GetLogonPlugin(FileController.PathCombine(UnityModule.LogonDirectory, (sender as CardControl).FileName), (sender as CardControl).ClassName).LogonForm.Show(this);

            if (MessageBox.Show(string.Format("是否使用登录界面 {0} ？", (sender as CardControl).Name), "使用登录界面？", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                ConfigController.SetConfig("LogonFile", (sender as CardControl).FileName);
                ConfigController.SetConfig("LogonName", (sender as CardControl).ClassName);
            }
            catch (Exception ex)
            {
                LogController.Warn("更新 Logon 配置遇到异常：{0}", ex.Message);
                MessageBox.Show(string.Format("更新 Logon 配置遇到异常：{0}", ex.Message));
                return;
            }
            LastActived = (sender as CardControl);
            MessageBox.Show(string.Format("更新 Logon 配置成功，重启即可查看效果"));
            //Application.Restart();
        }

    }
}
