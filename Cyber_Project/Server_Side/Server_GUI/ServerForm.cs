using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;

namespace Server_GUI
{
    public partial class ServerForm : Form
    {
        delegate void Engine_Activation_Delegate();

        
     
        public ServerForm()
        {
            InitializeComponent();
            this.Text = "Server Window";
            this.StartPosition = FormStartPosition.CenterScreen;

            Thread engine = new Thread(new ThreadStart(Pipe_Reader));
            engine.Start();

        }

        public void Pipe_Reader()
        {
            int i = 0; 
            // Open the named pipe.
            var server = new NamedPipeServerStream("Data");

            //if (this.InvokeRequired)
            //{
            //    Engine_Activation_Delegate Engine_Activation = new Engine_Activation_Delegate(this.Engine_Activation);
            //    this.Invoke(Engine_Activation);
            //}

            //Console.WriteLine("Waiting for connection...");
            server.WaitForConnection();


            //Console.WriteLine("Connected.");
            var br = new BinaryReader(server);
            //var bw = new BinaryWriter(server);
            
            while (true)
            {
                try
                {
                    i++;
                    var len = (int)br.ReadUInt32();
                    if (len == 0)
                        MessageBox.Show("len equals zero");

                    var str = new string(br.ReadChars(len));    // Read string
                    if (i == 1)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                textBox1.Text += str;
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

            Pipe_Writer(server);
        }



        public void Pipe_Writer(NamedPipeServerStream server)
        {
            string Data_To_Send = "Message From Server Using Pipe";

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox2.Text += Data_To_Send;

                });
            }
            var bw = new BinaryWriter(server);
            var buf = Encoding.ASCII.GetBytes(Data_To_Send);                // Get ASCII byte array     
            bw.Write((uint)buf.Length);                                     // Write string length
            bw.Write(buf);   
        }


        public void Engine_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"C:\Users\User\Desktop\Cyber_Project\Server_Side\Temp_GUI\bin\Debug\Python\Project_Engine.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.Start();     

        }

  
    }


}

