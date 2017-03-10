using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Classmates
{
    public partial class frmShow : Form
    {
        frmMain fm = new frmMain();

        public frmShow(frmMain fm)
        {
            InitializeComponent();
            this.fm = fm;
        }
        // 实现窗口的淡入淡出效果
        private void frmShow_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                Application.DoEvents();
                this.Opacity = i / 100.0;
                System.Threading.Thread.Sleep(20);
            }
            this.Close();
        }

        private void frmShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 100; i > 0; i--)
            {
                Application.DoEvents();
                this.Opacity = i / 100.0;
                System.Threading.Thread.Sleep(20);
            }
            Application.OpenForms["frmMain"].Visible = true;
            fm.LoadMainInfo();
        }
    }
}
