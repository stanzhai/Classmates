// 哎，不得不承认这些代码写的有点乱，不过已经将功能完美实现了
// 主要是很多东西不知道怎么去弄，经常性的该代码，弄得有点乱
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// 使用正则表达式
using System.Text.RegularExpressions;
namespace ImageManage
{
    public partial class Form1 : Form
    {
        private int moveX, moveY;  // 用于保存移动图像的数据
        private int phv, pvv;   // 用于记录移动图像前的位置
        private string filename;

        private Point m, sp;

        public Form1()
        {
            InitializeComponent();
        }
        // 截图，创建小图像
        private void CreateSmallImage()
        {
            Graphics g = this.pictureBox1.CreateGraphics();
            int sx = this.pictureBox3.Location.X - this.pictureBox1.Location.X;
            int sy = this.pictureBox3.Location.Y - this.pictureBox1.Location.Y;

            int width = this.pictureBox2.Size.Width;
            int height = this.pictureBox2.Size.Height;

            Rectangle r = new Rectangle(sx, sy, width, height);
            try
            {
                Bitmap bmp = new Bitmap(width, height);
                Graphics g3 = Graphics.FromImage(bmp);

                g3.DrawImage(this.pictureBox1.Image, 0, 0, r, GraphicsUnit.Pixel);

                this.pictureBox2.Image = new Bitmap(bmp, width, height);
                this.pictureBox3.Image = this.pictureBox2.Image;
            }
            catch (System.Exception ex)
            {

            }
        }
        // 加载完图像后，有可能要调整图像
        public void LoadImage(string filename)
        {
            this.filename = filename;
            Image imagetemp = Image.FromFile(filename);
            this.pictureBox1.Image = imagetemp;
            this.pictureBox3.Location = new Point(0, 0);
            this.button2_Click(button2, EventArgs.Empty);
            this.button5_Click(button5, EventArgs.Empty);
        }
        /// <summary>
        /// 实现图像的放大缩小
        /// </summary>
        /// <param name="s">源图像</param>
        /// <param name="filename">和源图像对应的文件名</param>
        /// <param name="mode">缩放参数“+”表示放大；“-”表示缩小</param>
        /// <returns></returns>
        private Bitmap stretchImage(Image s, string filename, string mode)
        {
            // 第二个参数是为了提高放大的效果，妈的，我太有才了！
            int multiple = 50;
            if (mode == "+")        // 参数+表示放大，-表示缩小
            {// 放大时，我忘了按比例放大了，真晕，不过现在我改正过来了
                if (s.Width + multiple <= 5000 || (s.Width + multiple) * s.Height / s.Width <= 5000)
                {
                    Bitmap bitmap = new Bitmap(Image.FromFile(filename), s.Width + multiple, (s.Width + multiple) * s.Height / s.Width);
                    return bitmap;
                }
                else
                {
                    return (Bitmap)s;
                }
            }
            else
            {
                if (s.Width - multiple >= 5 && (s.Width - multiple) * s.Height / s.Width >= 5)
                {
                    Bitmap bitmap = new Bitmap(Image.FromFile(filename), s.Width - multiple, (s.Width - multiple) * s.Height / s.Width);
                    return bitmap;
                }
                else
                {
                    return (Bitmap)s;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // 文件拖入，显示拖放效果
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }
        // 对拖放的文件进行处理
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string filename = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            Regex r = new Regex(@".+\.(bmp|jpg|jpeg|png|ico|gif)$");
            Match m = r.Match(filename);
            if (m.Success)
            {
                LoadImage(filename);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.panel1.HorizontalScroll.Visible == true || this.panel1.VerticalScroll.Visible == true)
            {
                this.pictureBox1.Cursor = Cursors.SizeAll;

                this.moveX = Cursor.Position.X;
                this.moveY = Cursor.Position.Y;
                this.phv = this.panel1.HorizontalScroll.Value;
                this.pvv = this.panel1.VerticalScroll.Value;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    this.panel1.HorizontalScroll.Value = phv + this.moveX - Cursor.Position.X;
                    this.panel1.VerticalScroll.Value = pvv + this.moveY - Cursor.Position.Y;
                }
                catch (System.Exception ex)
                {

                }
            }
        }
        // 放大图像
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.filename != null)
            {
                pictureBox1.Image = stretchImage(this.pictureBox1.Image, filename, "+");
                this.CreateSmallImage();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.filename != null)
            {
                pictureBox1.Image = stretchImage(this.pictureBox1.Image, filename, "-");
                this.CreateSmallImage();
            }


        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox1.Cursor = Cursors.Default;
        }
        // 
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            this.m = Cursor.Position;
            this.sp = this.pictureBox3.Location;
            this.pictureBox3.Cursor = Cursors.Hand;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pt = new Point(sp.X+Cursor.Position.X-m.X,sp.Y+Cursor.Position.Y-m.Y);
                this.pictureBox3.Location = pt;

                this.CreateSmallImage();
            }
        }
        // 保存文件
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.pictureBox2.Image != null)
            {
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    switch (this.saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            this.pictureBox2.Image.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                        case 2:
                            this.pictureBox2.Image.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 3:
                            this.pictureBox2.Image.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        default:
                            this.pictureBox2.Image.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }
        // 打开文件
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadImage(this.openFileDialog1.FileName);
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            this.pictureBox3.Cursor = Cursors.Default;
        }
    }
}
