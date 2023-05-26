using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameHandling : MonoBehaviour
{
    void Start()
    {
        fillSlaveMarketAndSyncWithPlayers();
    }

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
            dailyRoutine();

            MultiplayerManagerServer.day += 1;
            MyTCPServer.sendMessageToClients(
                Messages.Server.MultiplayerManager.Day.set,
                MultiplayerManagerServer.day + ""
            );
        }
    }

    private void dailyRoutine()
    {
        fillSlaveMarketAndSyncWithPlayers();
        calculatePlayerGoldAndSyncWithPlayers();
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

        TCPMessageHandlerServer.syncAvailableSlaves();
    }

    private void calculatePlayerGoldAndSyncWithPlayers()
    {
        MultiplayerManagerServer.connectedPlayers.ForEach(
            (connectedPlayer) =>
            {
                connectedPlayer.gold += (int)(100 + 100 * connectedPlayer.mine.bonusInPercent);
            }
        );
        TCPMessageHandlerServer.syncPlayers();
    }
}
