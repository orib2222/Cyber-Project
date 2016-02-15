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
       
        public Login_Form()
        {
            InitializeComponent();
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

        public void Client_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\15-2-16CP\Client_Side\Client_GUI\bin\Debug\Python\Client.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pythonProcess.Start();

        }

        public void KBF_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\15-2-16CP\Client_Side\Client_GUI\bin\Debug\Python\Freeze_KeyBoard.py";
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
            this.Close();
        }
    }
}
