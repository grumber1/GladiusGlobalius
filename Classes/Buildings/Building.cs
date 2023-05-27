using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Building
{
    public string name;
    public int level;
    public float bonusInPercent;
    public int upgradePrice;

    public Building() { }

    //Constructor
    public Building(string newName, int newLevel = 1)
    {
        this.name = newName;
        this.level = newLevel;
        this.bonusInPercent = ((this.level - 1) * 2) / 100;
        this.upgradePrice = this.level * 1000;
    }
}
