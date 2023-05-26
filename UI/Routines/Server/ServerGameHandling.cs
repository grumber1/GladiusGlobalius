using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameHandling : MonoBehaviour
{
    void FixedUpdate()
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
            fillSlaveMarketAndSyncWithPlayers();

            MultiplayerManagerServer.day += 1;
            MyTCPServer.sendMessageToClients(
                Messages.Server.MultiplayerManager.Day.set,
                MultiplayerManagerServer.day + ""
            );
        }
    }

    private void fillSlaveMarketAndSyncWithPlayers()
    {
        MultiplayerSlaveMarketServer.availableSlaves.Clear();
        for (int i = 0; i < 5; i++)
        {
            Gladiator gladiator = Methods.createNewGladiator(
                GladiatorNameGenerator.generateGladiatorName(),
                75,
                10,
                Weapons.fist,
                Weapons.knife
            );
            MultiplayerSlaveMarketServer.availableSlaves.Add(gladiator);
        }

        MyTCPServer.sendObjectToClients(
            Messages.Server.MultiplayerSlaveMarket.AvailableSlaves.syncAvailableSlaves,
            MultiplayerSlaveMarketServer.availableSlaves
        );
    }
}
