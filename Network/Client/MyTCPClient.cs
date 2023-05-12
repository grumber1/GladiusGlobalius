using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public static class MyTCPClient
{
    public static void TCPClient(string ip)
    {
        Int32 port = DevSettings.port;
        TcpClient clientToServerClient = new TcpClient(ip, port);
        MultiplayerManagerClient.clientToServerClient = clientToServerClient;
        Stream clientToServerStream = clientToServerClient.GetStream();
        MultiplayerManagerClient.clientToServerStream = clientToServerStream;
        Debug.Log("Client Thread: Received Stream");

        startReadIncomingNetworkTrafficThread();
        Debug.Log("Client Thread: ReadIncomingNetworkTraffic started");
    }

    public static void sendMessageToServer(
        string classToCall,
        string objectToCall,
        string method,
        string value
    )
    {
        string message = classToCall + "::::::" + objectToCall + ":::::" + method + "::::" + value;
        message = message + ":::::::";

        Debug.Log("Client: Trying to send message: " + message);
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        MultiplayerManagerClient.clientToServerStream.Write(data, 0, data.Length);
    }

    public static void sendObjectToServer<T>(
        string classToCall,
        string objectToCall,
        string method,
        T objectToSerialize
    )
    {
        string messageWithoutValue =
            classToCall + "::::::" + objectToCall + ":::::" + method + "::::";

        string serializedObject = Methods.SerializeObject(objectToSerialize);

        MyTCPClient.sendMessageToServer(classToCall, objectToCall, method, serializedObject);
    }

    private static void startReadIncomingNetworkTrafficThread()
    {
        ThreadStart readIncomingNetworkTrafficRef = new ThreadStart(CallReadIncomingNetworkTraffic);
        Thread readIncomingNetworkTrafficThread = new Thread(readIncomingNetworkTrafficRef);
        readIncomingNetworkTrafficThread.Start();
    }

    private static void CallReadIncomingNetworkTraffic()
    {
        readIncomingNetworkTraffic();
    }

    private static void readIncomingNetworkTraffic()
    {
        try
        {
            while (true)
            {
                Byte[] decodeData = new Byte[1024];
                Int32 streamBytes = MultiplayerManagerClient.clientToServerStream.Read(
                    decodeData,
                    0,
                    decodeData.Length
                );
                String receivedMessage = System.Text.Encoding.ASCII.GetString(
                    decodeData,
                    0,
                    streamBytes
                );
                Debug.Log("receivedMessage: " + receivedMessage);
                TCPMessageHandlerClient.handleMessage(receivedMessage);
            }
        }
        finally
        {
            Debug.Log("Client: Error in readIncomingNetworkTraffic");
            MyTCPClient.sendMessageToServer(
                "MultiplayerManager",
                "connectedPlayers",
                "disconnectPlayer",
                MultiplayerManagerClient.player.id
            );

            MultiplayerManagerClient.clientToServerStream.Close();
            MultiplayerManagerClient.clientToServerClient.Close();
        }
    }
}
