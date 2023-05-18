using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnedGladiators : MonoBehaviour
{
    public TMP_Dropdown availableGladiators;
    public int previouslyAvailableGladiators = 0;

    void Start()
    {
        availableGladiators.ClearOptions();
    }

    void Update()
    {
        fillAvailableGladiatorsInDropDownIfChangesOccur();
    }

    private void fillAvailableGladiatorsInDropDownIfChangesOccur()
    {
        if (
            previouslyAvailableGladiators
            != MultiplayerManagerClient.player.ownedGladiators.ToArray().Length
        )
        {
            List<string> availableGladiatorsString = new List<string>();

            MultiplayerManagerClient.player.ownedGladiators.ForEach(
                (availableGladiator) =>
                {
                    availableGladiatorsString.Add(availableGladiator.name);
                }
            );

            previouslyAvailableGladiators = availableGladiatorsString.ToArray().Length;
            availableGladiators.ClearOptions();
            availableGladiators.AddOptions(availableGladiatorsString);
        }
    }
}
