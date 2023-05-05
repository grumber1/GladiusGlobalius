using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player
{
    public int id;
    public string name;
    public NetworkPlayerData networkPlayerData;

    // public Buildings buildings = new Buildings();
    // public Crew crew = new Crew();

    public Player() { }

    //Constructor
    public Player(int newId, string newName, NetworkPlayerData newNetworkPlayerData)
    {
        id = newId;
        name = newName;
        networkPlayerData = newNetworkPlayerData;
    }
}
