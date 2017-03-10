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
namespace CPage
{
    // 用于保存一个同学的所有信息
    public struct CInfo
    {
        public string id;
        public string name;
        public string sex;
        public string qq;
        public string addr;
        public string email;
        public string tel;
        public string phone;
        public string bday;
        public string words;
        public string pic;
    }

    public partial class frmMain : Form
    {
        string path;
        bool exist = false;
        string id = "";
        string oldImage;

        public frmMain()
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
        }
        // 添加大头贴
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Image = Properties.Resources.NoPerson;
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.EndsWith("page"))
                {
                    exist = true;
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(file);
                    XmlNodeList persons = xmldoc.SelectSingleNode("Classmates").SelectNodes("person");
                    CInfo c = new CInfo();
                    foreach (XmlNode person in persons)
                    {
                        c.id = person.Attributes["id"].Value;
                        id = c.id;
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
                        oldImage = c.id + "." + c.pic;
                    }
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

                    if (File.Exists(path + c.id + "." + c.pic))
                    {
                        this.pictureBox1.Image = Image.FromFile(path + c.id + "." + c.pic);
                        this.pictureBox1.Tag = path + c.id + "." + c.pic;
                    }
                    else
                    {
                        // 加载默认头像
                        this.pictureBox1.Image = Properties.Resources.NoPerson;
                    }
                }
            }

        }
        /// <summary>
        /// 保存相关信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox4.Text == "")
            {
                MessageBox.Show("必须填写姓名！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CInfo c = new CInfo();
            // 将配置信息写入XML文档
            XmlDocument xmldoc = new XmlDocument();
            XmlNode dec = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");

            XmlElement root = xmldoc.CreateElement("Classmates");
            XmlElement person = xmldoc.CreateElement("person");
            if (exist == true)
            {
                c.id = id;
            }
            else
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
                if (this.pictureBox1.Tag.ToString() != path + oldImage)
                {
                    try
                    {
                        File.Copy(this.pictureBox1.Tag.ToString(), path + c.id + "." + c.pic);
                        File.Delete(path + oldImage);
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
            person.SetAttribute("id", c.id);
            person.SetAttribute("name", c.name);
            person.SetAttribute("sex", c.sex);
            person.SetAttribute("qq", c.qq);
            person.SetAttribute("addr", c.addr);
            person.SetAttribute("email", c.email);
            person.SetAttribute("tel", c.tel);
            person.SetAttribute("phone", c.phone);
            person.SetAttribute("bday", c.bday);
            person.SetAttribute("words", c.words);
            person.SetAttribute("pic", c.pic);
            root.AppendChild(person);
            xmldoc.AppendChild(dec);
            xmldoc.AppendChild(root);
            xmldoc.Save(Application.StartupPath + "\\" + c.id + ".page");
            MessageBox.Show("保存成功，到时候，您只需将本程序生成的两个文件复制给你同学就可以了\r\n单击确定将退出","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            System.Diagnostics.Process cmdrun = new System.Diagnostics.Process();
            cmdrun.StartInfo.FileName = "cmd.exe";
            cmdrun.StartInfo.UseShellExecute = false;
            cmdrun.StartInfo.CreateNoWindow = true;
            cmdrun.StartInfo.RedirectStandardInput = true;
            cmdrun.StartInfo.RedirectStandardOutput = true;
            cmdrun.Start();
            cmdrun.StandardInput.WriteLine(@"explorer /select, " + Application.StartupPath + "\\" + c.id + ".page");
            cmdrun.StandardInput.WriteLine("exit");
            cmdrun.Close();
            this.Close();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.button1_Click(button1, EventArgs.Empty);
            }
        }
    }
}
