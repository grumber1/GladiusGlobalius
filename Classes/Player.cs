using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player
{
    public string id;
    public string name;
    public string ip;

    // public Buildings buildings = new Buildings();
    // public Crew crew = new Crew();

    public Player() { }

    //Constructor
    public Player(string newId, string newName)
    {
        id = newId;
        name = newName;
    }
}
