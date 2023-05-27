using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Weapon
{
    public string name;
    public int damage;

    public Weapon() { }

    //Constructor
    public Weapon(string newName, int newDamage)
    {
        this.name = newName;
        this.damage = newDamage;
    }
}
