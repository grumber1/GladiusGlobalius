using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public GameObject LobbyGO;
    public GameObject GameGO;

    public TMP_Text connectedPlayersText;
    public Button StartGameButton;

    void Start()
    {
        if (!MultiplayerManagerClient.isServer)
        {
            StartGameButton.interactable = false;
        }
    }

    void Update()
    {
        updateConnectedPlayers();
        if (MultiplayerManagerClient.startGameButton == true)
        {
            MyTCPServer.serverWaitingForNewConnections = false;
            Methods.switchScreen(LobbyGO, GameGO);
        }
    }

    public void onClickStartGame()
    {
        MyTcpClient.sendToNetwork("MultiplayerManager.startGameButton.set;" + "true" + "$$$");
        Methods.switchScreen(LobbyGO, GameGO);
    }

    private void updateConnectedPlayers()
    {
        string connectedPlayersString = "";
        long ping = Methods.pingHostTime(MultiplayerManagerClient.remoteIp);
        MultiplayerManagerClient.connectedPlayers.ForEach(
            (connectedPlayer) =>
            {
                connectedPlayersString +=
                    connectedPlayer.name + " " + connectedPlayer.ip + " " + ping;
            }
        );
        connectedPlayersText.text = connectedPlayersString;
    }
}
