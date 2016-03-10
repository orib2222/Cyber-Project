using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client_GUI
{
    public class myEventArgs : EventArgs
    {
        private string User_Name, Password;

        public myEventArgs(string User_Name, string Password)
        {
            this.User_Name = User_Name;
            this.Password = Password;
        }

        public string get_UN()
        {
            return User_Name;
        }

        public string get_P()
        {
            return Password;
        }
    }
}
