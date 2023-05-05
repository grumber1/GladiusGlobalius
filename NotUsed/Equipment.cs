using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Armor
{
    public int id;
    public string gladiatorName;
    public int health;
    public int armor;
    public int defense;
    public int strength;
    public int agility;
    public int stamina;
    public int maneuverability;

    //Constructor
    public Armor(int newId, string newGladiatorName, int newHealth, int newArmor, int newDefense, int newStrength, int newAgility, int newStamina, int newManeuverability)
    {
        id = newId;
        gladiatorName = newGladiatorName;
        health = newHealth;
        armor = newArmor;
        defense = newDefense;
        strength = newStrength;
        agility = newAgility;
        stamina = newStamina;
        maneuverability = newManeuverability;
    }
}

public class Hero
{
  public int id;
  public string name;
  public int health;
  public int armor;
  public int defense;
  public int strength;
  public int agility;
  public int stamina;
  public int maneuverability;

  //Constructor
  public Hero(int newId, string newGladiatorName, int newHealth, int newArmor, int newDefense, int newStrength, int newAgility, int newStamina, int newManeuverability)
  {
    id = newId;
    name = newGladiatorName;
    health = newHealth;
    armor = newArmor;
    defense = newDefense;
    strength = newStrength;
    agility = newAgility;
    stamina = newStamina;
    maneuverability = newManeuverability;
  }

}



