using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Player
{
    public string id;
    public string name;
    public string ip;
    public int gold;
    public List<Gladiator> ownedGladiators = new List<Gladiator>();
    public bool readyForNextRound = false;
    public PlayerBuildings playerBuildings = new PlayerBuildings();

    public Player() { }

    //Constructor
    public Player(string newId, string newName, int newGold = 3000)
    {
        id = newId;
        name = newName.Replace("\r", "").Replace("\n", "");
        gold = newGold;
    }
}
