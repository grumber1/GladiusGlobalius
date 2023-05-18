using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public static class Messages
{
    public static class Server
    {
        public static class MultiplayerManager
        {
            public static class ConnectedPlayers
            {
                public static string syncConnectedPlayers =
                    "multiplayerManager.connectedPlayers.syncConnectedPlayers";
                public static string disconnect =
                    "multiplayerManager.connectedPlayers.disconnectPlayer";
            }

            public static class StartGameButton
            {
                public static string set = "multiplayerManager.startGameButton.set";
            }
        }

        public static class MultiplayerSlaveMarket
        {
            public static class AvailableSlaves
            {
                public static string syncAvailableSlaves =
                    "multiplayerSlaveMarket.availableSlaves.syncAvailableSlaves";
            }
        }
    }

    public static class Client
    {
        public static class MultiplayerManager
        {
            public static class ConnectedPlayers
            {
                public static string disconnectPlayer =
                    "multiplayerManager.connectedPlayers.disconnectPlayer";

                public static string addNewPlayer =
                    "multiplayerManager.connectedPlayers.addNewPlayer";
            }
        }

        public static class MultiplayerSlaveMarket
        {
            public static class AvailableSlaves
            {
                public static string buySlave = "multiplayerSlaveMarket.availableSlaves.buySlave";
            }
        }
    }

    //
}
