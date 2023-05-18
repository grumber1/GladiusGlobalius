using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public static class MyTCPClient
{
    public static int byteSizeForMessageToReceive = DevSettings.standardByteSize;

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
        byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

        sendByteSizeToServer(msg);

        Debug.Log("TCPClient: Trying to send message with " + msg.Length + " bytes");
        MultiplayerManagerClient.clientToServerStream.Write(msg, 0, msg.Length);
        Debug.Log("TCPClient: sent Message: " + message);
    }

    public static void sendByteSizeToServer(byte[] originalMessageInBytes)
    {
        string classToCallByteSizeMessage = "MyTCPServer";
        string objectToCallByteSizeMessage = "MessageByteSizeToReceive";
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
            "TCPClient: Setting Server-Receiver to " + originalMessageInBytes.Length + " bytes"
        );
        Debug.Log(
            "TCPClient: Settings-Message has a size of " + byteSizeMessageInBytes.Length + " bytes"
        );
        MultiplayerManagerClient.clientToServerStream.Write(
            byteSizeMessageInBytes,
            0,
            byteSizeMessageInBytes.Length
        );
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
                Byte[] bytes = new Byte[byteSizeForMessageToReceive];
                Int32 streamBytes = MultiplayerManagerClient.clientToServerStream.Read(
                    bytes,
                    0,
                    bytes.Length
                );
                String receivedMessage = System.Text.Encoding.ASCII.GetString(
                    bytes,
                    0,
                    streamBytes
                );
                byteSizeForMessageToReceive = DevSettings.standardByteSize;
                Debug.Log("TCPClient: received message: " + receivedMessage);
                Debug.Log("TCPServer: received message has " + bytes.Length + " bytes");
                TCPMessageHandlerClient.handleMessage(receivedMessage);
            }
        }
        finally
        {
            Debug.Log("Client readIncomingNetworkTraffic Thread: Error");
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
