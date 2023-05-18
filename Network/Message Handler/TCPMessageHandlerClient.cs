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
            case "MultiplayerManager":
                multiplayerManager(objectToCall, method, value);
                break;

            case "MultiplayerSlaveMarket":
                multiplayerSlaveMarket(objectToCall, method, value);
                break;

            case "MyTCPClient":
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

            case "startGameButton":
                startGameButton(method, value);
                break;
        }

        static void connectedPlayers(string method, string value)
        {
            switch (method)
            {
                case "syncConnectedPlayers":
                    syncConnectedPlayers(value);
                    break;

                    static void syncConnectedPlayers(string connectedPlayersSerializedString)
                    {
                        List<Player> connectedPlayers = Methods.DeserializeObject<List<Player>>(
                            connectedPlayersSerializedString
                        );

                        MultiplayerManagerClient.connectedPlayers = connectedPlayers;

                        Player playerById = Methods.getPlayerByIdClient(
                            MultiplayerManagerClient.player.id
                        );
                        MultiplayerManagerClient.player = playerById;
                    }
            }
        }

        static void startGameButton(string method, string value)
        {
            switch (method)
            {
                case "set":
                    set(value);
                    break;

                    static void set(string value)
                    {
                        if (value == "true")
                        {
                            MultiplayerManagerClient.startGameButton = true;
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
        }

        static void availableSlaves(string method, string value)
        {
            switch (method)
            {
                case "sync":
                    syncAvailableSlaves(value);
                    break;

                    static void syncAvailableSlaves(string availableSlavesSerialized)
                    {
                        List<Gladiator> availableSlaves = Methods.DeserializeObject<
                            List<Gladiator>
                        >(availableSlavesSerialized);

                        MultiplayerSlaveMarketClient.availableSlaves = availableSlaves;
                    }
            }
        }
    }

    private static void myTCPClient(string objectToCall, string method, string value)
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

                        MyTCPClient.byteSizeForMessageToReceive = byteSize;
                        Debug.Log("TCPClient: set byteSize to " + byteSize + " bytes");
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
