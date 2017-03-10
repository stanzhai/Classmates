using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml;
namespace Classmates
{
    public partial class frmAdd : Form
    {
        // 添加操作则为TRUE
        private bool add;
        private CInfo c;
        private int index;
        private frmMain fm;

        private string path;
        // 为了清理不用的大头贴&防止相同的大头贴出现,为文件名
        private string oldImage;

        public frmAdd(bool add,CInfo c,int index,frmMain fm)
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
            this.add = add;
            this.c = c;
            this.index = index;
            this.fm = fm;
            this.Icon = fm.Icon;
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            if (this.add == true)
            {
                this.Text = "添加同学信息";
                this.button1.Text = "添加";
                this.pictureBox1.Image = Properties.Resources.NoPerson;
            }
            else
            {
                this.Text = "修改同学信息";
                this.button1.Text = "修改";
                this.textBox4.Text = c.name;
                if (c.sex == "男")
                {
                    this.radioButton1.Checked = true;
                }
                else
                {
                    this.radioButton2.Checked = true;
                }
                if (c.qq == "∞")
                {
                    this.maskedTextBox1.Text = "";
                }
                else
                {
                    this.maskedTextBox1.Text = c.qq;
                }
                if (c.addr == "∞")
                {
                    this.textBox5.Text = "";
                }
                else
                {
                    this.textBox5.Text = c.addr;
                }
                if (c.email == "∞")
                {
                    this.textBox6.Text = "";
                }
                else
                {
                    this.textBox6.Text = c.email;
                }
                if (c.tel == "∞")
                {
                    this.maskedTextBox2.Text = "";
                }
                else
                {
                    this.maskedTextBox2.Text = c.tel;
                }
                if (c.phone == "∞")
                {
                    this.maskedTextBox3.Text = "";
                }
                else
                {
                    this.maskedTextBox3.Text = c.phone;
                }
                if (c.bday == "∞")
                {
                    this.maskedTextBox4.Text = "";
                }
                else
                {
                    this.maskedTextBox4.Text = c.bday;
                }
                if (c.words == "没有给我留言")
                {
                    this.textBox7.Text = "";
                }
                else
                {
                    this.textBox7.Text = c.words;
                }
                if (File.Exists(path + "Info\\" + c.id + "." + c.pic))
                {
                    this.pictureBox1.Image = Image.FromFile(path + "Info\\" + c.id + "." + c.pic);
                    this.pictureBox1.Tag = path + "Info\\" + c.id + "." + c.pic;
                    oldImage = c.id + "." + c.pic;
                }
                else
                {
                    // 加载默认头像
                    this.pictureBox1.Image = Properties.Resources.NoPerson;
                }
            }
        }
        // 选择图片
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "图像文件|*.jpg;*.jpeg;*.bmp;*.png;*.ico;*.gif|所有文件|*.*";
            this.openFileDialog1.Title = "选择大头贴";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 此功能主要针对老版同学录导入方便设计
                if (this.textBox4.Text == "")
                {
                    string filename = openFileDialog1.FileName.Split('\\')[openFileDialog1.FileName.Split('\\').Length - 1];
                    this.textBox4.Text = filename.Split('.')[0];
                }

                this.pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Tag = openFileDialog1.FileName;
            }
        }
        // 添加后修改同学
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox4.Text == "")
            {
                MessageBox.Show("必须填写姓名！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (add == true)
            {
                string newid = System.Guid.NewGuid().ToString().ToUpper();
                c.id = newid;
            }

            c.name = this.textBox4.Text;
            if (this.radioButton1.Checked == true)
            {
                c.sex = "男";
            }
            else
            {
                c.sex = "女";
            }
            c.qq = this.maskedTextBox1.Text;
            if (c.qq == "")
            {
                c.qq = "∞";
            }
            c.addr = this.textBox5.Text;
            if (c.addr == "")
            {
                c.addr = "∞";
            }
            c.email = this.textBox6.Text;
            if (c.email == "")
            {
                c.email = "∞";
            }
            if (this.maskedTextBox2.Text == "    -")
            {
                c.tel = "∞";
            }
            else
            {
                c.tel = this.maskedTextBox2.Text;
            }
            c.phone = this.maskedTextBox3.Text;
            if (c.phone == "")
            {
                c.phone = "∞";
            }
            if (this.maskedTextBox4.Text == "    年  月  日")
            {
                c.bday = "∞";
            }
            else
            {
                c.bday = this.maskedTextBox4.Text;
            }
            c.words = this.textBox7.Text;
            if (c.words == "")
            {
                c.words = "没有给我留言";
            }
            if (this.pictureBox1.Tag != null)
            {
                // pic只保存扩展名，图片的名字为id+扩展名，以防止重复
                c.pic = this.pictureBox1.Tag.ToString().Split('.')[this.pictureBox1.Tag.ToString().Split('.').Length - 1];
                // 复制头像到指定的文件夹
                string filename = this.pictureBox1.Tag.ToString().Split('\\')[this.pictureBox1.Tag.ToString().Split('\\').Length - 1];
                if (this.pictureBox1.Tag.ToString() != path + "Info\\" + oldImage)
                {
                    try
                    {
                        // 次两条语句的顺序一定不能颠倒
                        // 假如delete在前，遇到异常，将不会执行copy，这回导致读取图片的错误。
                        File.Copy(this.pictureBox1.Tag.ToString(), path + "Info\\" + c.id + "." + c.pic);
                        File.Delete(path + "Info\\" + oldImage);
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
            {
                // 空表示无头像
                c.pic = "";
            }
            // 将整合的信息添加或修改到同学列表中
            if (add == true)
            {
                fm.AddC(c);
            }
            else
            {
                fm.FixC(c, index);
            }
            fm.changed = true;
            this.Close();
        }
        // 批量导入
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Height = 430;
                string[] files = Directory.GetFiles(this.folderBrowserDialog1.SelectedPath);
                this.progressBar1.Maximum = files.Length;
                this.progressBar1.Value = 0;
                foreach (string file in files)
                {
                    Application.DoEvents();
                    this.label1.Text = "正在批量导入...";
                    this.progressBar1.Value++;
                    if (file.EndsWith("page"))
                    {
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(file);
                        XmlNodeList persons = xmldoc.SelectSingleNode("Classmates").SelectNodes("person");
                        CInfo c = new CInfo();
                        foreach (XmlNode person in persons)
                        {
                            c.id = person.Attributes["id"].Value;
                            c.name = person.Attributes["name"].Value;
                            c.phone = person.Attributes["phone"].Value;
                            c.pic = person.Attributes["pic"].Value;
                            c.qq = person.Attributes["qq"].Value;
                            c.sex = person.Attributes["sex"].Value;
                            c.tel = person.Attributes["tel"].Value;
                            c.words = person.Attributes["words"].Value;
                            c.addr = person.Attributes["addr"].Value;
                            c.bday = person.Attributes["bday"].Value;
                            c.email = person.Attributes["email"].Value;
                        }
                        // 效验是否有相同id的同学存在，存在则跳过
                        bool exist = false;
                        for (int i = 0; i < fm.CList.Count;i++ )
                        {
                            CInfo c2 = (CInfo)fm.CList[i];
                            if (c2.id == c.id)
                            {
                                exist = true;
                            }
                        }
                        if (exist == true)
                        {
                            continue;
                        }

                        if (c.pic != "")
                        {
                            try
                            {
                                System.IO.File.Copy(file.Split('.')[0] + "." + c.pic, path + "Info\\" + c.id + "." + c.pic);
                            }
                            catch (System.Exception ex)
                            {

                            }
                        }
                        fm.AddC(c);
                        fm.changed = true;
                    }
                }
                this.Close();
            }
        }
        // 多个文本框，共用这一个事件
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.button1_Click(button1, EventArgs.Empty);
            }
        }
    }
}
