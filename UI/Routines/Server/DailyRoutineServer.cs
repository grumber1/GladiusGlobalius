using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRoutineServer : MonoBehaviour
{
    void Start()
    {
        Debug.Log("DailyRoutineServer Active");
        fillSlaveMarketAndSyncWithPlayers();
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
