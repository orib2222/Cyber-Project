

import socket
from Connection_Module import *
from Detects_Process_Using_Camera import *
import os

def Checks_If_Camera_Is_Used():
    print "Inside "
    Detects_Process_Using_Camera_Obj = DetectProcess()
    Detects_Process_Using_Camera_Obj.run()
    Process_Name_Using_Camera =  Detects_Process_Using_Camera_Obj.process_name
    print "Inside " + Process_Name_Using_Camera
    return Process_Name_Using_Camera

def If_Process_Is_Not_Dangerous(Process_Name_Using_Camera):
    return False

if __name__=='__main__':

    while True:
        Process_Name_Using_Camera = Checks_If_Camera_Is_Used() ## And Returns The Process Name Using Camera
        bool = If_Process_Is_Not_Dangerous(Process_Name_Using_Camera)
        print "bool: " + str(bool)
        if bool == False:  ##if Camera is Used By a Hurting Program Returns True

            test = os.system("taskkill /im " + Process_Name_Using_Camera)
            if test == 128:
                print "No Such Process"


            Obj = Connecting_Using_Socket()

            my_socket = socket.socket()
            my_socket.connect(('192.168.56.1', 8086))

            Data_To_Send="KILL PROCESS USING CAMERA " + Process_Name_Using_Camera

            t2 = Thread(target=Obj.Socket_Send, args=(my_socket,Data_To_Send))
            t2.start()

            print "DTS " + Data_To_Send

            data = Obj.Socket_Recieve(my_socket)

            if data != "":
                print data
                my_socket.close()

            break

    print "Out Of While"