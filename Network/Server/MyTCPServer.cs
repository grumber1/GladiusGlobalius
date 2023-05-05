﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public static class MyTCPServer
{
    static TcpListener server = null;
    static Boolean acceptNewConnections = true;
    static Thread clientHandlingThread;
    public static string localIp;
    public static bool serverWaitingForNewConnections = true;
    public static string content = "";

    public static void listenerStart()
    {
        try
        {
            Int32 port = DevSettings.port;
            string[] localIps = getLocalIps();
            IPAddress localAddr = IPAddress.Parse(localIp);
            server = new TcpListener(localAddr, port);
            server.Start();

            addTextToServerConsole("Server: Listener started!");
            Debug.Log("Server: Listener started!");
            while (serverWaitingForNewConnections)
            {
                if (acceptNewConnections)
                {
                    acceptNewConnections = false;
                    startClientHandlingThread();
                }
            }
        }
        finally
        {
            stopChildThread();
        }
    }

    public static string[] getLocalIps()
    {
        string localIpsString = "";
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress iPAddress in host.AddressList)
        {
            if (AddressFamily.InterNetwork == iPAddress.AddressFamily)
            {
                localIpsString += iPAddress + ";";
            }
        }
        localIpsString = localIpsString.Remove(localIpsString.Length - 1);
        string[] localIps = localIpsString.Split(";");
        return localIps;
    }

    private static void stopChildThread()
    {
        clientHandlingThread.Abort();
    }

    private static String receiveMessage(NetworkStream stream)
    {
        Byte[] bytes = new Byte[1024];
        String receivedMessage = System.Text.Encoding.ASCII.GetString(
            bytes,
            0,
            stream.Read(bytes, 0, bytes.Length)
        );
        return receivedMessage;
    }

    private static void sendMessage(String message)
    {
        byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
        int i = 0;
        MultiplayerManagerServer.serverToClientStreams.ForEach(serverToClientStream =>
        {
            if (MultiplayerManagerServer.serverToClientClients[i].Connected)
            {
                serverToClientStream.Write(msg, 0, msg.Length);
            }
            i += 1;
        });
    }

    private static void startClientHandlingThread()
    {
        ThreadStart clientHandlingRef = new ThreadStart(CallClientHandlingThread);

        clientHandlingThread = new Thread(clientHandlingRef);
        clientHandlingThread.Start();
    }

    private static void CallClientHandlingThread()
    {
        clientHandling();
    }

    private static void clientHandling()
    {
        try
        {
            Debug.Log("Server Thread: ClientHandling started");
            addTextToServerConsole("Server: Waiting for new connection...");
            Debug.Log("Server: Waiting for new connection...");

            TcpClient serverToClientClient = server.AcceptTcpClient();
            MultiplayerManagerServer.serverToClientClients.Add(serverToClientClient);
            acceptNewConnections = true;

            addTextToServerConsole("Server: Client Connected!");
            Debug.Log("Server: Client Connected!");

            NetworkStream serverToClientStream = serverToClientClient.GetStream();
            MultiplayerManagerServer.serverToClientStreams.Add(serverToClientStream);

            IPAddress connectedIp = (
                (IPEndPoint)serverToClientClient.Client.RemoteEndPoint
            ).Address;
            MultiplayerManagerServer.connectedIpAddresses.Add(connectedIp);

            Debug.Log("Server: Client Added!");
            Boolean clientConnected = serverToClientClient.Connected;
            while (clientConnected)
            {
                if (serverToClientStream.DataAvailable)
                {
                    //Debug.Log("Server: Stream Data available");
                    String receivedMessage = receiveMessage(serverToClientStream);
                    Debug.Log("Server: received Message: " + receivedMessage);
                    string messageToSend = TCPMessageHandlerServer.handleMessage(receivedMessage);
                    sendMessage(messageToSend);
                    Debug.Log("Server: sent Message: " + messageToSend);
                }
                else
                {
                    clientConnected = serverToClientClient.Connected;
                }
            }
            Debug.Log("disconnecting Client");
            serverToClientClient.Close();
            server.Stop();
        }
        finally
        {
            Debug.Log("Server: ChildThread stopped");
            stopChildThread();
        }
    }

    private static void addTextToServerConsole(string newContent)
    {
        newContent += "\n";
        content = content + newContent;
    }
}