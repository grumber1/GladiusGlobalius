using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Server : MonoBehaviour
{
    public GameObject ServerGO;
    public GameObject GameGO;
    public GameObject runningServerGO;
    public GameObject startOfANewGame;
    public TMP_Text terminal;
    public TMP_Text serverInfo;
    public double connectedPlayersCount;

    void BeforeStart() { }

    void Update()
    {
        terminal.text = MyTCPServer.content.Replace("\r", "\n").Replace("\n\n", "\n");
        connectedPlayersCount = MultiplayerManagerServer.connectedPlayers.ToArray().Length;

        string serverInfoText = "Server: \n";
        string connectedPlayersText = "ConnectedPlayers: " + connectedPlayersCount + "\n";
        string clientHandlingThreadsText =
            "Client Handling Threads: "
            + MultiplayerManagerServer.clientHandlingThreads.ToArray().Length
            + "\n";
        string serverToClientClientsText =
            "Clients: " + MultiplayerManagerServer.serverToClientClients.ToArray().Length + "\n";
        string serverToClientStreamsText =
            "Streams: " + MultiplayerManagerServer.serverToClientStreams.ToArray().Length + "\n";

        string content =
            serverInfoText
            + connectedPlayersText
            + clientHandlingThreadsText
            + serverToClientClientsText
            + serverToClientStreamsText;
        serverInfo.text = content.Replace("\r", "\n").Replace("\n\n", "\n");
        ;
    }

    public void onClickStartGame()
    {
        MyTCPServer.sendMessageToClients(
            Messages.Server.MultiplayerManager.StartGameButton.set,
            "true"
        );
        startOfANewGame.SetActive(true);
        runningServerGO.SetActive(true);
    }
}
