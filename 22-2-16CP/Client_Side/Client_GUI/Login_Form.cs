using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Client_GUI
{
    public partial class Login_Form : Form
    {
        delegate void Client_Activation_Delegate();
        delegate void KBF_Activation_Delegate();
        delegate void Close_Current_Form();
        delegate void GetProcessesByName();
        delegate void kill();
       
        public Login_Form()
        {
            InitializeComponent();
            //this.BackColor = System.Drawing.Color.Gray;
        }

        public void Pipe_Reader(NamedPipeServerStream server)
        {
            //int i = 0;

            var br = new BinaryReader(server);
            var str = "";
            while (true)
            {
                try
                {
                    //i++;
                    var len = (int)br.ReadUInt32();
                    if (len == 0)

                    str = new string(br.ReadChars(len));    // Read string
                    if (str == "OK")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Close_Current_Form ccf = new Close_Current_Form(this.Close);
                                this.Invoke(ccf);
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
                                 MessageBox.Show("NOTOK");

                             });
                            
                            this.Invoke((MethodInvoker)delegate
                            {
                                MessageBox.Show("Wrong User Name Or Password");
                                KBF_Activation_Delegate KBF = new KBF_Activation_Delegate(this.KBF_Activation);
                                this.Invoke(KBF);

                            });
                            break;
                        }

                    }
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

            if (this.InvokeRequired == true)
            {
                Client_Activation_Delegate Engine_Activation = new Client_Activation_Delegate(this.Client_Activation);
                this.Invoke(Engine_Activation);
            }

            //Console.WriteLine("Waiting for connection...");
            server.WaitForConnection();

            string Data_To_Send = textBox1.Text + "," + textBox2.Text;

            var bw = new BinaryWriter(server);
            var buf = Encoding.ASCII.GetBytes(Data_To_Send);                // Get ASCII byte array     
            bw.Write((uint)buf.Length);                                     // Write string length
            bw.Write(buf);

            Pipe_Reader(server);
        }

        public void Client_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\22-2-16CP\Client_Side\Client_GUI\bin\Debug\Python\Client.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pythonProcess.Start();

        }

        public void KBF_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\22-2-16CP\Client_Side\Client_GUI\bin\Debug\Python\Freeze_KeyBoard.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pythonProcess.Start();

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Thread client = new Thread(new ThreadStart(Pipe_Writer));
            client.Start();
            //this.Close();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
           // ShowInTaskbar = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {       
            this.Invoke((MethodInvoker)delegate
            {
                GetProcessesByName kill = new GetProcessesByName(this.Killing_Python_Procsess);
                this.Invoke(kill);

            });
                
         
            this.Close();
        }


        public void Killing_Python_Procsess() 
        {           
            try
            {
                Process[] proc = Process.GetProcessesByName("python");
                for (int i = 0; i < proc.Length; i++)
                    proc[i].Kill();
            }
            catch
            {
                MessageBox.Show("So Many Errors");
            }
        }

        private void button3_Click(object sender, EventArgs e) //Clicked To Register
        {
            (new Register_Form()).ShowDialog();
        }

        private void KeyBoard_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ((Button)sender).Text;
        }

        private void Numeric_KeyBoard_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text + ((Button)sender).Text;
        }

        private void KeyBoard_Del_Click(object sender, EventArgs e)
        {
            string temp = "";
            for (int i = 0; i < textBox1.Text.Length - 1; i++)
            {
                temp += textBox1.Text[i];
            }
            textBox1.Text = temp;
        }

        private void Numeric_KeyBoard_Del_Click(object sender, EventArgs e)
        {
            string temp = "";
            for (int i = 0; i < textBox2.Text.Length-1; i++)
            {
                temp += textBox2.Text[i];
            }
            textBox2.Text = temp;
        }



        public void KEYBOARD()
        {
            int x = 269;
            int y = 465;
            for (int i = 0; i < 25; i++)
            {
                Button b = new Button();
                b.Location = new Point(x, y);
            }
        }

        


    }
}
