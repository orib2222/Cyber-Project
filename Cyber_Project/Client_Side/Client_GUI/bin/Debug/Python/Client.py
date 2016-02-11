

import socket
from Connection_Module import *
from Detects_Process_Using_Camera import *
import os

def Checks_If_Camera_Is_Used():
    print "Inside "
    Detects_Process_Using_Camera_Obj = DetectProcess()
    Detects_Process_Using_Camera_Obj.run()
    Process_Name_Using_Camera =  Detects_Process_Using_Camera_Obj.process_name
    ##print "Inside " + Process_Name_Using_Camera
    return Process_Name_Using_Camera

def If_Process_Is_Not_Dangerous(Process_Name_Using_Camera):
    return False

if __name__=='__main__':

    Local_Obj_Pipe = Connecting_Using_Pipe()

    Data_From_Server = ""
    Data_From_Server = Local_Obj_Pipe.Pipe_Server_To_Client()

    print "Data_From_Server: " + Data_From_Server

    Temp_Arr = Data_From_Server.split(',')

    if Temp_Arr[0] == "ori" and Temp_Arr[1] == "123":
        print "OK"
        Data_From_Client = "OK"
        Local_Obj_Pipe.Pipe_Client_To_Server(Data_From_Client)







    """



    Socket_Obj = Connecting_Using_Socket()
    my_socket = Socket_Obj.Return_Current_Socket()
    while True:
        Process_Name_Using_Camera = Checks_If_Camera_Is_Used() ## And Returns The Process Name Using Camera
        print "Hello1"


        if Process_Name_Using_Camera == "" or Process_Name_Using_Camera == None: ## Checks If Camera Process Is Used
            continue
        print "Hello2"
        bool = If_Process_Is_Not_Dangerous(Process_Name_Using_Camera) ## Returns False If The Parameter Process Is Dangerous
        ##print "bool: " + str(bool)
        if bool == False:  ##if Camera is Used By a Hurting Program Returns True
            test = os.system("taskkill /im " + Process_Name_Using_Camera)
            if test == 128: ## If Process Not Exists
                print "No Such Process"




            Data_To_Send="KILLED PROCESS USING CAMERA " + Process_Name_Using_Camera

            t2 = Thread(target=Socket_Obj.Socket_Send, args=(my_socket,Data_To_Send))
            t2.start()

            print "DTS " + Data_To_Send

            data = Socket_Obj.Socket_Recieve(my_socket)

            if data != "":
                print data
                my_socket.close()

            break"""
    while True:
        continue
    print "Out Of While"