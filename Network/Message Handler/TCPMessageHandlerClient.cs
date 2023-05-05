using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public static class TCPMessageHandlerClient
{
    public static void handleMessage(string incomingMessages)
    {
        Debug.Log("Client Message Handler: incomingMessages: " + incomingMessages);

        string[] messages = incomingMessages.Split(":::::::");
        foreach (string classObjectMethodValue in messages)
        {
            (string classToCall, string objectToCall, string method, string value) =
                Methods.resolveMessage(classObjectMethodValue);

            handlePayload(classToCall, objectToCall, method, value);
        }
    }

    private static void handlePayload(
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
                case "updateConnectedPlayers":
                    updateConnectedPlayers(value);
                    break;

                    static void updateConnectedPlayers(string value)
                    {
                        List<Player> connectedPlayers = Methods.DeserializeObject<List<Player>>(
                            value
                        );

                        MultiplayerManagerClient.connectedPlayers = connectedPlayers;
                        MultiplayerManagerClient.connectedPlayer = connectedPlayers[
                            MultiplayerManagerClient.id
                        ];
                    }
            }
        }
    }
}
