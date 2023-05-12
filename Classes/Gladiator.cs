using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Gladiator
{
    public string id;
    public string name;
    public int health;
    public int attack;
    public int defense;

    public Gladiator() { }

    //Constructor
    public Gladiator(string newId, string newName)
    {
        id = newId;
        name = newName;
    }
}
