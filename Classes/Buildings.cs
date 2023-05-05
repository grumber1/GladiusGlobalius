using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings
{
    public WoodChuck woodChuck = new WoodChuck();
    public GoldMine goldMine = new GoldMine();
}

public class WoodChuck
{
    public int level = 1;
    public int income;
    public int upgradeCost; 
    public WoodChuckUpgradeCosts woodChuckUpgradeCosts = new WoodChuckUpgradeCosts();
    public WoodChuck(){
        income = this.level * 1;
        upgradeCost = this.level * 50 * this.level;
    }

    public void incomeCalculator(){
        this.income = this.level * 1;
        this.upgradeCost = this.level * 50 * this.level;
    }
}

public class WoodChuckUpgradeCosts{
    public int wood;
    public int gold;
}

public class GoldMine
{
    public int level = 1;
    public int income;
    public int upgradeCost; 
    public GoldMine(){
        income = this.level * this.level;
        upgradeCost = this.level * 50;
    }
}

