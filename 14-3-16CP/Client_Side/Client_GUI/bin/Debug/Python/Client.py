

import socket
from Connection_Module import *
from Detects_Process_Using_Camera import *
import os
from Client_Security import *

def Checks_If_Camera_Is_Used():
    print "Inside "
    Detects_Process_Using_Camera_Obj = DetectProcess()
    Detects_Process_Using_Camera_Obj.run()
    Process_Name_Using_Camera =  Detects_Process_Using_Camera_Obj.process_name
    if Process_Name_Using_Camera != None:
        print "Inside " + Process_Name_Using_Camera
    return Process_Name_Using_Camera

def If_Process_Is_Not_Dangerous(Process_Name_Using_Camera):
    return False

if __name__=='__main__':

    Local_Obj_Pipe = Connecting_Using_Pipe()

    Data_From_Server = ""
    Data_From_Server = Local_Obj_Pipe.Pipe_Server_To_Client()

    print "Data_From_Server: " + Data_From_Server

    Temp_Arr = Data_From_Server.split(',')


    ##my_socket = Socket_Obj.Creating_Socket()
    my_socket = socket.socket()
    my_socket.connect(('10.92.5.79', 8080))

    Sec_Obj = Security()
    sym_key = Sec_Obj.key_exchange_client(my_socket)

    Socket_Obj = Connecting_Using_Socket(sym_key)


    ##if len(Temp_Arr) > 2: ##New Client Is Register
    UN = Temp_Arr[0]
    Pass = Temp_Arr[1]

        ##Data_From_Client = "OK"
        ##Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client)

    ##else:
        ##Data_From_Client = "NOTOK"
        ##Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client)

    ##User_Name_And_Password = Data_From_Server
    Socket_Obj.Socket_Send(my_socket, Data_From_Server)

    print "Before ending In Socket" + Data_From_Server

    Data_From_Server_About_Logging = Socket_Obj.Socket_Recieve(my_socket)

    print "After Recieve From Server" + Data_From_Server_About_Logging

    Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Server_About_Logging)

    ##print "AFTER"
    while (True):
        Process_Name_Using_Camera = Checks_If_Camera_Is_Used() ## And Returns The Process Name Using Camera
        ##print "Hello1"
        if Process_Name_Using_Camera != "VideoChatClient.exe" or Process_Name_Using_Camera == "" or Process_Name_Using_Camera == None: ## Checks If Camera Process Is Used

            Data_To_Send="All Clear"

            Socket_Obj.Socket_Send(my_socket,Data_To_Send)

            data = Socket_Obj.Socket_Recieve(my_socket)

        ##print "Hello2"
        else:
            bool = If_Process_Is_Not_Dangerous(Process_Name_Using_Camera) ## Returns False If The Parameter Process Is Dangerous
            ##print "bool: " + str(bool)
            if bool == False:  ##if Camera is Used By a Hurting Program Returns True
                test = os.system("taskkill /im " + Process_Name_Using_Camera)
                if test == 128: ## If Process Not Exists
                    print "No Such Process"

                Data_To_Send="KILLED PROCESS USING CAMERA " + Process_Name_Using_Camera

                Socket_Obj.Socket_Send(my_socket,Data_To_Send)

                print "DTS " + Data_To_Send

                data = Socket_Obj.Socket_Recieve(my_socket)

            ##if data != "":
              ##  print data
               ## my_socket.close()

            ##break


