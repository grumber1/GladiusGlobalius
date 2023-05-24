using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Gladiator
{
    public string id;
    public string name;
    public GladiatorBody body;
    public GladiatorAttributes attributes;
    public GladiatorWeapons weapons;
    public string ownedById;
    public string ownedByName;

    public Gladiator() { }

    //Constructor
    public Gladiator(
        string newId,
        string newName,
        GladiatorBody newBody,
        GladiatorAttributes newAttributes,
        GladiatorWeapons newWeapons,
        string newOwnedById = "000",
        string newOwnedByName = "slaveTrader"
    )
    {
        this.id = newId;
        this.name = newName.Replace("\r", "").Replace("\n", "");
        this.body = newBody;
        this.attributes = newAttributes;
        this.weapons = newWeapons;
        this.ownedById = newOwnedById;
        this.ownedByName = newOwnedByName;
    }
}
