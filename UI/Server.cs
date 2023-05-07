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
    public TMP_Text terminal;
    public TMP_Text serverInfo;
    public double connectedPlayersCount;

    void BeforeStart() { }

    // Update is called once per frame
    void Update()
    {
        terminal.text = MyTCPServer.content.Replace("\r", "\n").Replace("\n\n", "\n");
        connectedPlayersCount = MultiplayerManagerServer.connectedPlayers.ToArray().Length;
        string connectedPlayersText = "ConnectedPlayers: " + connectedPlayersCount;
        string serverInfoText = "Server:\n" + connectedPlayersText;
        serverInfo.text = serverInfoText;
    }

    public void onClickStartGame()
    {
        string messageToSend = Methods.convertMessage(
            "MultiplayerManager",
            "startGameButton",
            "set",
            "true"
        );
        MyTCPServer.sendMessage(messageToSend);
    }
}
