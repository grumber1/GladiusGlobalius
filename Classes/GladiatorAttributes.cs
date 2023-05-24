using System;
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
    public GladiatorAttributes(double baseAttributes)
    {
        int minValue = 50;
        int maxValue = 200;

        double randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.agility = Math.Round((baseAttributes) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.strength = Math.Round((baseAttributes) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.attack = Math.Round((baseAttributes) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.defense = Math.Round((baseAttributes) * randomNumber, 2);
    }
}
