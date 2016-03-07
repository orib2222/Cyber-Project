import sqlite3

def If_Client_Already_Exists(Client_Adress):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()
                
        for row in cur.execute('select * from Clients4'):
            Temp_Arr = str(row).split(',')
            strr =  str(Temp_Arr[2])

            User_Name_And_Password = strr[3:len(strr)-2]

            if Curret_Client_Add[1] == Client_Adress:
                return True
        return False

##conn = sqlite3.connect('Clients4.db')
##cur = conn.cursor()
##for row in cur.execute('select * from Clients4'):
##        print row

If_Client_Already_Exists("192.168.56.1")
##print temp
