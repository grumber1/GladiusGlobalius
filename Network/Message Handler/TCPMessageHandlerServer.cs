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
            case "MultiplayerManager":
                multiplayerManager(objectToCall, method, value);
                break;

            case "MyTCPServer":
                myTCPServer(objectToCall, method, value);
                break;

            case "MultiplayerSlaveMarket":
                multiplayerSlaveMarket(objectToCall, method, value);
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

                        MultiplayerManagerServer.clientHandlingThreads.RemoveAt(playerIndex);

                        MultiplayerManagerServer.connectedPlayers.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientClients[playerIndex].Dispose();
                        MultiplayerManagerServer.serverToClientClients.RemoveAt(playerIndex);

                        MultiplayerManagerServer.serverToClientStreams[playerIndex].Dispose();
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

    private static void myTCPServer(string objectToCall, string method, string value)
    {
        switch (objectToCall)
        {
            case "MessageByteSizeToReceive":
                messageByteSizeToReceive(method, value);
                break;
        }

        static void messageByteSizeToReceive(string method, string value)
        {
            switch (method)
            {
                case "set":
                    setMessageByteSize(value);
                    break;

                    static void setMessageByteSize(string byteSizeString)
                    {
                        byteSizeString = byteSizeString.Trim();
                        int byteSize = Int32.Parse(byteSizeString);

                        MyTCPServer.byteSizeForMessageToReceive = byteSize;
                        Debug.Log("TCPServer: set byteSize to " + byteSize + " bytes");
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
                    buySlave(value);
                    break;

                    static void buySlave(string gladiatorIdAndPlayerId)
                    {
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
                        MultiplayerSlaveMarketServer.availableSlaves.Remove(foundGladiator);

                        MyTCPServer.sendObjectToClients(
                            "MultiplayerManager",
                            "connectedPlayers",
                            "syncConnectedPlayers",
                            MultiplayerManagerServer.connectedPlayers
                        );

                        MyTCPServer.sendObjectToClients(
                            "MultiplayerSlaveMarket",
                            "availableSlaves",
                            "syncAvailableSlaves",
                            MultiplayerSlaveMarketServer.availableSlaves
                        );
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
                    setMessageByteSize(value);
                    break;

                    static void setMessageByteSize(string byteSizeString)
                    {
                        int byteSize = Int32.Parse(byteSizeString);

                        MyTCPClient.byteSizeForMessageToReceive = byteSize;
                    }
            }
        }
    }
}
