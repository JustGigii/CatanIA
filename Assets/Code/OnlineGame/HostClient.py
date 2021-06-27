import thread
import socket
import UnityEngine
class Server():
    ip = '192.168.1.141'
    port = 1232
    port= 321
    clients = []
    hostsocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    Server_socket = socket.socket()
    def __init__(self):
        self.Server_socket.connect((self.ip ,self.port))
    def BeHost(self):
        self.Server_socket.send(('host').encode())
        Answer =self.Server_socket.recv(1024).decode()
        (Hip,Hport)=Answer.split(':')
        self.hostsocket.bind((Hip, int(Hport)))
        #self.hostsocket.settimeout(10)
        self.Server_socket.close()
    def AddPlayer(self):
        self.hostsocket.listen(1)
        (client, cip) = self.hostsocket.accept()
        self.clients.append(client)
        return client
    def PopHost(self):
        self.Server_socket.send('PopHost'.encode())
        return self.Server_socket.recv(1024).decode()
    def Connection(self,ip):
        self.Server_socket.send('guesst'.encode())
        self.Server_socket.send((ip).encode())
        self.Server_socket.close()
        (hip, port) = ip.split(":")
        self.hostsocket.connect((hip,int(port)))
        #self.hostsocket.settimeout(10)
        self.hostsocket.send("Stop".encode())
    def Host(self,Client):
            answer = Client.recv(1024).decode()
            for i in self.clients:
                i.send((answer).encode())
            return answer
    def Hostsend(self, Messge):
        for i in self.clients:
            i.send((Messge).encode())
    def SendMEss(self,messge):
        self.hostsocket.send((messge).encode())
    def RecvFormServer(self):
        return self.hostsocket.recv(1024).decode()

