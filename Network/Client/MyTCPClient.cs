using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public static class MyTcpClient
{
    public static void TCPClient(string ip)
    {
        Int32 port = DevSettings.port;
        TcpClient client = new TcpClient(ip, port);
        Stream stream = client.GetStream();
        MultiplayerManagerClient.stream = stream;
        Debug.Log("Client Thread: Received Stream");

        startReadIncomingNetworkTrafficThread();
        Debug.Log("Client Thread: ReadIncomingNetworkTraffic started");
    }

    public static void sendToNetwork(string message)
    {
        //Debug.Log("MyTcpClient: sent data: " + message);
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        MultiplayerManagerClient.stream.Write(data, 0, data.Length);
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
                Int32 streamBytes = MultiplayerManagerClient.stream.Read(
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
            Debug.Log("Client Error");
            sendToNetwork("disconnected: " + MultiplayerManagerClient.name);
            MultiplayerManagerClient.stream.Close();
            MultiplayerManagerClient.client.Close();
        }
    }
}
