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
        
        private Button button1;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public Form1()
        {
            InitializeComponent();
            this.Form_Design();

            //Thread Client = new Thread(new ThreadStart(Client_Activation));
            //Client.Start();


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
        

    /*    public void Pipe_Reader(NamedPipeServerStream server)
        {
            //int i = 0;

            //var br = new BinaryReader(server);

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
                            //this.Invoke((MethodInvoker)delegate
                            //{
                            //    KBF_Activation_Delegate KBF = new KBF_Activation_Delegate(this.KBF_Activation);
                            //    this.Invoke(KBF);

                            //});
                            //break;
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
        */


        public void Pipe_Writer()
        {
            // Open the named pipe.
            var server = new NamedPipeServerStream("ClientData");

            //if (this.InvokeRequired)
            //{
            //    Client_Activation_Delegate Engine_Activation = new Client_Activation_Delegate(this.Client_Activation);
            //    this.Invoke(Engine_Activation);
            //}

            //Console.WriteLine("Waiting for connection...");
            server.WaitForConnection();

          

            //if (this.InvokeRequired)
            //{
            //    this.Invoke((MethodInvoker)delegate
            //    {
                    

            //    });
            //}

            //var bw = new BinaryWriter(server);
            //var buf = Encoding.ASCII.GetBytes(Data_To_Send);                // Get ASCII byte array     
            //bw.Write((uint)buf.Length);                                     // Write string length
            //bw.Write(buf);

            //Pipe_Reader(server);
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
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            (new Login_Form()).ShowDialog();
            //Show();
        }




















        //public void SysTrayApp()
        //{
        //    // Create a simple tray menu with only one item.
        //    trayMenu = new ContextMenu();
        //    trayMenu.MenuItems.Add("Login", OnLogin);
        //    trayMenu.MenuItems.Add("Exit", OnExit);

        //    // Create a tray icon. In this example we use a
        //    // standard system icon for simplicity, but you
        //    // can of course use your own custom icon too.
        //    trayIcon = new NotifyIcon();
        //    trayIcon.Text = "MyTrayApp";
        //    trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

        //    // Add menu to tray icon and show it.
        //    trayIcon.ContextMenu = trayMenu;
        //    trayIcon.Visible = true;
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    Visible = false; // Hide form window.
        //    ShowInTaskbar = false; // Remove from taskbar.

        //    base.OnLoad(e);
        //}

        //private void OnExit(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}

        //private void OnLogin(object sender, EventArgs e)
        //{
        //    (new LoginForm()).ShowDialog();
        //}


    }
}
