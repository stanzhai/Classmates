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
    public partial class frmMainSet : Form
    {
        private string path;
        private bool first;
        private bool load = false;
        private string oldImage, oldMusic;
        private frmMain fm;

        public frmMainSet(bool first,frmMain fm)
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
            this.first = first;
            this.fm = fm;
            this.Icon = fm.Icon;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = this.textBox1.Text + "的同学录";
        }

        private void frmMainSet_Load(object sender, EventArgs e)
        {
            // 用户配置信息文件存在的话，加载原有信息
            if (File.Exists(path + "Info\\user.hs"))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path + "Info\\user.hs");
                XmlNode root = xmldoc.SelectSingleNode("UserInfo");

                if (root.Attributes["usepassword"].Value != "True")
                {
                    this.checkBox1.Checked = false;
                    this.label1.ForeColor = Color.Black;
                    this.label2.ForeColor = Color.Black;
                }
                this.textBox1.Text = root.Attributes["username"].Value;
                this.textBox2.Text = root.Attributes["password"].Value;
                this.textBox3.Text = root.Attributes["txlname"].Value;
                this.textBox4.Text = root.Attributes["name"].Value;
                if (root.Attributes["sex"].Value == "男")
                {
                    this.radioButton1.Checked = true;
                }
                else
                {
                    this.radioButton2.Checked = true;
                }
                if (root.Attributes["qq"].Value == "∞")
                {
                    this.maskedTextBox1.Text = "";
                }
                else
                {
                    this.maskedTextBox1.Text = root.Attributes["qq"].Value;
                }
                if (root.Attributes["addr"].Value == "∞")
                {
                    this.textBox5.Text = "";
                }
                else
                {
                    this.textBox5.Text = root.Attributes["addr"].Value;
                }
                if (root.Attributes["email"].Value == "∞")
                {
                    this.textBox6.Text = "";
                }
                else
                {
                    this.textBox6.Text = root.Attributes["email"].Value;
                }
                if (root.Attributes["tel"].Value == "∞")
                {
                    this.maskedTextBox2.Text = "";
                }
                else
                {
                    this.maskedTextBox2.Text = root.Attributes["tel"].Value;
                }
                if (root.Attributes["phone"].Value == "∞")
                {
                    this.maskedTextBox3.Text = "";
                }
                else
                {
                    this.maskedTextBox3.Text = root.Attributes["phone"].Value;
                }
                if (root.Attributes["bday"].Value == "∞")
                {
                    this.maskedTextBox4.Text = "";
                }
                else
                {
                    this.maskedTextBox4.Text = root.Attributes["bday"].Value;
                }
                if (root.Attributes["words"].Value == "∞")
                {
                    this.textBox7.Text = "";
                }
                else
                {
                    this.textBox7.Text = root.Attributes["words"].Value;
                }
                this.pictureBox1.Tag = path + "Info\\" + root.Attributes["pic"].Value;
                oldImage = root.Attributes["pic"].Value;
                if (File.Exists(this.pictureBox1.Tag.ToString()))
                {
                    this.pictureBox1.Image = Image.FromFile((string)this.pictureBox1.Tag);
                }
                else
                {
                    this.pictureBox1.Image = Properties.Resources.NoPerson;
                }
                if (File.Exists(path + "Info\\" + root.Attributes["music"].Value))
                {
                    this.textBox8.Text = path + "Info\\" + root.Attributes["music"].Value;
                }
                oldMusic = root.Attributes["music"].Value;
            }
            else
            {
                this.pictureBox1.Image = Properties.Resources.NoPerson;
            }
        }
        // 保存设置信息
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("必须填写用户名，空格无效！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("必须填写登录密码，空格无效！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            } 
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("必须填写同学录名称，空格无效！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("必须填写姓名，空格无效！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 将配置信息写入XML文档
            XmlDocument xmldoc = new XmlDocument();
            XmlNode dec = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");

            XmlElement root = xmldoc.CreateElement("UserInfo");
            root.SetAttribute("usepassword", this.checkBox1.Checked.ToString());
            root.SetAttribute("username", this.textBox1.Text);
            root.SetAttribute("password", this.textBox2.Text);
            root.SetAttribute("txlname", this.textBox3.Text);
            root.SetAttribute("name", this.textBox4.Text);
            if (this.radioButton1.Checked == true)
            {
                root.SetAttribute("sex", "男");
            }
            else
            {
                root.SetAttribute("sex", "女");
            }
            if (this.maskedTextBox1.Text == "")
            {
                root.SetAttribute("qq", "∞");
            } 
            else
            {
                root.SetAttribute("qq", this.maskedTextBox1.Text);
            }
            if (this.textBox5.Text == "")
            {
                root.SetAttribute("addr", "∞");
            }
            else
            {
                root.SetAttribute("addr", this.textBox5.Text);
            }
            if (this.textBox6.Text == "")
            {
                root.SetAttribute("email", "∞");
            } 
            else
            {
                root.SetAttribute("email", this.textBox6.Text);
            }
            if (this.maskedTextBox2.Text == "    -")
            {
                root.SetAttribute("tel", "∞");
            } 
            else
            {
                root.SetAttribute("tel", this.maskedTextBox2.Text);
            }
            if (this.maskedTextBox3.Text == "")
            {
                root.SetAttribute("phone", "∞");
            } 
            else
            {
                root.SetAttribute("phone", this.maskedTextBox3.Text);
            }
            if (this.maskedTextBox4.Text == "    年  月  日")
            {
                root.SetAttribute("bday", "∞");
            } 
            else
            {
                root.SetAttribute("bday", this.maskedTextBox4.Text);
            }
            if (this.textBox7.Text == "")
            {
                root.SetAttribute("words", "∞");
            } 
            else
            {            
                root.SetAttribute("words", this.textBox7.Text);
            }
            if (this.pictureBox1.Tag == null)
            {
                root.SetAttribute("pic", "");
            }
            else
            {
                string filename = this.pictureBox1.Tag.ToString().Split('\\')[this.pictureBox1.Tag.ToString().Split('\\').Length -1];
                if (this.pictureBox1.Tag.ToString() != path + "Info\\" + oldImage)
                {
                    try
                    {
                        File.Copy(this.pictureBox1.Tag.ToString(), path + "Info\\" + filename);
                        File.Delete(path + "Info\\" + oldImage);
                    }
                    catch (System.Exception ex)
                    {
                    	
                    }
                }
                root.SetAttribute("pic", filename);
            }
            string filename2 = this.textBox8.Text.Split('\\')[this.textBox8.Text.Split('\\').Length - 1];
            if (this.textBox8.Text != path + "Info\\" + oldMusic)
            {
                try
                {
                    File.Copy(this.textBox8.Text, path + "Info\\" + filename2);
                    File.Delete(path + "Info\\" + oldMusic);
                }
                catch (System.Exception ex)
                {
                	
                }
            }
            root.SetAttribute("music", filename2);

            xmldoc.AppendChild(dec);
            xmldoc.AppendChild(root);
            xmldoc.Save(path + "Info\\user.hs");
            if (first == true)
            {
                frmShow fs = new frmShow(fm);
                fs.Show();
                load = true;
                this.Close();
            }
            else
            {
                load = true;
                fm.SetUserInfo();
                fm.LoadMusic();
                this.Close();
            }
        }
        // 选择大头贴
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "图像文件|*.jpg;*.jpeg;*.bmp;*.png;*.ico;*.gif|所有文件|*.*";
            this.openFileDialog1.Title = "选择大头贴";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Tag = openFileDialog1.FileName;
            }
        }
        // 选择背景音乐
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "音频文件|*.mp3;*.wma;*.mid|所有文件|*.*";
            this.openFileDialog1.Title = "选择背景音乐";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox8.Text = this.openFileDialog1.FileName;
            }
        }

        private void frmMainSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.load == false && this.first == true)
            {
                Application.Exit();
            }
        }
        // 复选状态改变时调整颜色标记
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.label1.ForeColor = Color.Red;
                this.label2.ForeColor = Color.Red;
            } 
            else
            {
                this.label1.ForeColor = Color.Black;
                this.label2.ForeColor = Color.Black;
            }
        }
        // 共用这一个事件
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.button1_Click(button1, EventArgs.Empty);
            }
        }
    }
}
