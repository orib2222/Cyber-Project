
import sqlite3

class DB_Managment_Class:

    def __init__(self):
        return

    def If_Client_Already_Exists(self,Client_Adress):
        conn = sqlite3.connect('Clients3.db')
        cur = conn.cursor()
        cur.execute("create table if not exists Clients3(ID INTEGER PRIMARY KEY , Client_Adress text );")

        for row in cur.execute('select * from Clients3'):
            Temp_Arr = str(row).split(',')
            strr =  str(Temp_Arr[1])
            Curret_Client_Add = strr[3:len(strr)-2]
            if Curret_Client_Add == Client_Adress:
                return False
        return True

## Adding Specific Client (given as parameter) To the Clients DataBase
    def Clients_DataBase_Add(self,Client_Adress):
        print Client_Adress
        conn = sqlite3.connect('Clients3.db')
        cur = conn.cursor()
        cur.execute("create table if not exists Clients3(ID INTEGER PRIMARY KEY , Client_Adress text );")
        cur.execute("insert into Clients3(Client_Adress) values (?)", (Client_Adress,) )
        conn.commit()

## Deletes Specific Client From The DataBase
    def Clients_DataBase_Delete(self,Client_Adress):
        conn = sqlite3.connect('Clients3.db')
        cur = conn.cursor()
        cur.execute("DELETE FROM Clients3 WHERE socket=(?)", [Client_Adress])
        ##print "Total number of rows deleted: ", conn.total_changes

        ##for row in cur.execute('select * from test'):
        ##   print row

        conn.close()

## Returns Clients ID Number
    def Insert_DB_Into_File(self):
        conn = sqlite3.connect('Clients3.db')
        cur = conn.cursor()
        ##counter = 0
        print "Before FILE Written"
        f = open('C:\\Users\\User\\Desktop\\ttt.txt', 'w')

        for row in cur.execute('select * from Clients3'):
            ##counter += 1
            f.write(str(row))

        f.close()
        conn.close()
        return
        ##return counter

