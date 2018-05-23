using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public partial class DefaultProgramForm : Form
    {
        public DefaultProgramForm()
        {
            InitializeComponent();
        }

        private void DefaultProgramForm_Load(object sender, EventArgs e)
        {
            this.Text = this.GetHashCode().ToString("X");
        }
    }
}
