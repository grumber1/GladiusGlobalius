using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GladiatorWeapons
{
    public Weapon leftArmWeapon;
    public Weapon rightArmWeapon;

    public GladiatorWeapons() { }

    //Constructor
    public GladiatorWeapons(Weapon newLeftArmWeapon, Weapon newRightArmWeapon)
    {
        this.leftArmWeapon = newLeftArmWeapon;
        this.rightArmWeapon = newRightArmWeapon;
    }
}
