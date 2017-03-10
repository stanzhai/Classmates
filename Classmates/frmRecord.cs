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
    public partial class frmRecord : Form
    {
        private ArrayList persons;
        private frmMain fm;
        private bool up = true;

        public frmRecord(ArrayList persons,frmMain fm)
        {
            InitializeComponent();
            this.persons = persons;
            this.fm = fm;
        }
        // 向上移动一名同学的位置
        private void MoveUp()
        {
            if (this.listBox1.SelectedItems.Count != 0)
            {
                int s = this.listBox1.SelectedIndex;
                if (s != 0)
                {
                    SFind sf = (SFind)persons[s];
                    SFind sf2 = (SFind)persons[s - 1];
                    fm.ExchangeCInfo(this.FindOriginalPos(sf.name), this.FindOriginalPos(sf2.name));
                    this.ExchangeInfo(s, s - 1);
                    this.ReLoad();
                    this.listBox1.SelectedIndex = s - 1;
                }
            }
            else
            {
                MessageBox.Show(this, "请先选择要操作的同学！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 向下移动一名同学的位置
        private void MoveDown()
        {
            if (this.listBox1.SelectedItems.Count != 0)
            {
                int s = this.listBox1.SelectedIndex;
                if (s != this.listBox1.Items.Count - 1)
                {
                    SFind sf = (SFind)persons[s];
                    SFind sf2 = (SFind)persons[s + 1];
                    fm.ExchangeCInfo(this.FindOriginalPos(sf.name), this.FindOriginalPos(sf2.name));
                    this.ExchangeInfo(s, s + 1);
                    this.ReLoad();
                    this.listBox1.SelectedIndex = s + 1;
                }
            }
            else
            {
                MessageBox.Show(this, "请先选择要操作的同学！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 从最初的CList同学列表中查找位置
        /// </summary>
        /// <param name="name">要查找的同学姓名</param>
        /// <returns>所在位置</returns>
        private int FindOriginalPos(string name)
        {
            for (int i = 0; i < fm.CList.Count;i++ )
            {
                if (((CInfo)fm.CList[i]).name == name)
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// 重新加载信息，排序用
        /// </summary>
        private void ReLoad()
        {
            this.listBox1.Items.Clear();
            for (int i = 0; i < persons.Count; i++)
            {
                SFind temp = new SFind();
                temp.name = ((SFind)persons[i]).name;
                temp.index = this.FindOriginalPos(temp.name);
                persons[i] = temp;
                this.listBox1.Items.Add(((SFind)persons[i]).name);
            }
        }
        /// <summary>
        /// 交换persons的信息，其中包括对应的CList的索引
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void ExchangeInfo(int a,int b)
        {
            object temp = persons[a];
            persons[a] = persons[b];
            persons[b] = temp;
        }
        // 双击查看同学信息
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SFind sf = (SFind)persons[this.listBox1.SelectedIndex];
            fm.LoadC(sf.index);
        }
        // 初始化
        private void frmRecord_Load(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            for (int i = 0; i < persons.Count;i++ )
            {
                this.listBox1.Items.Add(((SFind)persons[i]).name);
            }
            // 调整本窗口的位置
            this.Top = fm.Top + (fm.Height - this.Height) / 2;
            if (fm.Left + fm.Width + this.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                this.Left = fm.Left - this.Width;
            }
            else
            {
                this.Left = fm.Left + fm.Width;
            }
            this.listBox1.SelectedIndex = 0;
        }
        // 显示当前是第几位同学
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "相关记录： " + (this.listBox1.SelectedIndex+1).ToString() + "/" + this.listBox1.Items.Count.ToString();
            this.toolTip1.SetToolTip(this.button6, "查看 " + this.listBox1.Text + " 同学的信息\r\n也可以通过双击查看TA的信息");
            this.toolTip1.SetToolTip(this.listBox1, "双击，查看 " + this.listBox1.Text + " 同学的信息");
            this.toolTip1.SetToolTip(this.button1, "上移，调整 " + this.listBox1.Text + " 同学的位置");
            this.toolTip1.SetToolTip(this.button2, "下移，调整 " + this.listBox1.Text + " 同学的位置");
            this.toolTip1.SetToolTip(this.button7, "自动播放，从 " + this.listBox1.Text + " 同学开始向下以两秒的速度自动浏览");
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.listBox1.SelectedIndex >= 0 || this.listBox1.SelectedIndex<this.persons.Count)
                {
                    SFind sf = (SFind)persons[this.listBox1.SelectedIndex];
                    fm.LoadC(sf.index);
                }
            }
        }
        // 升序排列
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count>=2)
            {
                for (int i = 0; i < this.listBox1.Items.Count - 1;i++ )
                {
                    for (int j = 0; j < this.listBox1.Items.Count - 1 - i;j++ )
                    {
                        SFind sf = (SFind)persons[j];
                        SFind sf2 = (SFind)persons[j+1];
                        if (sf.name.CompareTo(sf2.name) > 0)
                        {
                            fm.ExchangeCInfo(this.FindOriginalPos(sf.name), this.FindOriginalPos(sf2.name));
                            this.ExchangeInfo(j, j + 1);
                        }
                    }
                }
                this.ReLoad();
                this.listBox1.SelectedIndex = 0;
            }
        }
        // 按姓名降序排列同学信息
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count >= 2)
            {
                for (int i = 0; i < this.listBox1.Items.Count - 1; i++)
                {
                    for (int j = 0; j < this.listBox1.Items.Count - 1 - i; j++)
                    {
                        SFind sf = (SFind)persons[j];
                        SFind sf2 = (SFind)persons[j + 1];
                        if (sf.name.CompareTo(sf2.name) < 0)
                        {
                            fm.ExchangeCInfo(this.FindOriginalPos(sf.name), this.FindOriginalPos(sf2.name));
                            this.ExchangeInfo(j, j + 1);
                        }
                    }
                }
                this.ReLoad();
                this.listBox1.SelectedIndex = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // 查看选中同学的信息
        private void button6_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0 || this.listBox1.SelectedIndex < this.persons.Count)
            {
                SFind sf = (SFind)persons[this.listBox1.SelectedIndex];
                fm.LoadC(sf.index);
            }
            else
            {
                MessageBox.Show(this, "请先选择要查看的同学！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 上移选中的同学,支持持续上移
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.up = true;
            this.timer2.Enabled = true;
        }
        // 下移选中的同学,支持持续下移
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            this.up = false;
            this.timer2.Enabled = true;
        }
        // 自动浏览
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count != 0)
            {            
                if (this.listBox1.SelectedItems.Count == 0)
                {
                    this.listBox1.SelectedIndex = 0;
                }
                int i = this.listBox1.SelectedIndex;
                if (i+1<this.listBox1.Items.Count)
                {
                    this.listBox1.SelectedIndex = i + 1;
                    SFind sf = (SFind)persons[this.listBox1.SelectedIndex];
                    fm.LoadC(sf.index);
                } 
                else
                {
                    this.timer1.Enabled = false;
                    this.button7.Text = "播";
                }
            }
            else
            {
                this.timer1.Enabled = false;
                this.button7.Text = "播";
            }
        }
        // 启动/停止自动播放
        private void button7_Click(object sender, EventArgs e)
        {
            // 先浏览选中的同学，在启动计时器自动播放
            if (this.listBox1.Items.Count != 0)
            {
                if (this.listBox1.SelectedItems.Count == 0)
                {
                    this.listBox1.SelectedIndex = 0;
                }
                SFind sf = (SFind)persons[this.listBox1.SelectedIndex];
                fm.LoadC(sf.index);
            }

            if (this.button7.Text == "播")
            {
                this.timer1.Enabled = true;
                this.button7.Text = "停";
            }
            else
            {
                this.timer1.Enabled = false;
                this.button7.Text = "播";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (up == true)
            {
                this.MoveUp();
            }
            else
            {
                this.MoveDown();
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            this.timer2.Enabled = false;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            this.timer2.Enabled = false;
        }
    }
}
