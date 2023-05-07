using UnityEngine;
using TMPro;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

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

                handlePayload(classToCall, objectToCall, method, value);

                outgoingMessages += outgoingMessage + "::::";
            }
        Debug.Log("Server: Outgoing Messages: " + outgoingMessages);

        return outgoingMessages;
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
                case "addNewPlayer":
                    addNewPlayer(value);
                    break;

                case "disconnectPlayer":
                    disconnectPlayer(value);
                    break;

                    static void addNewPlayer(string newPlayerName)
                    {
                        int newPlayerId = MultiplayerManagerServer.connectedPlayers
                            .ToArray()
                            .Length;

                        IPAddress newPlayerIp = MultiplayerManagerServer.connectedIpAddresses[
                            newPlayerId
                        ];
                        string newPlayerIpString = newPlayerIp.ToString();

                        NetworkPlayerData newNetworkPlayerData = new NetworkPlayerData(
                            newPlayerIpString
                        );

                        Player newPlayer = new Player(
                            newPlayerId,
                            newPlayerName,
                            newNetworkPlayerData
                        );

                        MultiplayerManagerServer.connectedPlayers.Add(newPlayer);
                        string serializedConnectedPlayers = Methods.SerializeObject(
                            MultiplayerManagerServer.connectedPlayers
                        );
                        string outgoingMessageWithoutValue =
                            "MultiplayerManager::::::connectedPlayers:::::addNewPlayer::::";
                        outgoingMessage = outgoingMessageWithoutValue + serializedConnectedPlayers;
                    }

                    static void disconnectPlayer(string id)
                    {
                        int indexPlayerToDisconnect = 0;
                        int i = 0;
                        MultiplayerManagerServer.connectedPlayers.ForEach(connectedPlayer =>
                        {
                            if (connectedPlayer.id.ToString() == id)
                            {
                                indexPlayerToDisconnect = i;

                                return;
                            }
                            i++;
                        });
                        MultiplayerManagerServer.connectedPlayers.RemoveAt(indexPlayerToDisconnect);
                        MultiplayerManagerServer.serverToClientClients.RemoveAt(
                            indexPlayerToDisconnect
                        );
                        MultiplayerManagerServer.serverToClientStreams.RemoveAt(
                            indexPlayerToDisconnect
                        );
                        string connectedPlayersSerializedString = Methods.SerializeObject(
                            MultiplayerManagerServer.connectedPlayers
                        );
                        string outgoingMessageWithoutValue =
                            "MultiplayerManager::::::connectedPlayers:::::syncConnectedPlayers::::";
                        outgoingMessage =
                            outgoingMessageWithoutValue + connectedPlayersSerializedString;
                    }
            }
        }
    }
}
