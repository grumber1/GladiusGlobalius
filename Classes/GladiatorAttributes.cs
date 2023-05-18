using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GladiatorAttributes
{
    public double agility;
    public double strength;
    public double attack;
    public double defense;

    public GladiatorAttributes() { }

    //Constructor
    public GladiatorAttributes(
        double newAgility = 10,
        double newStrength = 10,
        double newAttack = 10,
        double newDefense = 10
    )
    {
        this.agility = newAgility;
        this.strength = newStrength;
        this.attack = newAttack;
        this.defense = newDefense;
    }
}
