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

    void Update()
    {
        updateConnectedPlayers();
        if (MultiplayerManagerClient.startGameButton == true)
        {
            Methods.switchScreen(LobbyGO, GameGO);
        }
    }

    private void updateConnectedPlayers()
    {
        string connectedPlayersString = "";
        long ping = Methods.pingHostTime(MultiplayerManagerClient.remoteIp);
        MultiplayerManagerClient.connectedPlayers.ForEach(
            (connectedPlayer) =>
            {
                connectedPlayersString += connectedPlayer.name + " " + ping + "\n";
            }
        );
        connectedPlayersText.text = connectedPlayersString;
    }
}
