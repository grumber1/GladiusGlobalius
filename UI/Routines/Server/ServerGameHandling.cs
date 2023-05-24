using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameHandling : MonoBehaviour
{
    public GameObject DailyRoutineServerGO;

    void Update()
    {
        checkForNextDay();
    }

    private void checkForNextDay()
    {
        bool nextDay = true;
        MultiplayerManagerServer.connectedPlayers.ForEach(
            (connectedPlayer) =>
            {
                if (connectedPlayer.readyForNextRound == false)
                {
                    nextDay = false;
                }
            }
        );

        if (nextDay == true)
        {
            MultiplayerManagerServer.connectedPlayers.ForEach(
                (connectedPlayer) =>
                {
                    connectedPlayer.readyForNextRound = false;
                }
            );
            DailyRoutineServerGO.SetActive(false);
            DailyRoutineServerGO.SetActive(true);

            MultiplayerManagerServer.day += 1;
            MyTCPServer.sendMessageToClients(
                Messages.Server.MultiplayerManager.Day.set,
                MultiplayerManagerServer.day + ""
            );
        }
    }
}
