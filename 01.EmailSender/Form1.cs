using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
     

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBox4.Text.Contains("@gmail.com"))
                {
                    MessageBox.Show("You need provide an email @gmail.com");
                    return;
                }
                button1.Enabled = false;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(textBox4.Text);
                msg.Subject = textBox2.Text;
                msg.Body = textBox3.Text;

                foreach (string item in textBox1.Text.Split(';'))
                {
                    msg.To.Add(item);
                }
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(textBox4.Text, textBox5.Text);
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(msg);
                button1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("There was an error sending the message.Make sure you typed in credentials correctly and tat you have internet connection!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally { button1.Enabled = true; }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
