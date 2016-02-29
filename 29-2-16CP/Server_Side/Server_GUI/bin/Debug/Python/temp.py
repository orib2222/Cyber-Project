import sqlite3


conn = sqlite3.connect('Clients4.db')
cur = conn.cursor()
cur.execute("create table if not exists Clients4(ID INTEGER PRIMARY KEY , Client_Adress text , User_Name_And_Password text);")
temp = True

for row in cur.execute('select * from Clients4'):
    Temp_Arr = str(row).split(',')
    strr =  str(Temp_Arr[1])
    ##Curret_Client_Add = strr[3:len(strr)-2]
    Curret_Client_Add = strr.split("'")
    print Curret_Client_Add[1]



for row in cur.execute('select * from Clients4'):
    print row


print str(temp)
