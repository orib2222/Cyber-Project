
import sqlite3

class DB_Managment_Class:

    def __init__(self):
        return

    def DB_Creation(self):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()
        cur.execute("create table if not exists Clients4(ID INTEGER PRIMARY KEY , Client_Adress text , User_Name_And_Password text);")

    def If_Client_Already_Exists(self,Client_Adress):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()

        for row in cur.execute('select * from Clients4'):
            Temp_Arr = str(row).split(',')
            strr =  str(Temp_Arr[1])
            ##Curret_Client_Add = strr[3:len(strr)-2]
            Curret_Client_Add = strr.split("'")
            ##print Curret_Client_Add[1]
            if Curret_Client_Add[1] == Client_Adress:
                return True
        return False

    def If_Un_And_Pass_Already_Exists(self,UN_And_Pass):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()

        for row in cur.execute('select * from Clients4'):
            Temp_Arr = str(row).split(',')
            strr =  str(Temp_Arr[2])

            User_Name_And_Password = strr[3:len(strr)-2]

            if strr == UN_And_Pass:
                return True
        return False


## Adding Specific Client (given as parameter) To the Clients DataBase
    def Clients_DataBase_Add(self,Client_Adress,User_Name_And_Password):
        print Client_Adress
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()
        ##cur.execute("create table if not exists Clients3(ID INTEGER PRIMARY KEY , Client_Adress text );")
        cur.execute("insert into Clients4(Client_Adress,User_Name_And_Password) values (?,?)", (Client_Adress,User_Name_And_Password) )
        conn.commit()

## Deletes Specific Client From The DataBase
    def Clients_DataBase_Delete(self,Client_Adress):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()
        cur.execute("DELETE FROM Clients4 WHERE socket=(?)", [Client_Adress])
        ##print "Total number of rows deleted: ", conn.total_changes

        ##for row in cur.execute('select * from test'):
        ##   print row

        conn.close()

## Returns Clients ID Number
    def Insert_DB_Into_File(self):
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()
        ##counter = 0
        print "Before FILE Written"
        f = open('C:\\Users\\User\\Desktop\\RTR.txt', 'w')

        for row in cur.execute('select * from Clients4'):
            ##counter += 1
            f.write(str(row))

        f.close()
        conn.close()
        return
        ##return counter

