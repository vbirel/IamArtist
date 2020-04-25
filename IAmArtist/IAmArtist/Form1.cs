using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAmArtist
{
    public partial class FormMain : Form
    {
        public PictureBox picBox;
      
       string [] picName={"атом","какойто_толстый_муж","король","пустыный_котя","солнышко","страные_телепузики","то_что_у_меня-на_аве","хэт_кид"};

        string standarSave= Application.StartupPath;
        public Graphics grMain, grStels, grPic, grOld;
        bool logg = false, chenged=false;
        int x0, y0, x, y, w=0, h=0, penW=1, mod=0;
        public Bitmap bitMain, stels;
        Pen pen;
        Color fon = Color.White, penCol = Color.Black;
        Label saize = new Label();
        


        public FormMain()
        {
            InitializeComponent();
            Timer timer = new Timer() { Interval = 1000 };
            timer.Tick += timerTick;
            timer.Start();

            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
           
            TrackBar saizTrack = new TrackBar();
            saizTrack.Minimum = 1;
            saizTrack.Maximum = 10;
            saizTrack.Scroll += saizTrackScroll;
            flowLayoutMenu.Controls.Add(saizTrack);
            saize.Text ="толщина линии: "+ saizTrack.Value.ToString();
            
            flowLayoutMenu.Controls.Add(saize);

            CreateMenu();//метод создания меню
            filing();
            toolStripComboBoxImage.SelectedIndex = 0;
            toolStripComboBoxImage.DropDownStyle = ComboBoxStyle.DropDownList;
            picBox.Visible = false;



            GrapicsMain();

         
            pen = new Pen(penCol, penW);
            pictureBoxMain.Invalidate();
        }
        public void CreateMenu()//создание меню
        {
            RadioButton[] buttonMenu = new RadioButton[10];
            for (int i = 0; i < buttonMenu.Length; i++)
            {
                buttonMenu[i] = new RadioButton();
                buttonMenu[i].Appearance = Appearance.Button;
                buttonMenu[i].BackColor = Color.White;
                flowLayoutMenu.Controls.Add(buttonMenu[i]);
            }
           
            buttonMenu[0].Text = "карондаш";
            buttonMenu[1].Text = "линия";
            buttonMenu[2].Text = "рамка";
            buttonMenu[3].Text = "овал";
            buttonMenu[4].Text = "ломаная";
            buttonMenu[5].Text = "Безье?";
            buttonMenu[6].Text = "закрашеный";
            buttonMenu[7].Text = "стерка";
            buttonMenu[8].Text = "текст";
            buttonMenu[9].Text = "картинка";
           
            buttonMenu[0].CheckedChanged += buttonMenu0_Click;
            buttonMenu[1].CheckedChanged += buttonMenu1_Click;
            buttonMenu[2].CheckedChanged += buttonMenu2_Click;
            buttonMenu[3].CheckedChanged += buttonMenu3_Click;
            buttonMenu[4].CheckedChanged += buttonMenu4_Click;
            buttonMenu[5].CheckedChanged += buttonMenu5_Click;
            buttonMenu[6].CheckedChanged += buttonMenu6_Click;
            buttonMenu[7].CheckedChanged += buttonMenu7_Click;
            buttonMenu[8].CheckedChanged += buttonMenu8_Click;
            buttonMenu[9].CheckedChanged += buttonMenu9_Click;

            buttonMenu[0].Checked = true;

            picBox = new PictureBox();
            picBox.BackColor = Color.Transparent;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            flowLayoutMenu.Controls.Add(picBox);
            
        }

        public void GrapicsMain()//подготовка к графике
        {

            w = SystemInformation.VirtualScreen.Width;
            h = SystemInformation.VirtualScreen.Height;
            bitMain = new Bitmap(w, h);
            grPic = pictureBoxMain.CreateGraphics();
            grMain = Graphics.FromImage(bitMain);
            grOld = pictureBoxMain.CreateGraphics();
            grMain.Clear(fon);
            pictureBoxMain.Image = bitMain;
            pictureBoxMain.Invalidate();
            stels = new Bitmap(w, h);
            grStels = Graphics.FromImage(stels);
            grStels.Clear(fon);
           
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)//загруска фона
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                grMain.DrawImage(Image.FromFile(openFileDialog1.FileName), 0, 0);
                pictureBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxMain.Invalidate();
               
            }
        }

        public void cutImeg()
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Size(pictureBoxMain.Width, pictureBoxMain.Height));

            Bitmap CuttedImage = CutImage(bitMain, rect);
            CuttedImage.SetResolution(pictureBoxMain.Width, pictureBoxMain.Height);
            pictureBoxMain.Image = CuttedImage;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)// сохранение иображения
        {
            if (pictureBoxMain.Image == null)
                return;

            cutImeg();
            saveFileDialog1.Filter= "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.CheckPathExists = true;
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                try
                {

                    pictureBoxMain.Image.Save(saveFileDialog1.FileName);
                    standarSave = saveFileDialog1.FileName;
                    chenged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public Bitmap CutImage(Bitmap bitMain, Rectangle rect)//обрезка
        {

            Bitmap bmp = new Bitmap(bitMain.Width, bitMain.Height); 

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(bitMain, 0, 0, rect, GraphicsUnit.Pixel);

            return bmp;
        }

        private void buttonMenu0_Click(object sender, EventArgs e)// для карандаша
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "карондаш";
                mod = 0;
            }

        }

        private void saizTrackScroll(object sender, EventArgs e)//yfcnhjqrf njkotys
        {
            TrackBar saizTrack = (TrackBar)sender;
            pen.Width = saizTrack.Value;
            saize.Text = "толщина линии: " + saizTrack.Value.ToString();
            
        }

        private void buttonMenu1_Click(object sender, EventArgs e)// для линии
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "линия";
                mod = 1;
            }
        }

        private void buttonMenu2_Click(object sender, EventArgs e)// для рамки
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "рамка";
                mod = 2;
            }
        }

        private void buttonMenu3_Click(object sender, EventArgs e)// для овала
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "овал";
                mod = 3;
            }
        }

        private void pictureBoxMain_MouseDown(object sender, MouseEventArgs e)//нажатие на поля для рисование
        {
            logg = true;
            chenged = true;
            x0 = e.X;
            y0 = e.Y;
        }

        private void pictureBoxMain_MouseUp(object sender, MouseEventArgs e)//jngecrfybt vsib
        {
            logg = false;

            grMain.DrawImage(stels, 0, 0);
        }

        public void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutImeg();
            pictureBoxMain.Image.Save(standarSave);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//выход
        {
            if(chenged)
            {
                DialogResult result =  MessageBox.Show("сохранить изменения?",
                    "сохранение",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                   );
                if(result==DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                    Environment.Exit(0);
                }
                else if (result==DialogResult.No)
                {
                    Environment.Exit(0);
                }else
                {
                    return;
                }
            }else
                Environment.Exit(0);
        }

        //private void FormMain_ResizeEnd(object sender, EventArgs e)
        //{
        //   // grOld.DrawImage(pictureBoxMain.Image, 0, 0);
        //}

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitMain, 0, 0);
            if (logg)
                e.Graphics.DrawImage(stels, 0, 0);
        }

        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)//рисование в памяти
        {
            toolStripStatusXorY.Text = "кардинаты X: " + e.X + "Y: " + e.Y;
            if (logg)
            {
                x = e.X;
                y = e.Y;
                grStels.Clear(Color.FromArgb(0, 0, 0, 0));

                if(mod==0)
                {
                    grMain.DrawLine(pen, x0, y0, x, y);
                    x0 = x;
                    y0 = y;
                }else if(mod==1)
                {
                    grStels.DrawLine(pen, x0, y0, x, y);
                }
                pictureBoxMain.Invalidate();
            }
            pictureBoxMain.Refresh();
        }

        private void buttonMenu4_Click(object sender, EventArgs e)// для ломаной
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "ломаная";
                mod = 4;
            }
        }

        private void buttonMenu5_Click(object sender, EventArgs e)// для чиво?
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "безье";
                mod = 5;
            }
        }

        private void buttonMenu6_Click(object sender, EventArgs e)// для закрашеный
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "закрашеный";
                mod = 6;
            }
        }

        private void buttonMenu7_Click(object sender, EventArgs e)// для стрелки
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "стрелка";
                mod = 7;
            }
        }

        private void buttonMenu8_Click(object sender, EventArgs e)// для текста
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "текст";
                toolStripTextBoxText.Visible = true;
                mod = 8;
            }
            else
            {
                toolStripTextBoxText.Visible = false;
            }
        }

        private void buttonMenu9_Click(object sender, EventArgs e)// для картинки
        {
            RadioButton buttonMenu = (RadioButton)sender;

            if (buttonMenu.Checked)
            {
                toolStripStatusText.Text = "рисунок";
                toolStripComboBoxImage.Visible = true;
                picBox.Image = Image.FromFile("img/" + toolStripComboBoxImage.Text + ".png");
                picBox.Visible = true;
                mod = 9;
            }
            else
            {
                toolStripComboBoxImage.Visible = false;
                picBox.Visible = false;
            }
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)//отчистка
        {
            grMain.Clear(fon);
            pictureBoxMain.Invalidate();
        }

        private void toolStripButtonColor_Click(object sender, EventArgs e)//выбор цвета
        {
            
            if(colorDialog1.ShowDialog()==DialogResult.OK)
            {
                penCol = colorDialog1.Color;
                pen.Color = penCol;
            }
        }

        public void filing()//заполнение комбобокса
        {
            for(int i=0; i< picName.Length; i++)
            {
                toolStripComboBoxImage.Items.Add(picName[i]);
            }
        }        

        private void timerTick(object sender, EventArgs e)//отображение даты
        {
            toolStripStatusDate.Text = DateTime.Now.ToLongDateString();
            toolStripStatusTame.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolStripComboBoxImage_SelectedIndexChanged(object sender, EventArgs e)//изменение картинки с изменением комбобокса
        {
            picBox.Image = Image.FromFile("img/" + toolStripComboBoxImage.Text + ".png");
        }
    }
}
