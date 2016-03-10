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
        delegate void Open_Form1();
        delegate void kill();

        public Register_Form Reg_Form;
        public int Client_Procees_ID;
        public string Registered_UN, Registered_P;
        public bool If_Registered = false;

        public Login_Form()
        {
            InitializeComponent();
            Kill_Explorer();
            //this.BackColor = System.Drawing.Color.Gray;
        }

        private void Kill_Explorer()
        {
            try
            {
                Process[] proc = Process.GetProcessesByName("explorer");
                for (int i = 0; i < proc.Length; i++)
                    proc[i].Kill();
            }
            catch(Exception ex){}
        }

        private void Start_Explorer()
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Windows\explorer.exe";
            process.Start();

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
                        MessageBox.Show("Error");
                    var str = new string(br.ReadChars(len));    // Read string
                    if (str == "Logged In!")
                    {
                        Start_Explorer();
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.Hide();
                                (new Form1()).ShowDialog();
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
                                 MessageBox.Show(str);

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

            string Data_To_Send = "";

            if (If_Registered == true)
                Data_To_Send = textBox1.Text + "," + textBox2.Text + "," + "REG";
            else
                Data_To_Send = textBox1.Text + "," + textBox2.Text;

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
            pythonProcess.StartInfo.Arguments = @"Python\Client.py";
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pythonProcess.Start();
            Client_Procees_ID = pythonProcess.Id;
        }

        public void KBF_Activation()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = @"C:\Python27\python.exe";
            pythonProcess.StartInfo.Arguments = @"Python\Freeze_KeyBoard.py";
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
            Reg_Form = new Register_Form();
            Reg_Form.myEvent += new myDelegate(myFunction);
            Reg_Form.Show();
            If_Registered = true;
            
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

        private void myFunction(object sender, myEventArgs e) 
        {
            //MessageBox.Show(e.get_UN() + e.get_P());
            Registered_UN = e.get_UN();
            Registered_P = e.get_P();

            textBox1.Text = Registered_UN;
            textBox2.Text = Registered_P;
        }

    }
}
