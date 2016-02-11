
## Connection Module Using Pipes Or Socket

import socket
from threading import Thread
import threading
import sys
import time
import struct
import os

global Counter

class Connecting_Using_Pipe():
    
    
    def __init__(self):
        Temp = open(r'\\.\pipe\Data', 'r+b', 0)
        self.connecting_client_server_Pipe = Temp
        self.lock = threading.Semaphore(1)
   


    def Pipe_Client_To_Server(self):
      
        data = "OROROR"
        self.lock.acquire(1)
        self.connecting_client_server_Pipe.write(struct.pack('I', len(data)) + data)
        self.connecting_client_server_Pipe.seek(0)
        self.lock.release()

        data = "New Data"

        self.lock.acquire(1)
        self.connecting_client_server_Pipe.write(struct.pack('I', len(data)) + data)
        self.connecting_client_server_Pipe.seek(0)
        self.lock.release()


    def Pipe_Server_To_Client(self):
        
        Data_From_Server_Len = struct.unpack('I', self.connecting_client_server_Pipe.read(4))[0]     # Read str length
        Data = self.connecting_client_server_Pipe.read(Data_From_Server_Len)                         # Read str
        self.connecting_client_server_Pipe.seek(0)

       ## f = open('C:\\Users\\User\\Desktop\\ttt.txt', 'w')
       ## f.write(Data)
       ## f.close()




class Connecting_Using_Socket():

    def __init__(self):

        pass

    def Creating_Socket(self, my_socket):
        my_socket = socket.socket()
        my_socket.connect(('192.168.1.19', 8084))

    def Socket_Recieve(self,Client_Socket):
        Data_From_Client = Client_Socket.recv(1024)
        return Data_From_Client


    def Socket_Send(self,Client_Socket,Data_To_Send):
        Client_Socket.send(Data_To_Send)



