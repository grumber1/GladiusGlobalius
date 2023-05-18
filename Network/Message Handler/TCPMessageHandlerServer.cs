using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;

public static class TCPMessageHandlerServer
{
    public static void handleMessage(string incomingMessages)
    {
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
            case "multiplayerManager":
                multiplayerManager(objectToCall, method, value);
                break;

            case "multiplayerSlaveMarket":
                multiplayerSlaveMarket(objectToCall, method, value);
                break;

            case "myTCPServer":
                myTCPServer(objectToCall, method, value);
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
                    addNewPlayer(method, value);
                    break;

                case "disconnectPlayer":
                    disconnectPlayer(method, value);
                    break;

                    static void addNewPlayer(string method, string newPlayerSerialized)
                    {
                        Debug.Log(
                            "TCP Message Handler Client: calling "
                                + method
                                + "."
                                + newPlayerSerialized
                        );

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
                            Messages
                                .Server
                                .MultiplayerManager
                                .ConnectedPlayers
                                .syncConnectedPlayers,
                            MultiplayerManagerServer.connectedPlayers
                        );
                    }

                    static void disconnectPlayer(string method, string playerId)
                    {
                        Debug.Log("TCP Message Handler Client: calling " + method + "." + playerId);

                        int playerIndex = Methods.getPlayerIndexByPlayerIdServer(playerId);

                        MultiplayerManagerServer.clientHandlingThreads.RemoveAt(playerIndex);

                        MultiplayerManagerServer.connectedPlayers.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientClients[playerIndex].Dispose();
                        MultiplayerManagerServer.serverToClientClients.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientStreams[playerIndex].Dispose();
                        MultiplayerManagerServer.serverToClientStreams.RemoveAt(playerIndex);

                        MyTCPServer.sendObjectToClients(
                            Messages
                                .Server
                                .MultiplayerManager
                                .ConnectedPlayers
                                .syncConnectedPlayers,
                            MultiplayerManagerServer.connectedPlayers
                        );
                    }
            }
        }
    }

    private static void multiplayerSlaveMarket(string objectToCall, string method, string value)
    {
        switch (objectToCall)
        {
            case "availableSlaves":
                availableSlaves(method, value);
                break;
        }

        static void availableSlaves(string method, string value)
        {
            switch (method)
            {
                case "buySlave":
                    buySlave(method, value);
                    break;

                    static void buySlave(string method, string gladiatorIdAndPlayerId)
                    {
                        Debug.Log(
                            "TCP Message Handler Client: calling "
                                + method
                                + "."
                                + gladiatorIdAndPlayerId
                        );

                        string playerId = gladiatorIdAndPlayerId.Split("::")[0];
                        string gladiatorId = gladiatorIdAndPlayerId.Split("::")[1];

                        Player foundPlayer =
                            MultiplayerManagerServer.connectedPlayers.FirstOrDefault(
                                player => player.id == playerId
                            );

                        Gladiator foundGladiator =
                            MultiplayerSlaveMarketServer.availableSlaves.FirstOrDefault(
                                gladiator => gladiator.id == gladiatorId
                            );

                        // Set OwnerId for selected Gladiator
                        foundGladiator.ownedBy = playerId;
                        foundPlayer.ownedGladiators.Add(foundGladiator);
                        Debug.Log(
                            "1: " + MultiplayerSlaveMarketServer.availableSlaves.ToArray().Length
                        );
                        MultiplayerSlaveMarketServer.availableSlaves.Remove(foundGladiator);
                        Debug.Log(
                            "2: " + MultiplayerSlaveMarketServer.availableSlaves.ToArray().Length
                        );

                        MyTCPServer.sendObjectToClients(
                            Messages
                                .Server
                                .MultiplayerManager
                                .ConnectedPlayers
                                .syncConnectedPlayers,
                            MultiplayerManagerServer.connectedPlayers
                        );

                        MyTCPServer.sendObjectToClients(
                            Messages
                                .Server
                                .MultiplayerSlaveMarket
                                .AvailableSlaves
                                .syncAvailableSlaves,
                            MultiplayerSlaveMarketServer.availableSlaves
                        );
                    }
            }
        }
    }

    private static void myTCPServer(string objectToCall, string method, string value)
    {
        switch (objectToCall)
        {
            case "messageByteSizeToReceive":
                messageByteSizeToReceive(method, value);
                break;
        }

        static void messageByteSizeToReceive(string method, string value)
        {
            switch (method)
            {
                case "set":
                    setMessageByteSize(method, value);
                    break;

                    static void setMessageByteSize(string method, string byteSizeString)
                    {
                        Debug.Log(
                            "TCP Message Handler Client: calling " + method + "." + byteSizeString
                        );

                        byteSizeString = byteSizeString.Trim();
                        int byteSize = Int32.Parse(byteSizeString);

                        MyTCPServer.byteSizeForMessageToReceive = byteSize;
                        Debug.Log("TCPServer: set byteSize to " + byteSize + " bytes");
                    }
            }
        }
    }

    private static void defaultSetup(string objectToCall, string method, string value)
    {
        switch (objectToCall)
        {
            case "objectToCall":
                messageByteSizeToReceive(method, value);
                break;
        }

        static void messageByteSizeToReceive(string method, string value)
        {
            switch (method)
            {
                case "method":
                    setMessageByteSize(method, value);
                    break;

                    static void setMessageByteSize(string method, string byteSizeString)
                    {
                        Debug.Log(
                            "TCP Message Handler Client: calling " + method + "." + byteSizeString
                        );

                        int byteSize = Int32.Parse(byteSizeString);

                        MyTCPClient.byteSizeForMessageToReceive = byteSize;
                    }
            }
        }
    }
}
