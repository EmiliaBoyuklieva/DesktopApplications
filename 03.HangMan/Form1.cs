using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace _03.HangMan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string word = "";
        int amount = 0;
        List<Label> labels = new List<Label>();
        enum BodyPart
        {
            Head,
            Left_Eye,
            Right_Eye,
            Mouth,
            Left_Arm,
            Right_Arm,
            Body,
            Left_Leg,
            Right_Leg

        }
        void DrawHangPost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);

            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));

                    //Drawing body

            //DrawBodyPart(BodyPart.Head);
            //DrawBodyPart(BodyPart.Left_Eye);
            //DrawBodyPart(BodyPart.Right_Eye);
            //DrawBodyPart(BodyPart.Mouth);
            //DrawBodyPart(BodyPart.Left_Arm);
            //DrawBodyPart(BodyPart.Right_Arm);
            //DrawBodyPart(BodyPart.Body);
            //DrawBodyPart(BodyPart.Left_Leg);
            //DrawBodyPart(BodyPart.Right_Leg);
           
        }

        void DrawBodyPart(BodyPart bp)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Blue);

            if(bp == BodyPart.Head)
            {
                g.DrawEllipse(p,40, 50, 40, 40);
            }
            else if (bp == BodyPart.Left_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);

                g.FillEllipse(s,50, 60, 5, 5);
            }
            else if (bp == BodyPart.Right_Eye)
            {
                SolidBrush s = new SolidBrush(Color.Black);

                g.FillEllipse(s, 63, 60, 5, 5);
            }
            else if (bp == BodyPart.Mouth)
            {
                SolidBrush s = new SolidBrush(Color.Black);

                g.DrawArc(p, 50, 60, 20, 20,45,90);
            }
            else if (bp == BodyPart.Body)
            {
               
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            }
            else if (bp == BodyPart.Left_Arm)
            {

                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            }
            else if (bp == BodyPart.Right_Arm)
            {

                g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            }
            else if (bp == BodyPart.Left_Leg)
            {

                g.DrawLine(p, new Point(60, 170), new Point(30, 190));
            }
            else if (bp == BodyPart.Right_Leg)
            {

                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            }

        }

        void MakeLabel()
        {
            word=GetRandomWord();
            char[] chars = word.ToCharArray();
            int between = 330 / chars.Length ;

            for (int i = 0;  i < chars.Length; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * between) + 10, 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox2;
                labels[i].BringToFront();
                labels[i].CreateControl();
            }

            label1.Text = "Word Lenght: " + (chars.Length).ToString();
          
        }


        string GetRandomWord()
        {
            WebClient wc = new WebClient();
            string wordList = wc.DownloadString
            ("https://www.mit.edu/~ecprice/wordlist.10000");
            string[] words = wordList.Split('\n');
            Random random = new Random();
            return words[random.Next(0, words.Length)];
        }

        void ResetGame()
        {
            Graphics g = panel1.CreateGraphics();
            g.Clear(panel1.BackColor);
            GetRandomWord();
            MakeLabel();
            DrawHangPost();
            label2.Text = "Missed: ";
            textBox1.Text = "";
            amount = 0;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            DrawHangPost();
            MakeLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];

            if (!char.IsLetter(letter))
            {
                MessageBox.Show("You can submit only letters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (word.Contains(letter))
            {
                char[] letters = word.ToCharArray();

                for (int i = 0; i < letters.Length; i++)
                {
                    if(letters[i]== letter)
                    {
                        labels[i].Text = letter.ToString();
                    }
                }

                foreach (var item in labels)
                {
                    if (item.Text == "_")
                    {
                        return;
                    }
                    
                }
                MessageBox.Show("You have won!", "Congrats");
                ResetGame();

            }
            else
            {
                MessageBox.Show("The letter that you guessed isn't in the word", "Sorry");
                label2.Text += " " + letter.ToString() + ",";
                DrawBodyPart((BodyPart)amount);
                amount++;

                if (amount == 8)
                {
                    MessageBox.Show("Sorry but you lost!The word was " + word);
                    ResetGame();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text== word)
            {
                MessageBox.Show("You have won!", "Congrats");
                ResetGame();
            }
            else
            {
                MessageBox.Show("The word that you guessed is wrong!","Sorry");
                DrawBodyPart((BodyPart)amount);
                amount++;
                if (amount == 8)
                {
                    MessageBox.Show("Sorry but you lost!The word was " + word);
                    ResetGame();
                }
            }
        }
    }
}
