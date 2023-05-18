using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class runningServer : MonoBehaviour
{
    void Start()
    // Fill Slave Market and send to network
    {
        for (int i = 0; i < 5; i++)
        {
            Gladiator gladiator = Methods.createNewGladiator(
                "RusselCrow",
                75,
                10,
                Weapons.fist,
                Weapons.knife
            );
            MultiplayerSlaveMarketServer.availableSlaves.Add(gladiator);
        }

        MyTCPServer.sendObjectToClients(
            "MultiplayerSlaveMarket",
            "availableSlaves",
            "sync",
            MultiplayerSlaveMarketServer.availableSlaves
        );
    }

    //

    // Update is called once per frame
    void Update() { }
}
