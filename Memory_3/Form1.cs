using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Memory_3
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Timer timer = new Timer { Interval = 1000 };
        string[,] data = new string[36, 2];
        string[] proverka = new string[36];
        string[] baza = new string[36];

        PictureBox firstClicked = null;
        PictureBox secondClicked = null;

        string firstClick = null;
        string secondClick = null;

        int ff = 0;
        int time = 60;

        List<string> icons = new List<string>()
        {
            "im1", "im1", "im2", "im2", "im3", "im3", "im4", "im4",
            "im5", "im5", "im6", "im6", "im7", "im7", "im8", "im8",
            "im9", "im9", "im10", "im10", "im11", "im11", "im12", "im12",
            "im13", "im13", "im14", "im14", "im15", "im15", "im16", "im16",
            "im17", "im17", "im18", "im18"
        };

        List<string> icons_reserv = new List<string>()
        {
            "im1", "im1", "im2", "im2", "im3", "im3", "im4", "im4",
            "im5", "im5", "im6", "im6", "im7", "im7", "im8", "im8",
            "im9", "im9", "im10", "im10", "im11", "im11", "im12", "im12",
            "im13", "im13", "im14", "im14", "im15", "im15", "im16", "im16",
            "im17", "im17", "im18", "im18"
        };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            startGameTimer();
            int n = 0;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                if (pictureBox != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    pictureBox.Image = Properties.Resources.question;
                    //pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(icons[randomNumber]);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    data[n, 0] = pictureBox.Name.ToString();
                    data[n, 1] = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                    n++;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            PictureBox pictureBox = sender as PictureBox;
            int ss = 0;

            if (pictureBox != null)
            {
                for(int i = 0;i < 36;i++)
                {
                    if(data[i, 0] == pictureBox.Name.ToString())
                    {
                        ss = i; break;
                    }
                }

                //if (pictureBox.Image == Properties.Resources.question)
                for (int i = 0; i < 36; i++)
                {
                    if (pictureBox.Name.ToString() == baza[i])
                    {
                        MessageBox.Show("Ты уже нажал");
                        return;
                    }
                }
                
                if (firstClicked == null)
                {
                    firstClicked = pictureBox;
                    firstClick = data[ss, 1];
                    ff = ss;
                    firstClicked.Image = (Image)Properties.Resources.ResourceManager.GetObject(data[ss, 1]);
                    return;
                }

                secondClicked = pictureBox;
                secondClick = data[ss, 1];
                secondClicked.Image = (Image)Properties.Resources.ResourceManager.GetObject(data[ss, 1]);

                

                if (firstClick == secondClick)
                {
                    proverka[ss] = secondClick;
                    proverka[ff] = firstClick;
                    baza[ss] = firstClicked.Name.ToString();
                    baza[ff] = secondClicked.Name.ToString();
                    firstClicked = null;
                    secondClicked = null;
                    firstClick = null;
                    secondClick = null;

                    //MessageBox.Show(ff.ToString() + " " + ss.ToString());
                    CheckForWinner();
                    return;
                }

                

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.Image = Properties.Resources.question;
            secondClicked.Image = Properties.Resources.question;

            firstClicked = null;
            secondClicked = null;

            firstClick = null;
            secondClick = null;
        }

        private void CheckForWinner()
        {
            int i = 0;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                
                if (pictureBox != null)
                {
                    //if (pictureBox.Image != (Image)Properties.Resources.ResourceManager.GetObject("question"))
                    //    return;
                    if(proverka[i] == null)
                        return;
                    
                }
                i++;
            }
            MessageBox.Show("Ты нашёл все картинки!", "Победа");
            //for (int j = 0; j < 36; j++)
            //{
            //    proverka[j] = null;
            //    baza[j] = null;
            //}
            Array.Clear(baza, 0, 36);
            Array.Clear(proverka, 0, 36);
            //Close();
            ResetImages();
        }

        private void ResetImages() //функция сброса графичиских полей после раунда
        {
            time = 60;
            startGameTimer();
            icons.Clear();
            icons.AddRange(icons_reserv);
            Array.Clear(baza,0,36);
            Array.Clear(proverka, 0, 36);
            int n = 0;
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                PictureBox pictureBox = control as PictureBox;
                if (pictureBox != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    pictureBox.Image = Properties.Resources.question;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    data[n, 0] = pictureBox.Name.ToString();
                    data[n, 1] = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                    n++;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //функция определения нажатия кнопки клавы
        {
            switch (keyData)
            {
                case Keys.W:
                    for(int i = 0; i < 36; i++)
                    {
                        proverka[i] = "123";
                    }

                    int ss = 0;
                    foreach (Control control in tableLayoutPanel1.Controls)
                    {
                        PictureBox pictureBox = control as PictureBox;
                        pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(data[ss, 1]);
                        ss++;
                    }
                    CheckForWinner();
                    //Application.Exit();
                    break;
                default:
                    break;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ResetImages();
            startGameTimer();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //timer2.Stop();
            //MessageBox.Show("Время вышло");
            //toolStripTextBox1.Text = "00:08";
        }

        private void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0)
                {
                    timer.Stop();
                    MessageBox.Show("Время вышло");
                    ResetImages();
                }

                var ssTime = TimeSpan.FromSeconds(time);
                toolStripTextBox1.Text = "00: " + time.ToString();
            };
        }
    }
}
