using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace _04.AddressBook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Person> people = new List<Person>();
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!Directory.Exists(path + "\\Address Book"))
            {
                Directory.CreateDirectory(path + "\\Address Book");
            }
            if(!File.Exists(path + "\\Address Book\\settings.xml"))
            {
                // File.Create(path + "\\Address Book\\settings.xml");
                XmlTextWriter xWriter = new XmlTextWriter(path + "\\Address Book\\settings.xml",Encoding.UTF8);
                xWriter.WriteStartElement("People");
                xWriter.WriteEndElement();
                xWriter.Close();
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\Address Book\\settings.xml");

            foreach (XmlNode xNode in xDoc.SelectNodes("People/Person"))
            {
                Person p = new Person();

                p.Name = xNode.SelectSingleNode("Name").InnerText;
                p.Email = xNode.SelectSingleNode("Email").InnerText;
                p.StreetAddress = xNode.SelectSingleNode("Address").InnerText;
                p.AdditionalNotes = xNode.SelectSingleNode("Notes").InnerText;
                p.Birthday = DateTime.FromFileTime(Convert.ToInt64(xNode.SelectSingleNode("Birthday").InnerText));
                people.Add(p);
                listView1.Items.Add(p.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Person p = new Person();

            p.Name = textBox1.Text;
            p.Email = textBox2.Text;
            p.StreetAddress = textBox3.Text;
            p.AdditionalNotes = textBox4.Text;
            p.Birthday= dateTimePicker1.Value;
            
            people.Add(p);
            listView1.Items.Add(p.Name);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
           textBox1.Text = people[listView1.SelectedItems[0].Index].Name;
           textBox2.Text = people[listView1.SelectedItems[0].Index].Email;
           textBox3.Text = people[listView1.SelectedItems[0].Index].StreetAddress;
           textBox4.Text = people[listView1.SelectedItems[0].Index].AdditionalNotes;
           dateTimePicker1.Value = people[listView1.SelectedItems[0].Index].Birthday;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Remove();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        void Remove()
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                people.RemoveAt(listView1.SelectedItems[0].Index);
                
            }
            catch  {  }
            
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            people[listView1.SelectedItems[0].Index].Name = textBox1.Text;
            people[listView1.SelectedItems[0].Index].Email = textBox2.Text ;
            people[listView1.SelectedItems[0].Index].StreetAddress= textBox3.Text;
            people[listView1.SelectedItems[0].Index].AdditionalNotes= textBox4.Text;
            people[listView1.SelectedItems[0].Index].Birthday = dateTimePicker1.Value;
            listView1.SelectedItems[0].Text = textBox1.Text;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xDoc.Load(path + "\\Address Book\\settings.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();

            foreach (var p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xName = xDoc.CreateElement("Name");
                XmlNode xEmail = xDoc.CreateElement("Email");
                XmlNode xAddress = xDoc.CreateElement("Address");
                XmlNode xNotes = xDoc.CreateElement("Notes");
                XmlNode xBirthday = xDoc.CreateElement("Birthday");

                xName.InnerText = p.Name;
                xEmail.InnerText = p.Email;
                xAddress.InnerText = p.StreetAddress;
                xNotes.InnerText = p.AdditionalNotes;
                xBirthday.InnerText = p.Birthday.ToFileTime().ToString();

                xTop.AppendChild(xName);
                xTop.AppendChild(xEmail);
                xTop.AppendChild(xAddress);
                xTop.AppendChild(xNotes);
                xTop.AppendChild(xBirthday);
                xDoc.DocumentElement.AppendChild(xTop);
               
            }

            xDoc.Save(path + "\\Address Book\\settings.xml");
        }

       
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }
    }

    class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime Birthday { get; set; }
    }
}
