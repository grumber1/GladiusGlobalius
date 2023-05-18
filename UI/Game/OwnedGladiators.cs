using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnedGladiators : MonoBehaviour
{
    public TMP_Dropdown availableGladiators;

    void Start() { }

    void Update()
    {
        List<string> availableSlavesString = getAvailableGladiators();
        availableGladiators.AddOptions(availableSlavesString);
    }

    private List<string> getAvailableGladiators()
    {
        List<string> availableGladiatorsString = new List<string>();

        MultiplayerManagerClient.player.ownedGladiators.ForEach(
            (availableGladiator) =>
            {
                availableGladiatorsString.Add(availableGladiator.name);
            }
        );

        availableGladiators.ClearOptions();

        return availableGladiatorsString;
    }
}
