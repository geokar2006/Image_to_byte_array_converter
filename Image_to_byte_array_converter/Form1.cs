using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_to_byte_array_converter
{
    public partial class Form1 : Form
    {
        bool esl = false;
        bool esl2 = false;
        byte[] imageArray = null;
        private OpenFileDialog ofd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form_openfile(object sender, MouseEventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Картинки(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG" +
            "|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                esl = true;
                imageArray = File.ReadAllBytes(ofd.FileName);
            }
        }


        private void Form_convert(object sender, MouseEventArgs e)
        {
            if (esl)
            {
                int lg = imageArray.Length;
                richTextBox1.Text += "byte[] data = new byte[]{";
                for (int i = 0; i < lg; ++i)
                {
                    if (i + 1 == lg)
                    {
                        if(imageArray[i] == 255)
                        {
                            richTextBox1.Text += "byte.MaxValue";
                        }
                        else
                        {
                            richTextBox1.Text += imageArray[i];
                        }
                        
                    }
                    else
                    {
                        if (imageArray[i] == 255)
                        {
                            richTextBox1.Text += "byte.MaxValue, ";
                        }
                        else
                        {
                            richTextBox1.Text += imageArray[i] + ", ";
                        }
                    }
                }
                richTextBox1.Text += "}";
                MessageBox.Show("Файл конвертирован");
                esl2 = true;
            }
            else
            {
                MessageBox.Show("Откройте Файл");
            }
        }

        private void Form_Save(object sender, MouseEventArgs e)
        {
            if (esl2)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = sfd.FileName;
                richTextBox1.SaveFile(filename, System.Windows.Forms.RichTextBoxStreamType.PlainText);
                MessageBox.Show("Файл сохранён");
            }
            else
            {
                MessageBox.Show("Конвертируйте Файл");
            }
        }

        private void Form_sbros(object sender, MouseEventArgs e)
        {
            esl = false;
            esl2 = false;
            richTextBox1.Text = "";
            
        }

        private void avtor(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Автор:\r\ngeoakr2006\r\nYoutube:\r\ngeokar2006");
        }
    }
}
