using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Media;
using System.IO;
using System.Xml;
using System.Collections;
// 特别说明：∞表示信息为空
namespace Classmates
{
    public partial class frmMain : Form
    {
        private string path = Path.getPath();
        /// <summary>
        /// 同学列表，其中包含每一位同学的详细信息
        /// </summary>
        public ArrayList CList = new ArrayList();
        // 如果添加或修改同学信息changed将被置为TRUE，提示保存信息
        public bool changed = false;
        // 记录当前浏览的同学的位置，初始化为-1，表示没有开始浏览
        private int index = -1;
        // 最后保存同学录信息时，要检查文件的有效性，没用的图片会被删除，为了防止删除用户个人信息的头像，不得已引入的变量
        public string userimage="";

        Point[] rec = new Point[38];
        int x,y;
        Point pts;
        // 调用directsound的声明
        private DxVBLib.DirectX7 objdx = new DxVBLib.DirectX7();
        private DxVBLib.DirectSound objds;
        private DxVBLib.DirectSoundBuffer dsb;
        private DxVBLib.DSBUFFERDESC buf;
        private DxVBLib.WAVEFORMATEX wav;

        private void AddWav(string filename, bool loop)
        {
            objds = objdx.DirectSoundCreate("");
            objds.SetCooperativeLevel((int)this.Handle, DxVBLib.CONST_DSSCLFLAGS.DSSCL_NORMAL);
            buf.lFlags = DxVBLib.CONST_DSBCAPSFLAGS.DSBCAPS_STATIC;

            wav.nFormatTag = 1;
            wav.nChannels = 1;
            wav.lSamplesPerSec = 22050;
            wav.lAvgBytesPerSec = 44100;
            wav.nBlockAlign = 2;

            dsb = objds.CreateSoundBufferFromFile(filename, ref buf, out wav);
            if (loop == true)
            {
                dsb.Play(DxVBLib.CONST_DSBPLAYFLAGS.DSBPLAY_LOOPING);
            }
            else
            {
                dsb.Play(DxVBLib.CONST_DSBPLAYFLAGS.DSBPLAY_DEFAULT);
            }
        }
        /// <summary>
        /// 交换同学的信息，排序用
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void ExchangeCInfo(int a,int b)
        {
            object temp = CList[a];
            CList[a] = CList[b];
            CList[b] = temp;
            this.changed = true;
        }

        public frmMain()
        {
            InitializeComponent();

            // 可用主标题标签移动无标题栏窗口
            this.label1.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.label1.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.label1.MouseUp += new MouseEventHandler(Form1_MouseUp);
            // 添加音效
            this.pictureBox1.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label1.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label2.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label4.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label5.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label6.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label7.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label8.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label9.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label10.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label11.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);
            this.label12.MouseEnter += new EventHandler(this.linkLabel_MouseEnter);

            this.linkLabel12.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel11.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel10.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel8.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel7.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel6.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel5.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel4.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel3.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel2.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);
            this.linkLabel1.MouseEnter += new System.EventHandler(this.linkLabel_MouseEnter);

            this.linkLabel12.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel11.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel10.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel8.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel7.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel6.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel5.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel4.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel3.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel2.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);
            this.linkLabel1.MouseLeave += new System.EventHandler(this.linkLabel_MouseLeave);

            rec[0].X = 12;
            rec[0].Y = 10;
            rec[1].X = 460;
            rec[1].Y = 0;
            rec[2].X = 466;
            rec[2].Y = 18;
            rec[3].X = 465;
            rec[3].Y = 37;
            rec[4].X = 459;
            rec[4].Y = 44;
            rec[5].X = 459;
            rec[5].Y = 65;
            rec[6].X = 459;
            rec[6].Y = 101;
            rec[7].X = 458;
            rec[7].Y = 115;
            rec[8].X = 458;
            rec[8].Y = 137;
            rec[9].X = 458;
            rec[9].Y = 187;
            rec[10].X = 457;
            rec[10].Y = 191;
            rec[11].X = 458;
            rec[11].Y = 537;
            rec[12].X = 467;
            rec[12].Y = 539;
            rec[13].X = 470;
            rec[13].Y = 550;
            rec[14].X = 470;
            rec[14].Y = 576;
            rec[15].X = 465;
            rec[15].Y = 576;
            rec[16].X = 465;
            rec[16].Y = 590;
            rec[17].X = 461;
            rec[17].Y = 609;
            rec[18].X = 4;
            rec[18].Y = 585;
            rec[19].X = 1;
            rec[19].Y = 571;
            rec[20].X = 3;
            rec[20].Y = 559;
            rec[21].X = 3;
            rec[21].Y = 549;
            rec[22].X = 8;
            rec[22].Y = 539;
            rec[23].X = 15;
            rec[23].Y = 538;
            rec[24].X = 16;
            rec[24].Y = 506;
            rec[25].X = 15;
            rec[25].Y = 489;
            rec[26].X = 15;
            rec[26].Y = 387;
            rec[27].X = 16;
            rec[27].Y = 322;
            rec[28].X = 16;
            rec[28].Y = 173;
            rec[29].X = 17;
            rec[29].Y = 140;
            rec[30].X = 13;
            rec[30].Y = 119;
            rec[31].X = 14;
            rec[31].Y = 71;
            rec[32].X = 8;
            rec[32].Y = 70;
            rec[33].X = 3;
            rec[33].Y = 44;
            rec[34].X = 9;
            rec[34].Y = 25;
            rec[35].X = 2;
            rec[35].Y = 29;
            rec[36].X = 12;
            rec[36].Y = 26;
            rec[37].X = 4;
            rec[37].Y = 29;
        }
        /// <summary>
        /// 添加同学
        /// </summary>
        /// <param name="c">CInfo结构体的同学信息</param>
        public void AddC(CInfo c)
        {
            this.CList.Add(c);
            index = this.CList.Count - 1;
            LoadC(index);
        }
        /// <summary>
        /// 修改同学信息
        /// </summary>
        /// <param name="c">CInfo结构体的同学信息</param>
        /// <param name="index">同学录所在所有记录（CList）的索引</param>
        public void FixC(CInfo c,int index)
        {
            this.CList[index] = c;
            LoadC(index);
        }
        /// <summary>
        /// 读取一名同学的信息
        /// </summary>
        /// <param name="index">在所有同学记录（CList）中的索引</param>
        public void LoadC(int index)
        {
            CInfo c = (CInfo)this.CList[index];

            this.label2.Text = c.name + "同学";
            this.label3.Text = c.name;
            this.label13.Text = c.sex;
            this.label14.Text = c.qq;
            this.label15.Text = c.addr;
            this.label16.Text = c.email;
            this.label17.Text = c.tel;
            this.label18.Text = c.phone;
            this.label19.Text = c.bday;
            this.label20.Text = c.words;
            this.label12.Text = "TA给我的留言 ↓";
            if (c.pic != "")
            {         
                if (File.Exists(path + "Info\\" + c.id + "." + c.pic))
                {
                    this.pictureBox1.Image = Image.FromFile(path + "Info\\" + c.id + "." + c.pic);
                }
                else
                {
                    this.pictureBox1.Image = Properties.Resources.NoPerson;
                }
            }
            else
            {
                this.pictureBox1.Image = Properties.Resources.NoPerson;
            }
            // 此语句防止外部窗口调用,忘了修改index
            this.index = index;
        }
        // 加载用户信息
        public void SetUserInfo()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path + "Info\\user.hs");
            XmlNode root = xmldoc.SelectSingleNode("UserInfo");

            this.label1.Text = root.Attributes["txlname"].Value;
            this.label3.Text = root.Attributes["name"].Value;
            this.label2.Text = this.label3.Text + "的个人信息";
            this.label12.Text = "个性签名 ↓";
            this.label13.Text = root.Attributes["sex"].Value;

            this.label14.Text = root.Attributes["qq"].Value;
            this.label15.Text = root.Attributes["addr"].Value;
            this.label16.Text = root.Attributes["email"].Value;
            this.label17.Text = root.Attributes["tel"].Value;
            this.label18.Text = root.Attributes["phone"].Value;
            this.label19.Text = root.Attributes["bday"].Value;
            this.label20.Text = root.Attributes["words"].Value;
            if (File.Exists(path + "Info\\" + root.Attributes["pic"].Value))
            {
                this.pictureBox1.Image = Image.FromFile(path + "Info\\" + root.Attributes["pic"].Value);
                this.userimage = path + "Info\\" + root.Attributes["pic"].Value;
            }
            else
            {
                // 加载默认头像
                this.pictureBox1.Image = Properties.Resources.NoPerson;
                this.userimage = "";
            }
            index = -1;
        }
        // 加载音乐信息
        public void LoadMusic()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path + "Info\\user.hs");
            XmlNode root = xmldoc.SelectSingleNode("UserInfo");

            // 加载背景音乐
            if (File.Exists(path + "Info\\" + root.Attributes["music"].Value))
            {
                this.axWindowsMediaPlayer1.URL = path + "Info\\" + root.Attributes["music"].Value;
            }
        }
        // 播放点击声音
        private void PlayClickSound()
        {
            if (File.Exists(path + "MouseClick.wav"))
            {
                AddWav(path + "MouseClick.wav", false);
            }
        }
        // 播放翻页声音
        private void PlayPageSound()
        {
            if (File.Exists(path + "ChangePage.wav"))
            {
                AddWav(path + "ChangePage.wav", false);
            }
        }

        // 通过滚轮控制查看同学记录
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case Message.WM_MOUSEWHEEL:
                    if (m.WParam.ToInt32() > 0)   // 滚轮向上滚
                    {
                        Pre();
                    }
                    else
                    {
                        Next();
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 主要是供frmShow窗口调用，因为启动前的很多工作可能没完成
        /// </summary>
        public void LoadMainInfo()
        {
            // 导入已有的同学录信息
            if (File.Exists(path + "Info\\classmates.hs"))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path + "Info\\classmates.hs");
                XmlNodeList persons = xmldoc.SelectSingleNode("Classmates").SelectNodes("person");
                foreach (XmlNode person in persons)
                {
                    CInfo c = new CInfo();
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
                    if (!System.IO.File.Exists(Path.getPath()+"Info\\"+c.id+"."+c.pic))
                    {
                        c.pic = "";
                    }
                    CList.Add(c);
                }
            }
            else
            {
                MessageBox.Show("您的同学录里面还没有一位同学信息哦，可使用“添加同学”功能添加同学！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            SetUserInfo();
            LoadMusic();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(path + "Info"))
            {
                Directory.CreateDirectory(path + "Info");
                if (!File.Exists(path + "Info\\user.hs"))
                {
                    MessageBox.Show("这是您第一次使用本同学录，请将以下信息填写完整！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMainSet fms = new frmMainSet(true,this);
                    fms.Show();
                    return;
                }
            }
            else
            {
                if (!File.Exists(path + "Info\\user.hs"))
                {
                    MessageBox.Show("这是您第一次使用本同学录，请将以下信息填写完整！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMainSet fms = new frmMainSet(true,this);
                    fms.Show();
                    return;
                }
                else
                {
                    frmLogin fl = new frmLogin(this);
                    fl.Show();
                }
            }

        }
        // 创建不规则的窗体
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddPolygon(rec);
            this.Region = new System.Drawing.Region(gp);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pts = Cursor.Position;
                x = this.Left;
                y = this.Top;
                this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = Cursor.Position;
                this.Left = x + pt.X - pts.X;
                this.Top = y + pt.Y - pts.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // 移动窗口后，调整窗口位置，不让窗口越界,挺好玩的
            if (this.Left<0)
            {
                this.Left = 0;
            }
            if (this.Top <0)
            {
                this.Top = 0;
            }
            if (this.Left+this.Width>Screen.PrimaryScreen.Bounds.Width)
            {
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width;
            }
            if (this.Top + this.Height>Screen.PrimaryScreen.WorkingArea.Height)
            {
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            }
            this.Cursor = Cursors.Default;
        }
        // 退出
        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            this.Close();
        }
        // 鼠标进入播放声音
        private void linkLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                ((LinkLabel)sender).LinkColor = Color.Green;
            }
            catch (System.Exception ex)
            {
            	
            }
            if (File.Exists(path + "MouseEnter.wav"))
            {
                AddWav(path + "MouseEnter.wav", false);
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void linkLabel_MouseLeave(object sender, EventArgs e)
        {
            ((LinkLabel)sender).LinkColor = Color.FromArgb(0, 0, 255);
        }
        // 显示更改设置窗口
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Application.OpenForms["frmMainSet"].Close();
            }
            catch (System.Exception ex)
            {
            	
            }
            PlayClickSound();
            frmMainSet fms = new frmMainSet(false,this);
            fms.Show();
        }

        private void frmMain_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                this.axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
        // 首页
        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            SetUserInfo();
        }
        // 窗口退出时判断是否保存同学录信息
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed == true)
            {
                if (MessageBox.Show("同学录的信息已有改动，是否保存此改动？","操作提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmProcess fp = new frmProcess(SaveMode.SaveInfo,this,CList);
                    fp.ShowDialog();
                }
            }
        }
        // 添加同学
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            frmAdd fa = new frmAdd(true, new CInfo(), 0, this);
            fa.ShowDialog();
        }
        // 关于
        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            frmAbout fa = new frmAbout();
            fa.ShowDialog();
        }
        // 修改同学
        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            if (this.index == -1)
            {
                MessageBox.Show("有关用户的个人信息请使用“更改设置”功能修改","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                frmAdd fa = new frmAdd(false, (CInfo)CList[index], index, this);
                fa.ShowDialog();
            }
        }
        /// <summary>
        /// 浏览上一人
        /// </summary>
        private void Pre()
        {
            if (this.index == -1 && this.CList.Count >= 1)
            {
                PlayPageSound();
                LoadC(0);
                index = 0;
            }
            else
            {
                if (index - 1 >= 0)
                {

                    PlayPageSound();
                    LoadC(--index);
                }

            }
        }
        // 浏览上一人
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Pre();
        }
        /// <summary>
        /// 浏览下一人
        /// </summary>
        private void Next()
        {
            if (this.index == -1 && this.CList.Count >= 1)
            {
                PlayPageSound();
                LoadC(0);
                index = 0;
            }
            else
            {
                if (index + 1 < this.CList.Count)
                {
                    PlayPageSound();
                    LoadC(++index);
                }
            }
        }
        // 浏览下一人
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Next();
        }
        // 删除一名同学
        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            if (index != -1)
            {
                CInfo c = (CInfo)CList[index];
                string name = c.name;
                if (MessageBox.Show("确定要删除 " +name +" 同学的信息？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    changed = true;
                    CList.RemoveAt(index);
                    if (CList.Count == 0)
                    {
                        index = -1;
                        SetUserInfo();
                    }
                    else
                    {
                        if (index + 1 < this.CList.Count)
                        {
                            LoadC(++index);
                        }
                        else
                        {
                            LoadC(--index);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("哎呀呀，您是不能把自己的信息删除的，可以在“更改设置”中更改\r\n这里是删除一名同学的地方","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        // 查看所有同学记录
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            ArrayList al = new ArrayList();
            for (int i =0;i<CList.Count;i++)
            {
                SFind sf = new SFind();
                sf.name = ((CInfo)CList[i]).name;
                sf.index = i;
                al.Add(sf);
            }
            if (al.Count == 0)
            {
                MessageBox.Show("由于您现在没有一位同学的记录信息，所以不能查看所有记录","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                frmRecord fr = new frmRecord(al, this);
                fr.ShowDialog();
            }
        }
        // 查找同学
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            frmFind ff = new frmFind(CList, this);
            ff.Show(); 
        }
        // 批量操作
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PlayClickSound();
            if (this.CList.Count == 0)
            {
                MessageBox.Show("由于您现在没有一位同学的记录信息，所以不能查看所有记录","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                frmBat fb = new frmBat(this.CList, this);
                fb.ShowDialog();
            }
        }
    }
}
