

## Python Engine

import socket
from threading import Thread
import threading
import sys
import time
import struct
import os
import sqlite3
from Connection_Module import *
from DB_Managment import *

Local_Obj_Pipe = Connecting_Using_Pipe()


def Socket_Server():

    Adress = (("0.0.0.0",8080))
    Server_Socket = socket.socket()
    Server_Socket.bind(Adress)
    Server_Socket.listen(15)

    while True:
        print 'waiting for client connection...'
        (Client_Socket, Client_Adress) = Server_Socket.accept()
        print "Connected From: " + str(Client_Adress[0])

        DB_Obj = DB_Managment_Class()

        Local_Obj_Socket = Connecting_Using_Socket()
        User_Name_And_Password = Local_Obj_Socket.Socket_Recieve(Client_Socket)## Recieving Client User Name And Password

        DB_Obj.DB_Creation() ## Create DB or Doing Nothing If DB Is Already Exists

        ## Next 5 Lines Extracts The User And Password From Client

        Temp_Arr = User_Name_And_Password.split(',')
        print Temp_Arr[0] + " , " + Temp_Arr[1]
        if Temp_Arr[0] == "ori" and Temp_Arr[1] == "123":
            print "Before If_Client_Already_Exists" + Client_Adress[0]
            Bool_Result = DB_Obj.If_Client_Already_Exists(Client_Adress[0])



        ##Bool_Result = If_Client_Already_Exists(str(Client_Adress[0]))

        if Bool_Result == True:
            DB_Obj.Clients_DataBase_Add(str(Client_Adress[0]), User_Name_And_Password)

        else:
            print "Already Exists"

        DB_Obj.Insert_DB_Into_File()

        try:
            Data_From_Client = Client_Adress[0] + User_Name_And_Password
        except:
            print "Client Disconected"
            continue

        Pipe_Client_To_Server = Thread(target=Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client,))
        Pipe_Client_To_Server.start()


        t1 = Thread(target = Client_Handler,args=(Client_Socket,Client_Adress))
        t1.start()

        ##t1 = Thread(target=Client_Handler, args=(Client_Socket,Client_Adress,))
        ##t1.start()
        ##time.sleep(2)


def Client_Handler( Client_Socket, Client_Adress):
    print "Inside Handler"
    while (True):
        Local_Obj_Socket = Connecting_Using_Socket()
        try:
            Data_From_Client = Local_Obj_Socket.Socket_Recieve(Client_Socket)
        except:
            print "Client Disconected"
            break

        print Data_From_Client

        if "KILLED PROCESS USING CAMERA" in Data_From_Client:
            print "INSIDE IF"
            time.sleep(2)
            Pipe_Client_To_Server = Thread(target=Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client,))
            Pipe_Client_To_Server.start()

            Data_From_Server = ""
            Data_From_Server = Local_Obj_Pipe.Pipe_Server_To_Client()



            ##Pipe_Server_To_Client = Thread(target=Local_Obj_Pipe.Pipe_Server_To_Client(), args=())
            ##Pipe_Server_To_Client.start()
        else:
            print "INSIDE ELSE"
            Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client)

            Data_From_Server = ""
            Data_From_Server = Local_Obj_Pipe.Pipe_Server_To_Client()

        Local_Obj_Socket.Socket_Send(Client_Socket,Data_To_Send="Hello From Server!")



if __name__=='__main__':
    global Counter

    t = Thread(target = Socket_Server())
    t.start()

    ####################################################################

    ##Obj2 = Connecting_Using_Socket()









        
