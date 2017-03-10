using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;
namespace Classmates
{
    public partial class frmLogin : Form
    {
        private string path;
        private string name, password;
        // 为了防止退出本窗口是退出整个应用程序
        private bool load = false;

        private frmMain fm;
        public frmLogin(frmMain fm)
        {
            InitializeComponent();
            // 保证路径正确，根目录下和文件夹下得到的路径是不同的
            if (Application.StartupPath.EndsWith("\\"))
            {
                path = Application.StartupPath;
            }
            else
            {
                path = Application.StartupPath + "\\";
            }
            this.fm = fm;
            this.Icon = fm.Icon;
        }
        // 退出整个程序
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // 关掉本窗口则退出整个程序
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 若因登录成功而关闭本窗口则不会退出整个程序
            if (load == false)
            {
                Application.Exit();
            }
        }
        // 在用户名处按下回车，将焦点转移到密码框中
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                textBox2.Focus();
            }
        }
        // 载入user.hs中的信息
        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path + "Info\\user.hs"))
            {
                MessageBox.Show("用户配置信息不存在，请补充相关信息！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmMainSet fms = new frmMainSet(true,fm);
                fms.Show();
                this.Close();
                return;
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path + "Info\\user.hs");
                XmlNode root = xmldoc.SelectSingleNode("UserInfo");
                // 如果设置程序不使用密码登录，则直接进入
                if (root.Attributes["usepassword"].Value != "True")
                {
                    frmShow fs = new frmShow(fm);
                    fs.Show();
                    load = true;
                    this.Close();
                }
                name = root.Attributes["username"].Value;
                password = root.Attributes["password"].Value;
            }
        }
        // 验证登录
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != name)
            {
                MessageBox.Show("您输入的用户名有误，请重新输入！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox1.Focus();
                this.textBox1.Text = "";
                return;
            }
            if (this.textBox2.Text != password)
            {
                MessageBox.Show("您输入的密码有误，请重新输入！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox2.Focus();
                this.textBox2.Text = "";
                return;
            }
            frmShow fs = new frmShow(fm);
            fs.Show();
            load = true;
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button1_Click(this.button1, EventArgs.Empty);
            }
        }
        // button1&button2共用的事件
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Red;
        }
        // button1&button2共用的事件
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.White;
        }
    }
}
