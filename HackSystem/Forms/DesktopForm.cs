using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
