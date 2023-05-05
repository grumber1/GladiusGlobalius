using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber
{
    
    public string belongsTo;
    public CrewMember Pilot;
    public CrewMember Navigator;
    public CrewMember Funker;
    public CrewMember Mechaniker;
    public CrewMember TopGunner;
    public CrewMember BallGunner;
    public CrewMember LeftGunner;
    public CrewMember RightGunner;  
    public CrewMember BackGunner; 
    public int health = 100;
    public CrewMember[] crewMembers = new CrewMember[9];

    public Bomber(string playerName) {
        belongsTo = playerName;
    }
}
