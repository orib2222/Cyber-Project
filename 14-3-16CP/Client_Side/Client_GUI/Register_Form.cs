using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client_GUI
{
    public delegate void myDelegate(object sender, myEventArgs e);

    public partial class Register_Form : Form
    {
        public event myDelegate myEvent;
        public string User_Name, Password;

        public Register_Form()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Name = textBox1.Text;
            Password = textBox2.Text;

            myEventArgs myEventArgs_temp = new myEventArgs(User_Name, Password);
            if (myEvent != null)
                myEvent(this, myEventArgs_temp);

            this.Close();

        }
    }
}
