using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Server : MonoBehaviour
{
    public TMP_Text terminal;
    public TMP_Text serverInfo;
    public double connectedPlayersCount;

    void BeforeStart() { }

    // Update is called once per frame
    void Update()
    {
        terminal.text = MyTCPServer.content;
        connectedPlayersCount = MultiplayerManagerServer.connectedPlayers.ToArray().Length;
        string connectedPlayersText = "ConnectedPlayers: " + connectedPlayersCount;
        string serverInfoText = "Server:\n" + connectedPlayersText;
        serverInfo.text = serverInfoText;
    }
}
