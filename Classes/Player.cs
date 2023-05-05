using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player
{
    public int id;
    public string name;
    public string ip;

    // public Buildings buildings = new Buildings();
    // public Crew crew = new Crew();

    public Player() { }

    //Constructor
    public Player(int newId, string newName, string newIp)
    {
        id = newId;
        name = newName;
        ip = newIp;
    }
}
