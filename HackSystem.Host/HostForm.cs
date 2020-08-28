using System.Windows.Forms;
using HackSystem.Host.Configs;

namespace HackSystem.Host
{
    public partial class HostForm : Form
    {
        public HostForm()
        {
            this.Text = $"{HostConfigs.Title} {Application.ProductVersion}";
            this.Icon = HostResource.Logo;

            this.InitializeComponent();
        }
    }
}
