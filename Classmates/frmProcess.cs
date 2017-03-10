using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace Classmates
{
    public partial class frmProcess : Form
    {
        private string rpath;
        private SaveMode sm;
        private frmMain fm;
        private ArrayList person;

        public frmProcess(SaveMode sm,frmMain fm,ArrayList person)
        {
            InitializeComponent();
            // 保证路径正确，根目录下和文件夹下得到的路径是不同的
            if (Application.StartupPath.EndsWith("\\"))
            {
                rpath = Application.StartupPath;
            }
            else
            {
                rpath = Application.StartupPath + "\\";
            }

            this.sm = sm;
            this.fm = fm;
            this.Icon = fm.Icon;
            this.person = person;
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            switch (sm)
            {
                case SaveMode.SaveInfo: 
                    this.Text = "正在保存同学录信息...";
                    break;
                case SaveMode.SaveOneInfo: 
                    this.Text = "正在导出同学录信息...";
                    break;
                case SaveMode.SaveToPage:
                    this.Text = "正在导出为网页信息...";
                    break;
                default:
                    break;
            }
        }
        // 开始保存同学录，或导出同学录
        private void frmProcess_Shown(object sender, EventArgs e)
        {
            XmlDocument xmldoc;
            XmlElement root;
            switch (sm)
            {
                case SaveMode.SaveInfo:
                    this.progressBar2.Maximum = 2;
                    this.progressBar2.Value = 0;
                    // 第一步，检查相关文件，删除无用的图片文件
                    Regex rg = new Regex(@".+\.(bmp|jpg|jpeg|png|ico|gif)$");
                    string[] files = System.IO.Directory.GetFiles(Application.StartupPath + "\\Info");
                    progressBar1.Maximum = files.Length;
                    progressBar1.Value = 0;
                    foreach (string file in files)
                    {
                        Application.DoEvents();
                        this.label1.Text = "正在排查无用图片文件...";
                        progressBar1.Value++;
                        // 用户个人信息的头像，跳出
                        if (file == fm.userimage)
                        {
                            continue;
                        }
                        Match m = rg.Match(file.ToLower());
                        if (m.Success)
                        {
                            string filename = file.Split('\\')[file.Split('\\').Length - 1];
                            bool del = true;
                            for (int i = 0; i < person.Count; i++)
                            {
                                CInfo c = (CInfo)person[i];
                                if (c.id + "." + c.pic == filename)
                                {
                                    del = false;
                                    break;
                                }
                            }
                            if (del == true)
                            {
                                try
                                {
                                    System.IO.File.Delete(file);
                                }
                                catch (System.Exception ex)
                                {

                                }
                            }
                        }
                    }
                    this.progressBar2.Value = 1;
                    // 第二步，保存相关信息
                    xmldoc = new XmlDocument();
                    XmlNode dec = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");

                    root = xmldoc.CreateElement("Classmates");
                    this.progressBar1.Maximum = person.Count - 1;
                    this.progressBar1.Value = 0;
                    for (int i = 0; i < person.Count; i++)
                    {
                        Application.DoEvents();
                        this.label1.Text = "正在保存同学信息...";
                        this.progressBar1.Value = i;
                        CInfo c = (CInfo)person[i];
                        XmlElement p = xmldoc.CreateElement("person");
                        p.SetAttribute("id", c.id);
                        p.SetAttribute("name", c.name);
                        p.SetAttribute("sex", c.sex);
                        p.SetAttribute("qq", c.qq);
                        p.SetAttribute("addr", c.addr);
                        p.SetAttribute("email", c.email);
                        p.SetAttribute("tel", c.tel);
                        p.SetAttribute("phone", c.phone);
                        p.SetAttribute("bday", c.bday);
                        p.SetAttribute("words", c.words);
                        p.SetAttribute("pic", c.pic);
                        root.AppendChild(p);
                    }
                    xmldoc.AppendChild(dec);
                    xmldoc.AppendChild(root);
                    xmldoc.Save(rpath + "Info\\classmates.hs");
                    this.progressBar2.Value = 2;
                    break;
                case SaveMode.SaveOneInfo:
                    this.folderBrowserDialog1.Description = "请选择要保存导出信息的目录";
                    if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string path;
                        if (this.folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                        {
                            path = this.folderBrowserDialog1.SelectedPath;
                        }
                        else
                        {
                            path = this.folderBrowserDialog1.SelectedPath + "\\";
                        }

                        this.label1.Text = "正在导出同学信息...";
                        this.progressBar1.Maximum = this.person.Count;
                        this.progressBar2.Maximum = this.person.Count;
                        this.progressBar1.Value = 0;
                        this.progressBar2.Value = 0;
                        for (int i = 0; i < this.person.Count; i++)
                        {
                            this.progressBar1.Value++;
                            this.progressBar2.Value++;

                            CInfo c = (CInfo)this.person[i];
                            xmldoc = new XmlDocument();
                            XmlNode xmldec = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                            root = xmldoc.CreateElement("Classmates");
                            XmlElement p = xmldoc.CreateElement("person");
                            p.SetAttribute("id", c.id);
                            p.SetAttribute("name", c.name);
                            p.SetAttribute("sex", c.sex);
                            p.SetAttribute("qq", c.qq);
                            p.SetAttribute("addr", c.addr);
                            p.SetAttribute("email", c.email);
                            p.SetAttribute("tel", c.tel);
                            p.SetAttribute("phone", c.phone);
                            p.SetAttribute("bday", c.bday);
                            p.SetAttribute("words", c.words);
                            p.SetAttribute("pic", c.pic);
                            root.AppendChild(p);
                            xmldoc.AppendChild(xmldec);
                            xmldoc.AppendChild(root);
                            xmldoc.Save(path + "\\" + c.id + ".page");

                            if (System.IO.File.Exists(Path.getPath() + "Info\\" + c.id + "." + c.pic))
                            {
                                System.IO.File.Copy(Path.getPath() + "Info\\" + c.id + "." + c.pic, path + c.id + "." + c.pic);
                            }
                        }
                    }
                    break;
                case SaveMode.SaveToPage:
                    this.folderBrowserDialog1.Description = "请选择要保存导出网页的目录";
                    if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        this.progressBar2.Maximum = 5;
                        string path;
                        if (this.folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                        {
                            path = this.folderBrowserDialog1.SelectedPath;
                        }
                        else
                        {
                            path = this.folderBrowserDialog1.SelectedPath + "\\";
                        }
                        // 1.生成相应的目录
                        Directory.CreateDirectory(path + "Image");
                        Directory.CreateDirectory(path + "Sound");
                        Directory.CreateDirectory(path + "ChildPages");
                        this.progressBar2.Value = 1;
                        // 2.生成所需文件
                        // 导出资源的图标
                        if (!File.Exists(path + "favicon.ico"))
                        {
                            System.IO.FileStream ico = new System.IO.FileStream(path + "favicon.ico", System.IO.FileMode.CreateNew);
                            Properties.Resources.favicon.Save(ico);
                            ico.Close();
                        }

                        xmldoc = new XmlDocument();
                        xmldoc.Load(rpath + "Info\\user.hs");
                        root = (XmlElement)xmldoc.SelectSingleNode("UserInfo");

                        string tp = this.textBox1.Text;
                        string title = root.Attributes["txlname"].Value;
                        string user = root.Attributes["name"].Value;
                        tp=tp.Replace("α", title);
                        tp=tp.Replace("β", root.Attributes["music"].Value);
                        tp=tp.Replace("γ", title);
                        tp=tp.Replace("δ", root.Attributes["name"].Value+"的个人信息");
                        tp=tp.Replace("ε", root.Attributes["name"].Value);
                        tp=tp.Replace("ζ", root.Attributes["sex"].Value);
                        tp=tp.Replace("η", root.Attributes["qq"].Value);
                        tp=tp.Replace("θ", root.Attributes["addr"].Value);
                        tp=tp.Replace("ι", root.Attributes["email"].Value);
                        tp=tp.Replace("κ", root.Attributes["tel"].Value);
                        tp=tp.Replace("μ", root.Attributes["bday"].Value);
                        tp=tp.Replace("ν", title);
                        tp=tp.Replace("ο", root.Attributes["name"].Value);
                        tp = tp.Replace("λ", root.Attributes["phone"].Value);
                        string head = root.Attributes["pic"].Value;
                        if (head == "")
                        {
                            head = "noperson.jpg";
                        }
                        tp=tp.Replace("ω", head);

                        string words = root.Attributes["words"].Value;
                        words =words.Replace("\r\n", "<br>");
                        tp=tp.Replace("ξ", words);
                        File.WriteAllText(path + "main.html", tp, Encoding.Unicode);
                        this.progressBar2.Value = 2;
                        // 循环生成列表页面
                        string r = "";
                        this.progressBar1.Maximum = person.Count;
                        this.progressBar1.Value = 0;
                        for (int i = 0; i < person.Count; i++)
                        {
                            Application.DoEvents();
                            this.label1.Text = "正在生成列表页面";
                            this.progressBar1.Value++;
                            CInfo c = (CInfo)person[i];
                            tp = textBox4.Text;
                            tp = tp.Replace("α", ((i%5)*150).ToString()+"px");
                            tp = tp.Replace("β", ((i / 5) * 170+60).ToString() + "px");
                            tp = tp.Replace("γ", c.id + ".html");
                            tp = tp.Replace("δ", c.name);
                            string head2 = c.pic;
                            if (head2 == "")
                            {
                                head2 = "noperson.jpg";
                            }
                            else
                            {
                                head2 = c.id + "." + c.pic;
                            }
                            tp = tp.Replace("ε", head2);
                            r = r + tp + "\r\n";
                        }
                        this.progressBar2.Value = 3;
                        tp = this.textBox3.Text;
                        tp = tp.Replace("α", user);
                        tp = tp.Replace("β", (((person.Count-1)/5)* 170+230).ToString());
                        tp = tp.Replace("γ", title);
                        tp = tp.Replace("δ", r);
                        tp = tp.Replace("ε", user);
                        File.WriteAllText(path + "list.html", tp, Encoding.Unicode);
                        // 循环生成子页面
                        this.progressBar1.Value = 0;
                        for (int i = 0; i < person.Count;i++ )
                        {
                            Application.DoEvents();
                            this.label1.Text = "正在生成子页面...";
                            this.progressBar1.Value++;
                            CInfo c = (CInfo)person[i];
                            tp = textBox2.Text;
                            tp = tp.Replace("α", c.name);
                            tp = tp.Replace("γ", title);
                            tp = tp.Replace("δ", c.name + "同学");
                            tp = tp.Replace("ε", c.name);
                            tp = tp.Replace("ζ", c.sex);
                            tp = tp.Replace("η", c.qq);
                            tp = tp.Replace("θ", c.addr);
                            tp = tp.Replace("ι", c.email);
                            tp = tp.Replace("κ", c.tel);
                            tp = tp.Replace("μ", c.bday);
                            tp = tp.Replace("ν", title);
                            tp = tp.Replace("ο", user);
                            tp = tp.Replace("λ", c.phone);
                            words = c.words;
                            words = words.Replace("\r\n", "<br>");
                            tp = tp.Replace("ξ", words);
                            string head2 = c.pic;
                            if (head2 == "")
                            {
                                head2 = "noperson.jpg";
                            }
                            else
                            {
                                head2 = c.id + "." + c.pic;
                                try
                                {
                                    File.Copy(rpath + "Info\\" + head2, path + "Image\\" + head2);
                                }
                                catch (System.Exception ex)
                                {
                                	
                                }
                            }
                            tp = tp.Replace("ω", head2);
                            File.WriteAllText(path + "ChildPages\\" + c.id + ".html",tp,Encoding.Unicode);
                        }
                        this.progressBar2.Value = 4;
                        fm.BackgroundImage.Save(path + "Image\\main.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Properties.Resources.NoPerson.Save(path + "Image\\noperson.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        // 3.复制文件
                        string music = root.Attributes["music"].Value;
                        if (music != "")
                        {
                            try
                            {
                                File.Copy(rpath+"Info\\"+music,path+"Sound\\"+music);
                            }
                            catch (System.Exception ex)
                            {
                            	
                            }
                        }
                        if (head != "")
                        {
                            try
                            {
                                File.Copy(rpath + "Info\\" + head, path + "Image\\" + head);
                            }
                            catch (System.Exception ex)
                            {

                            }

                        }
                        this.progressBar2.Value = 5;
                        System.Diagnostics.Process.Start(path + "main.html");
                    }
                    break;
                default:
                    break;
            }
            this.Close();
        }
    }
}
