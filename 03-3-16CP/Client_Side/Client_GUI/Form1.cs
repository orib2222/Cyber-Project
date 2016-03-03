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
            //this.Form_Design();

            SysTrayApp();
        }

        public void SysTrayApp()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("ShowStatus", OnShowStatus);
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "MyTrayApp";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnShowStatus(object sender, EventArgs e)
        {
            
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
