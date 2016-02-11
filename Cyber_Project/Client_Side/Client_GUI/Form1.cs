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
using System.IO.Pipes;
using System.IO;

namespace Client_GUI
{
    public partial class Form1 : Form
    {
        delegate void Client_Activation_Delegate();
        private Button button1;

        public Form1()
        {
            InitializeComponent();

            this.Form_Design();

            //Thread Client = new Thread(new ThreadStart(Client_Activation));
            //Client.Start();

            textBox1.Text += "Insert Your User Name Here";
            textBox2.Text += "Insert Your Password Here";

            
        }

        public void Client_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\Cyber_Project\Client_Side\Client_GUI\bin\Debug\Python\Client.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.Start();

        }

         public void KBF_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\Cyber_Project\Client_Side\Client_GUI\bin\Debug\Python\Freeze_KeyBoard.py";
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
            button1 = new Button();
            button1.Width += 50;
            button1.Text = "Panic Button";
            button1.Location = new Point(220, 100);
            Controls.Add(button1);
            button1.Visible = false;
        }
        

        public void Pipe_Reader(NamedPipeServerStream server)
        {
            //int i = 0;

            var br = new BinaryReader(server);

            while (true)
            {
                try
                {
                    //i++;
                    var len = (int)br.ReadUInt32();
                    if (len == 0)
                        MessageBox.Show("len equals zero");

                    var str = new string(br.ReadChars(len));    // Read string
                    if (str == "OK")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                button1.Visible = true;
                            });
                            break;
                        }
                    }

                    else 
                    {

                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                KBF_Activation();

                            });
                            break;
                        }
                        
                    }


                    //if (i == 2)
                    //{
                    //    if (this.InvokeRequired)
                    //    {
                    //        this.Invoke((MethodInvoker)delegate
                    //        {
                    //            textBox3.Text += str;

                    //        });
                    //        break;
                    //    }

                    //}
                }

                catch
                {
                    //MessageBox.Show("Error");
                }


            }

            //Pipe_Writer(server);
        }



        public void Pipe_Writer()
        {
            // Open the named pipe.
            var server = new NamedPipeServerStream("ClientData");

            if (this.InvokeRequired)
            {
                Client_Activation_Delegate Engine_Activation = new Client_Activation_Delegate(this.Client_Activation);
                this.Invoke(Engine_Activation);
            }

            //Console.WriteLine("Waiting for connection...");
            server.WaitForConnection();

            string Data_To_Send = textBox1.Text + "," + textBox2.Text;

            //if (this.InvokeRequired)
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
                    

            //    });
            //}

            var bw = new BinaryWriter(server);
            var buf = Encoding.ASCII.GetBytes(Data_To_Send);                // Get ASCII byte array     
            bw.Write((uint)buf.Length);                                     // Write string length
            bw.Write(buf);

            Pipe_Reader(server);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Thread client = new Thread(new ThreadStart(Pipe_Writer));
            client.Start();
        }
    }
}
