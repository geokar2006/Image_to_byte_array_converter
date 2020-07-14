using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_to_byte_array_converter
{
    public partial class Form1 : Form
    {
        bool esl = false;
        string g = "";
        List<string> ga = new List<string>();
        bool esl2 = false;
        byte[] fileBytes = null;
        MemoryStream ms = new MemoryStream();
        private OpenFileDialog ofd;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form_openfile(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Картинки(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|Все файлы(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                FileStream stream = File.OpenRead(ofd.FileName);
                g = new SoapHexBinary(File.ReadAllBytes(ofd.FileName)).ToString();
                esl = true;
            }
        }


        private void Form_convert(object sender, EventArgs e)
        {
            if (esl)
            {
                int lg = g.Length;
                for (int i = 0; i < lg/2; ++i)
                {
                    char str1 = g[0];
                    char str2 = g[1];
                    string str3 = "0x"+str1 + str2;
                    ga.Add(str3);
                    g = g.Remove(0, 2);
                    Application.DoEvents();
                }
                int lg2 = ga.Count;
                for (int i = 0; i < lg2; ++i)
                {
                    if (i+1 != lg2)
                    {
                        ga[i] += ", ";
                    }
                    Application.DoEvents();
                }
                richTextBox1.Lines = ga.ToArray();
                richTextBox1.Text += "};";
                richTextBox1.Text = "byte[] data = new byte[]{" +richTextBox1.Text;
                MessageBox.Show("Файл конвертирован.");
                esl2 = true;
            }
            else
            {
                MessageBox.Show("Откройте Файл!");
            }
        }

        private void Form_Save(object sender, EventArgs e)
        {
            if (esl2)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Текстовые файлы(*.txt)|*.txt|Все файлы(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = sfd.FileName;
                richTextBox1.SaveFile(filename, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                MessageBox.Show("Файл сохранён.");
            }
            else
            {
                MessageBox.Show("Конвертируйте Файл!");
            }
        }

        private void Form_sbros(object sender, EventArgs e)
        {
            esl = false;
            esl2 = false;
            richTextBox1.Text = "";
            
        }

        private void avtor(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
