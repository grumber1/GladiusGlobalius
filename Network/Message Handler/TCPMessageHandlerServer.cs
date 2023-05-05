using UnityEngine;
using TMPro;
using System;
using System.Net;

public static class TCPMessageHandlerServer
{
    private static string outgoingMessage;
    private static string outgoingMessages;

    public static string handleMessage(string incomingMessages)
    {
        Debug.Log("Server: incomingMessages: " + incomingMessages);
        outgoingMessages = "";
        string[] messages = incomingMessages.Split(":::::::");
        foreach (string classObjectMethodValue in messages)
            if (classObjectMethodValue != "")
            {
                outgoingMessage = "";

                (string classToCall, string objectToCall, string method, string value) =
                    Methods.resolveMessage(classObjectMethodValue);

                Debug.Log("Server: handle Message: " + classObjectMethodValue); // MultiplayerManager:connectedPlayers:Add:saf
                Debug.Log("Server: handle classToCall: " + classToCall); // MultiplayerManager
                Debug.Log("Server: handle objectToCall: " + objectToCall); // connectedPlayers
                Debug.Log("Server: handle method: " + method); // Add
                Debug.Log("Server: handle value: " + value);

                handleMsg(classToCall, objectToCall, method, value);

                outgoingMessages += outgoingMessage + "::::";
            }
        Debug.Log("Server: Outgoing Messages: " + outgoingMessages);

        return outgoingMessages;
    }

    private static void handleMsg(
        string classToCall,
        string objectToCall,
        string method,
        string value
    )
    {
        switch (classToCall)
        {
            case "MultiplayerManager":
                multiplayerManager(objectToCall, method, value);
                break;
        }
    }

    private static void multiplayerManager(string objectToCall, string method, string value)
    {
        // Class:Object:Method:Value::::
        // MultiplayerManager:connectedPlayers:Add:saf::::
        switch (objectToCall)
        {
            case "connectedPlayers":
                connectedPlayers(method, value);
                break;
        }

        static void connectedPlayers(string method, string value)
        {
            switch (method)
            {
                case "Add":
                    addConnectedPlayersServer(value);
                    break;

                    static void addConnectedPlayersServer(string value)
                    {
                        string playerName = value;
                        int playerIndex = MultiplayerManagerServer.connectedPlayers
                            .ToArray()
                            .Length;
                        IPAddress playerIp = MyTCPServer.connectedIpAddresses[playerIndex];
                        string playerIpString = playerIp.ToString();

                        Player newPlayer = new Player(playerIndex, playerName, playerIpString);

                        MultiplayerManagerServer.connectedPlayers.Add(newPlayer);
                        string serializedConnectedPlayers = Methods.SerializeObject(
                            MultiplayerManagerServer.connectedPlayers
                        );
                        string outgoingMessageWithoutValue =
                            "MultiplayerManager::::::connectedPlayers:::::updateConnectedPlayers::::";
                        outgoingMessage += outgoingMessageWithoutValue + serializedConnectedPlayers;
                    }
            }
        }
    }
}
