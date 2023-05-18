using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class runningServer : MonoBehaviour
{
    void Start()
    {
        fillSlaveMarket();
    }

    void Update() { }

    private void fillSlaveMarket()
    {
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
