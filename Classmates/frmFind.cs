using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
namespace Classmates
{
    public partial class frmFind : Form
    {
        ArrayList al;
        frmMain fm;

        public frmFind(ArrayList al,frmMain fm)
        {
            InitializeComponent();
            this.al = al;
            this.fm = fm;
        }
        // 按姓名找
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请填写要找的姓名，可以是部分姓名！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ArrayList r = new ArrayList();
            for (int i = 0; i < al.Count;i++ )
            {
                CInfo c = (CInfo)al[i];
                if (c.name.Contains(this.textBox1.Text))
                {
                    SFind sf = new SFind();
                    sf.name = c.name;
                    sf.index = i;
                    r.Add(sf);
                }
            }
            if (r.Count>0)
            {
                frmRecord fr = new frmRecord(r, fm);
                this.Close();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("抱歉，没有找到名字包含 " +this.textBox1.Text + " 的相关信息","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        // 按住址查找
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox3.Text == "" || this.textBox3.Text == "∞")
            {
                MessageBox.Show("请填写要找的地址，例如：“聊城”！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ArrayList r = new ArrayList();
            for (int i = 0; i < al.Count; i++)
            {
                CInfo c = (CInfo)al[i];
                if (c.addr.Contains(this.textBox3.Text))
                {
                    SFind sf = new SFind();
                    sf.name = c.name;
                    sf.index = i;
                    r.Add(sf);
                }
            }
            if (r.Count > 0)
            {
                frmRecord fr = new frmRecord(r, fm);
                this.Close();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("抱歉，没有找到住址包含 " + this.textBox3.Text + " 的相关信息", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.button1_Click(button1, EventArgs.Empty);
            }
        }
        // 按性别查找
        private void button2_Click(object sender, EventArgs e)
        {
            string sex;
            if (this.radioButton1.Checked == true)
            {
                sex = "男";
            }
            else
            {
                sex = "女";
            }
            ArrayList r = new ArrayList();
            for (int i = 0; i < al.Count; i++)
            {
                CInfo c = (CInfo)al[i];
                if (c.sex == sex)
                {
                    SFind sf = new SFind();
                    sf.name = c.name;
                    sf.index = i;
                    r.Add(sf);
                }
            }
            if (r.Count > 0)
            {
                frmRecord fr = new frmRecord(r, fm);
                this.Close();
                fr.ShowDialog();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.button3_Click(button3, EventArgs.Empty);
            }
        }
    }
}
