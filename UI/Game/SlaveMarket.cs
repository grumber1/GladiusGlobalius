using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlaveMarket : MonoBehaviour
{
    public TMP_Dropdown availableSlaves;
    public int previouslyAvailableSlaves = 0;

    void Start() { }

    void Update()
    {
        fillAvailableSlavesInDropDownIfChangesOccur();
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

    private void fillAvailableSlavesInDropDownIfChangesOccur()
    {
        if (
            previouslyAvailableSlaves
            != MultiplayerSlaveMarketClient.availableSlaves.ToArray().Length
        )
        {
            List<string> availableSlavesString = new List<string>();

            MultiplayerSlaveMarketClient.availableSlaves.ForEach(
                (availableSlave) =>
                {
                    availableSlavesString.Add(availableSlave.name);
                }
            );
            previouslyAvailableSlaves = availableSlavesString.ToArray().Length;
            availableSlaves.ClearOptions();
            availableSlaves.AddOptions(availableSlavesString);
        }
    }
}
