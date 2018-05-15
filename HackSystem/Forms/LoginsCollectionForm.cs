using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LoginTemplate;

namespace HackSystem
{
    public partial class LoginsCollectionForm : Form
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

        public LoginsCollectionForm()
        {
            InitializeComponent();
        }

        private void LoginsCollectionForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            ThreadPool.QueueUserWorkItem(new WaitCallback(
                (ILoveU) => {
                    string ActivedFileName = ConfigController.GetConfig("LoginFile");
                    string ActivedClassName = ConfigController.GetConfig("LoginName");
                    foreach (LoginTemplateClass LoginInstance in LoginController.ScanLoginPlugins(UnityModule.LoginDirectory))
                    {
                        try
                        {
                            this.Invoke(new Action(() =>
                            {
                                try
                                {
                                    CardControl Login = new CardControl(LoginInstance.FileName, LoginInstance.GetType().Name, LoginInstance.Name, LoginInstance.Description, LoginInstance.Preview.Clone() as Image);
                                    Login.Click += ActiveLogin;
                                    if (Login.FileName == ActivedFileName && Login.ClassName == ActivedClassName)
                                    {
                                        LastActived = Login;
                                    }
                                    LoginsLayoutPanel.Controls.Add(Login);
                                }
                                catch (Exception ex)
                                {
                                    LogController.Error("Login集合窗口扫描时遇到异常：{0}", ex.Message);
                                }
                            }));
                            (LoginInstance as IDisposable).Dispose();
                        }
                        catch { }
                    }
                }));
        }

        private void ActiveLogin(object sender, EventArgs e)
        {
            if (!(sender is CardControl)) return;

            //预览
            //LoginController.GetLoginPlugin(FileController.PathCombine(UnityModule.LoginDirectory, (sender as CardControl).FileName), (sender as CardControl).ClassName).LoginForm.Show(this);

            if (MessageBox.Show(string.Format("是否使用登录界面 {0} ？", (sender as CardControl).Name), "使用登录界面？", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                ConfigController.SetConfig("LoginFile", (sender as CardControl).FileName);
                ConfigController.SetConfig("LoginName", (sender as CardControl).ClassName);
            }
            catch (Exception ex)
            {
                LogController.Warn("更新 Login 配置遇到异常：{0}", ex.Message);
                MessageBox.Show(string.Format("更新 Login 配置遇到异常：{0}", ex.Message));
                return;
            }
            LastActived = (sender as CardControl);
            MessageBox.Show(string.Format("更新 Login 配置成功，重启即可查看效果"));
            //Application.Restart();
        }

    }
}
