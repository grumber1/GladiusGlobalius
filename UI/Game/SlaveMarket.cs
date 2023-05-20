using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlaveMarket : MonoBehaviour
{
    public TMP_Dropdown availableSlaves;
    public TMP_Text gladiatorDetailsText;
    public int previouslyAvailableSlaves = 0;

    void Start() { }

    void Update()
    {
        fillAvailableSlavesInDropDownIfChangesOccur();
        getGladiatorDetailsText();
    }

    private void getGladiatorDetailsText()
    {
        string gladiatorDetails = "";
        if (MultiplayerSlaveMarketClient.availableSlaves.ToArray().Length != 0)
        {
            {
                int dropdownIndex = availableSlaves.value;
                Gladiator selectedGladiator = MultiplayerSlaveMarketClient.availableSlaves[
                    dropdownIndex
                ];

                string selectedGladiatorName = selectedGladiator.name;
                string selectedGladiatorOwnedBy = selectedGladiator.ownedBy;
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

    public void onClickBuy()
    {
        if (MultiplayerSlaveMarketClient.availableSlaves.ToArray().Length != 0)
        {
            string gladiatorToBuyId = MultiplayerSlaveMarketClient.availableSlaves[
                availableSlaves.value
            ].id;

            MyTCPClient.sendMessageToServer(
                Messages.Client.MultiplayerSlaveMarket.AvailableSlaves.buySlave,
                MultiplayerManagerClient.player.id + "::" + gladiatorToBuyId
            );
        }
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
