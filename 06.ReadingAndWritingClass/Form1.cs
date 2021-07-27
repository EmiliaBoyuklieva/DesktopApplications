using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _06.ReadingAndWritingClass;

namespace _06.ReadingAndWritingClass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

                        // Пример за Writer

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Writer w = new Writer(ofd.FileName);
                w.ChangeByteOrder(IO.ByteOrder.LittleEndian);
                w.WriteUnicodeString("Emi");
                w.Close();
               
            }


                         // Пример за Reader

            //OpenFileDialog ofd = new OpenFileDialog();
            //if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    Reader r = new Reader(ofd.FileName);
            //    MessageBox.Show(r.ReadInt64().ToString("X"));
            //}
        }
    }
}
