using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Weapon
{
    public double damage;
    public string name;

    public Weapon() { }

    //Constructor
    public Weapon(string newName, double newDamage)
    {
        this.name = newName;
        this.damage = newDamage;
    }
}
