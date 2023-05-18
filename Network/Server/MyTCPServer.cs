using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public static class MyTCPServer
{
    static TcpListener server = null;
    static Boolean acceptNewConnections = true;
    public static Thread listenerThread;
    public static string localIp;
    public static bool serverWaitingForNewConnections = true;
    public static string content = "";
    public static int byteSizeForMessageToReceive = DevSettings.standardByteSize;

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
            //TODO muss ich hier vorher die clients disconnecten???
            server.Stop();
            //stopChildThread();
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

    private static String receiveMessage(NetworkStream stream)
    {
        Byte[] bytes = new Byte[byteSizeForMessageToReceive];
        String receivedMessage = System.Text.Encoding.ASCII.GetString(
            bytes,
            0,
            stream.Read(bytes, 0, bytes.Length)
        );
        byteSizeForMessageToReceive = DevSettings.standardByteSize;
        Debug.Log("TCPServer: received message: " + receivedMessage);
        Debug.Log("TCPServer: received message has " + bytes.Length + " bytes");
        return receivedMessage;
    }

    public static void sendByteSizeToClients(byte[] originalMessageInBytes)
    {
        string classToCallByteSizeMessage = "myTCPClient";
        string objectToCallByteSizeMessage = "messageByteSizeToReceive";
        string methodByteSizeMessage = "set";
        string valueByteSizeMessage = originalMessageInBytes.Length.ToString();
        string byteSizeMessage =
            classToCallByteSizeMessage
            + "::::::"
            + objectToCallByteSizeMessage
            + ":::::"
            + methodByteSizeMessage
            + "::::"
            + valueByteSizeMessage;

        byte[] byteSizeMessageInBytes = System.Text.Encoding.ASCII.GetBytes(byteSizeMessage);

        Debug.Log("Original byteSizeMessageInBytes is " + byteSizeMessageInBytes.Length + " bytes");
        for (
            int messageBytes = byteSizeMessageInBytes.Length;
            messageBytes < DevSettings.standardByteSize;
            messageBytes++
        )
        {
            byteSizeMessage += " ";
        }

        byteSizeMessageInBytes = System.Text.Encoding.ASCII.GetBytes(byteSizeMessage);
        Debug.Log("Extended byteSizeMessageInBytes is " + byteSizeMessageInBytes.Length + " bytes");

        Debug.Log(
            "TCPServer: Setting Client-Receiver to " + originalMessageInBytes.Length + " bytes"
        );
        Debug.Log(
            "TCPServer: Settings-Message has a size of " + byteSizeMessageInBytes.Length + " bytes"
        );
        int i = 0;
        MultiplayerManagerServer.serverToClientStreams.ForEach(serverToClientStream =>
        {
            if (MultiplayerManagerServer.serverToClientClients[i].Connected)
            {
                serverToClientStream.Write(
                    byteSizeMessageInBytes,
                    0,
                    byteSizeMessageInBytes.Length
                );
            }
            i++;
        });
    }

    public static void sendMessageToClients(string classObjectMethodToCall, string value)
    {
        string classToCall = classObjectMethodToCall.Split(".")[0];
        string objectToCall = classObjectMethodToCall.Split(".")[1];
        string method = classObjectMethodToCall.Split(".")[2];

        string message = classToCall + "::::::" + objectToCall + ":::::" + method + "::::" + value;
        message = message + ":::::::";
        byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

        sendByteSizeToClients(msg);

        Debug.Log("TCPServer: Trying to send message with " + msg.Length + " bytes");
        addTextToServerConsole("TCPServer: Trying to send message with " + msg.Length + " bytes");
        int i = 0;
        MultiplayerManagerServer.serverToClientStreams.ForEach(serverToClientStream =>
        {
            if (MultiplayerManagerServer.serverToClientClients[i].Connected)
            {
                serverToClientStream.Write(msg, 0, msg.Length);
            }
            i++;
        });

        Debug.Log("TCPServer: sent Message: " + message);
        addTextToServerConsole("Server: sent Message: " + message);
    }

    public static void sendObjectToClients<T>(string classObjectMethodToCall, T objectToSerialize)
    {
        string serializedObject = Methods.SerializeObject(objectToSerialize);

        MyTCPServer.sendMessageToClients(classObjectMethodToCall, serializedObject);
    }

    private static void startClientHandlingThread()
    {
        ThreadStart clientHandlingRef = new ThreadStart(CallClientHandlingThread);
        Thread clientHandlingThread = new Thread(clientHandlingRef);

        MultiplayerManagerServer.clientHandlingThreads.Add(clientHandlingThread);
        int index = MultiplayerManagerServer.clientHandlingThreads.ToArray().Length - 1;
        MultiplayerManagerServer.clientHandlingThreads[index].Start();
    }

    private static void CallClientHandlingThread()
    {
        clientHandling();
    }

    private static void clientHandling()
    {
        try
        {
            Debug.Log("Server ClientHandling Thread: started");
            addTextToServerConsole("Server: Waiting for new connection...");
            Debug.Log("Server ClientHandling Thread: Waiting for new connection...");

            TcpClient serverToClientClient = MyTCPServer.server.AcceptTcpClient();
            MultiplayerManagerServer.serverToClientClients.Add(serverToClientClient);
            acceptNewConnections = true;

            addTextToServerConsole("Server: Client Connected!");
            Debug.Log("Server ClientHandling Thread: Client Connected!");

            NetworkStream serverToClientStream = serverToClientClient.GetStream();
            MultiplayerManagerServer.serverToClientStreams.Add(serverToClientStream);

            IPAddress connectedIp = (
                (IPEndPoint)serverToClientClient.Client.RemoteEndPoint
            ).Address;
            MultiplayerManagerServer.connectedIpAddresses.Add(connectedIp);

            Debug.Log("Server ClientHandling Thread: Client Added!");
            while (serverToClientClient.Connected)
            {
                String receivedMessage = receiveMessage(serverToClientStream);
                addTextToServerConsole("Server: received Message: " + receivedMessage);
                TCPMessageHandlerServer.handleMessage(receivedMessage);
            }
            Debug.Log("Server ClientHandling Thread: disconnecting Client");
        }
        finally
        {
            Debug.Log("Server ClientHandling Thread: stopped");
            MyTCPServer.sendMessageToClients(
                Messages.Server.MultiplayerManager.ConnectedPlayers.disconnect,
                "true"
            );
            MultiplayerManagerServer.serverToClientStreams.ForEach(
                (serverToClientStream) =>
                {
                    serverToClientStream.Dispose();
                }
            );
            MultiplayerManagerServer.serverToClientClients.ForEach(
                (serverToClientClient) =>
                {
                    serverToClientClient.Dispose();
                }
            );
            server.Stop();
        }
    }

    private static void addTextToServerConsole(string newContent)
    {
        newContent += "\n";
        content = content + newContent;
        string[] contentSplit = content.Split("\n");
        int contentLines = content.Split("\n").Length;
        int maxLines = 30;
        if (contentLines > maxLines)
        {
            int tooManyLines = contentLines - maxLines;
            content = "";

            for (int i = tooManyLines; i < contentLines; i++)
            {
                content += contentSplit[i];
            }
        }
    }
}
