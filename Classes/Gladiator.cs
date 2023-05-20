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
    public string ownedBy;

    public Gladiator() { }

    //Constructor
    public Gladiator(
        string newId,
        string newName,
        GladiatorBody newBody,
        GladiatorAttributes newAttributes,
        GladiatorWeapons newWeapons,
        string newOwnedBy = "slaveTrader"
    )
    {
        this.id = newId;
        this.name = newName.Replace("\r", "").Replace("\n", "");
        this.body = newBody;
        this.attributes = newAttributes;
        this.weapons = newWeapons;
        this.ownedBy = newOwnedBy;
    }
}
