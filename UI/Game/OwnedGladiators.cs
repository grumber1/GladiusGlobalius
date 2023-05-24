using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnedGladiators : MonoBehaviour
{
    public TMP_Dropdown availableGladiators;
    public TMP_Text gladiatorDetailsText;
    public int previouslyAvailableGladiators = 0;

    void Start()
    {
        availableGladiators.ClearOptions();
    }

    void Update()
    {
        fillAvailableGladiatorsInDropDownIfChangesOccur();
        fillGladiatorDetails();
    }

    private void fillGladiatorDetails()
    {
        string gladiatorDetails = "";
        if (MultiplayerManagerClient.player.ownedGladiators.ToArray().Length != 0)
        {
            {
                int dropdownIndex = availableGladiators.value;
                Gladiator selectedGladiator = MultiplayerManagerClient.player.ownedGladiators[
                    dropdownIndex
                ];

                string selectedGladiatorName = selectedGladiator.name;
                string selectedGladiatorOwnedBy = selectedGladiator.ownedByName;
                string selectedGladiatorLeftHandWeapon =
                    selectedGladiator.weapons.leftArmWeapon.name
                    + " - Damage: "
                    + selectedGladiator.weapons.leftArmWeapon.damage;
                string selectedGladiatorRightHandWeapon =
                    selectedGladiator.weapons.rightArmWeapon.name
                    + " - Damage: "
                    + selectedGladiator.weapons.rightArmWeapon.damage;
                string selectedGladiatorHealthBody =
                    selectedGladiator.body.body + "/" + selectedGladiator.body.bodyFull;
                string selectedGladiatorHealthNeck =
                    selectedGladiator.body.neck + "/" + selectedGladiator.body.neckFull;
                string selectedGladiatorHealthHead =
                    selectedGladiator.body.head + "/" + selectedGladiator.body.headFull;
                string selectedGladiatorHealthLeftArm =
                    selectedGladiator.body.leftArm + "/" + selectedGladiator.body.leftArmFull;
                string selectedGladiatorHealthRightArm =
                    selectedGladiator.body.rightArm + "/" + selectedGladiator.body.rightArmFull;
                string selectedGladiatorHealthLeftLeg =
                    selectedGladiator.body.leftLeg + "/" + selectedGladiator.body.leftLegFull;
                string selectedGladiatorHealthRightLeg =
                    selectedGladiator.body.rightLegFull + "/" + selectedGladiator.body.rightLegFull;
                string selectedGladiatorAgility = selectedGladiator.attributes.agility + "";
                string selectedGladiatorStrength = selectedGladiator.attributes.strength + "";
                string selectedGladiatorAttack = selectedGladiator.attributes.attack + "";
                string selectedGladiatorDefense = selectedGladiator.attributes.defense + "";

                gladiatorDetails +=
                    selectedGladiatorName
                    + "\n"
                    + Methods.firstLetterToUppercase(selectedGladiatorOwnedBy)
                    + "\n"
                    + Methods.firstLetterToUppercase(selectedGladiatorLeftHandWeapon)
                    + "\n"
                    + Methods.firstLetterToUppercase(selectedGladiatorRightHandWeapon)
                    + "\n"
                    + selectedGladiatorHealthBody
                    + "\n"
                    + selectedGladiatorHealthNeck
                    + "\n"
                    + selectedGladiatorHealthHead
                    + "\n"
                    + selectedGladiatorHealthLeftArm
                    + "\n"
                    + selectedGladiatorHealthRightArm
                    + "\n"
                    + selectedGladiatorHealthLeftLeg
                    + "\n"
                    + selectedGladiatorHealthRightLeg
                    + "\n"
                    + selectedGladiatorAgility
                    + "\n"
                    + selectedGladiatorStrength
                    + "\n"
                    + selectedGladiatorAttack
                    + "\n"
                    + selectedGladiatorDefense;
            }
        }
        gladiatorDetailsText.text = gladiatorDetails;
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
