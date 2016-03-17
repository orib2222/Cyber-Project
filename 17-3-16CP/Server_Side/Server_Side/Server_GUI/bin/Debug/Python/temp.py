import sqlite3

def Insert_DB_Into_File(self):
    conn = sqlite3.connect('Clients4.db')
    cur = conn.cursor()
    ##counter = 0
    print "Before FILE Written"
    f = open('C:\\Users\\User\\Desktop\\RTR.txt', 'w')
    for row in cur.execute('select * from Clients4'):
        ##counter += 1
        print row
        f.write(str(row))

    f.close()
    conn.close()
    return
    ##return counter
