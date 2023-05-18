using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public static class TCPMessageHandlerClient
{
    public static void handleMessage(string incomingMessages)
    {
        string[] messages = incomingMessages.Split(":::::::");
        foreach (string classObjectMethodValue in messages)
        {
            if (classObjectMethodValue != "")
            {
                (string classToCall, string objectToCall, string method, string value) =
                    Methods.resolveMessage(classObjectMethodValue);

                handlePayload(classToCall, objectToCall, method, value);
            }
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

            case "myTCPClient":
                myTCPClient(objectToCall, method, value);
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

                static void connectedPlayers(string method, string value)
                {
                    switch (method)
                    {
                        case "syncConnectedPlayers":
                            syncConnectedPlayers(method, value);
                            break;

                            static void syncConnectedPlayers(
                                string method,
                                string connectedPlayersSerializedString
                            )
                            {
                                Debug.Log(
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + connectedPlayersSerializedString
                                );

                                List<Player> connectedPlayers = Methods.DeserializeObject<
                                    List<Player>
                                >(connectedPlayersSerializedString);

                                MultiplayerManagerClient.connectedPlayers = connectedPlayers;

                                Player playerById = Methods.getPlayerByIdClient(
                                    MultiplayerManagerClient.player.id
                                );
                                MultiplayerManagerClient.player = playerById;
                            }
                        case "disconnectPlayer":
                            disconnectPlayer(method, value);
                            break;

                            static void disconnectPlayer(string method, string notNeededValue)
                            {
                                Debug.Log(
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + notNeededValue
                                );

                                MultiplayerManagerClient.clientToServerStream.Dispose();
                                MultiplayerManagerClient.clientToServerClient.Dispose();
                            }
                    }
                }

            case "startGameButton":
                startGameButton(method, value);
                break;

                static void startGameButton(string method, string value)
                {
                    switch (method)
                    {
                        case "set":
                            set(method, value);
                            break;

                            static void set(string method, string startGameButtonValue)
                            {
                                Debug.Log(
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + startGameButtonValue
                                );

                                if (startGameButtonValue == "true")
                                {
                                    MultiplayerManagerClient.startGameButton = true;
                                }
                            }
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

                static void availableSlaves(string method, string value)
                {
                    switch (method)
                    {
                        case "syncAvailableSlaves":
                            syncAvailableSlaves(method, value);
                            break;

                            static void syncAvailableSlaves(
                                string method,
                                string availableSlavesSerialized
                            )
                            {
                                Debug.Log(
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + availableSlavesSerialized
                                );

                                List<Gladiator> availableSlaves = Methods.DeserializeObject<
                                    List<Gladiator>
                                >(availableSlavesSerialized);

                                MultiplayerSlaveMarketClient.availableSlaves = availableSlaves;
                            }
                    }
                }
        }
    }

    private static void myTCPClient(string objectToCall, string method, string value)
    {
        switch (objectToCall)
        {
            case "messageByteSizeToReceive":
                messageByteSizeToReceive(method, value);
                break;

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
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + byteSizeString
                                );

                                byteSizeString = byteSizeString.Trim();
                                int byteSize = Int32.Parse(byteSizeString);

                                MyTCPClient.byteSizeForMessageToReceive = byteSize;
                                Debug.Log("TCPClient: set byteSize to " + byteSize + " bytes");
                            }
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
                                    "TCP Message Handler Client: calling "
                                        + method
                                        + "."
                                        + byteSizeString
                                );

                                int byteSize = Int32.Parse(byteSizeString);

                                MyTCPClient.byteSizeForMessageToReceive = byteSize;
                            }
                    }
                }
        }
    }
}
