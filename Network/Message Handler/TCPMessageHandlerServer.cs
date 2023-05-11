using UnityEngine;
using TMPro;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

public static class TCPMessageHandlerServer
{
    public static void handleMessage(string incomingMessages)
    {
        Debug.Log("Server: incomingMessages: " + incomingMessages);
        string[] messages = incomingMessages.Split(":::::::");
        foreach (string classObjectMethodValue in messages)
            if (classObjectMethodValue != "")
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
                case "addNewPlayer":
                    addNewPlayer(value);
                    break;

                case "disconnectPlayer":
                    disconnectPlayer(value);
                    break;

                    static void addNewPlayer(string newPlayerSerialized)
                    {
                        Player newPlayer = Methods.DeserializeObject<Player>(newPlayerSerialized);

                        string newlyConnectedIp = MultiplayerManagerServer.connectedIpAddresses[
                            0
                        ].ToString();
                        newPlayer.ip = newlyConnectedIp;
                        MultiplayerManagerServer.connectedIpAddresses.Clear();

                        MultiplayerManagerServer.connectedPlayers.Add(newPlayer);
                        string connectedPlayersSerializedString = Methods.SerializeObject(
                            MultiplayerManagerServer.connectedPlayers
                        );

                        MyTCPServer.sendObjectToClients(
                            "MultiplayerManager",
                            "connectedPlayers",
                            "syncConnectedPlayers",
                            MultiplayerManagerServer.connectedPlayers
                        );
                    }

                    static void disconnectPlayer(string playerId)
                    {
                        int playerIndex = Methods.getPlayerIndexByPlayerIdServer(playerId);
                        MultiplayerManagerServer.connectedPlayers.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientClients[playerIndex].Close();
                        MultiplayerManagerServer.serverToClientClients.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientStreams[playerIndex].Close();
                        MultiplayerManagerServer.serverToClientStreams.RemoveAt(playerIndex);

                        MyTCPServer.sendObjectToClients(
                            "MultiplayerManager",
                            "connectedPlayers",
                            "syncConnectedPlayers",
                            MultiplayerManagerServer.connectedPlayers
                        );
                    }
            }
        }
    }
}
