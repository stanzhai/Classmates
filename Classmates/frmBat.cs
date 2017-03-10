using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Xml;
namespace Classmates
{
    public partial class frmBat : Form
    {
        private ArrayList person;
        private frmMain fm;

        public frmBat(ArrayList person,frmMain fm)
        {
            InitializeComponent();
            this.person = person;
            this.fm = fm;
            this.Icon = fm.Icon;
            this.imageList1.Images.Add(this.Icon);
        }
        /// <summary>
        /// 检查操作的有效性
        /// </summary>
        /// <returns>false表示失败</returns>
        private bool Check()
        {
            if (this.listView1.CheckedItems.Count == 0)
            {
                MessageBox.Show("要导出的项目个数为0，请选择要导出的同学！","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void frmBat_Load(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            for (int i = 0; i < person.Count;i++ )
            {
                CInfo ci = (CInfo)person[i];
                ListViewItem lvi = new ListViewItem(ci.name, 0);
                lvi.Checked = true;
                lvi.ToolTipText = ci.name + " 同学：\r\n性别：" + ci.sex + "\r\n留言：" + ci.words;
                this.listView1.Items.Add(lvi);
            }
        }
        // 导出信息
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.Check())
            {
                return;
            }
            else
            {
                ArrayList al = new ArrayList();
                for (int i = 0; i < this.listView1.CheckedItems.Count;i++ )
                {
                    int index = this.listView1.Items.IndexOf(this.listView1.CheckedItems[i]);
                    al.Add(this.person[index]);
                }
                frmProcess fp = new frmProcess(SaveMode.SaveOneInfo, this.fm, al);
                fp.ShowDialog();
            }
        }
        // 导出网页
        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.Check())
            {
                return;
            }
            else
            {
                ArrayList al = new ArrayList();
                for (int i = 0; i < this.listView1.CheckedItems.Count; i++)
                {
                    int index = this.listView1.Items.IndexOf(this.listView1.CheckedItems[i]);
                    al.Add(this.person[index]);
                }
                frmProcess fp = new frmProcess(SaveMode.SaveToPage, this.fm, al);
                fp.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.Check())
            {
                return;
            }
            else
            {
                this.saveFileDialog1.FileName = "同学录信息.csv";
                this.saveFileDialog1.Filter = "逗号分隔符文件|*.csv";
                this.saveFileDialog1.Title = "导出为表格";
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string r = "姓名,性别,QQ号,住址,邮箱,电话,手机,生日,留言\r\n";
                    for (int i = 0; i < this.listView1.CheckedItems.Count; i++)
                    {
                        int index = this.listView1.Items.IndexOf(this.listView1.CheckedItems[i]);
                        CInfo c = (CInfo)fm.CList[index];
                        string t = c.name + "," + c.sex + "," + c.qq + "," + c.addr + "," + c.email + "," + c.tel + "," + c.phone + "," + c.bday + "," + c.words + "\r\n";
                        t = t.Replace("∞", "");
                        t = t.Replace("没有给我留言", "");
                        r += t;
                    }
                    System.IO.File.WriteAllText(this.saveFileDialog1.FileName, r, Encoding.Default);
                }
            }

        }
    }
}
