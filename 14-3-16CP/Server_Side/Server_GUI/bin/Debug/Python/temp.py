import sqlite3

def If_Un_And_Pass_Already_Exists(UN_And_Pass):
        print "PARAM=" + UN_And_Pass
        conn = sqlite3.connect('Clients4.db')
        cur = conn.cursor()

        for row in cur.execute('select * from Clients4'):
            print row
            strr =  str(row[2])
            print strr

            if strr == UN_And_Pass:
                return True
        return False

print str(If_Un_And_Pass_Already_Exists("ori123"))
