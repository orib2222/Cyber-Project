using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Client_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Form_Design();

            Thread Client = new Thread(new ThreadStart(Client_Activation));
            Client.Start();
        }

        public void Client_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\Ori\Desktop\Cyber_Project\Client_Side\Client_GUI\bin\Debug\Python\Client.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.Start();

        }

        public void Form_Design()
        {
            this.Text = "Client Software Window";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width += 300;

            this.DrawString();
            this.Panic_Button_Creation();
        }

        public void Panic_Button_Creation()
        {
            Button button1 = new Button();
            button1.Width += 50;
            button1.Text = "Panic Button";
            button1.Location = new Point(220, 100);
            Controls.Add(button1);
        }

        public void DrawString()
        {
            RichTextBox richTextBox1 = new RichTextBox();
            richTextBox1.Font = new Font("Consolas", 18f, FontStyle.Bold);
            richTextBox1.BackColor = Color.AliceBlue;

            string sentence = "    Welcome To Secure Computer Program";
            richTextBox1.AppendText(sentence);
            richTextBox1.SelectionBackColor = Color.AliceBlue;
            richTextBox1.AppendText(" ");
            richTextBox1.Width += 500;
            richTextBox1.Height = 40;
            Controls.Add(richTextBox1);
                
            
        }
    }
}
