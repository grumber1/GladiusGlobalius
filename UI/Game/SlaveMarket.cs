using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlaveMarket : MonoBehaviour
{
    public TMP_Dropdown availableSlaves;

    void Start() { }

    void Update()
    {
        List<string> availableSlavesString = getAvailableSlaves();
        availableSlaves.AddOptions(availableSlavesString);
    }

    public void onClickBuy()
    {
        string gladiatorToBuyId = MultiplayerSlaveMarketClient.availableSlaves[
            availableSlaves.value
        ].id;

        MyTCPClient.sendMessageToServer(
            Messages.Client.MultiplayerSlaveMarket.AvailableSlaves.buySlave,
            MultiplayerManagerClient.player.id + "::" + gladiatorToBuyId
        );
    }

    private List<string> getAvailableSlaves()
    {
        List<string> availableSlavesString = new List<string>();

        MultiplayerSlaveMarketClient.availableSlaves.ForEach(
            (availableSlave) =>
            {
                availableSlavesString.Add(availableSlave.name);
            }
        );

        availableSlaves.ClearOptions();

        return availableSlavesString;
    }
}
