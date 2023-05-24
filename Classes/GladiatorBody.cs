using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GladiatorBody
{
    public double body;
    public double neck;
    public double head;
    public double leftArm;
    public double rightArm;
    public double leftLeg;
    public double rightLeg;

    //
    public double bodyFull;
    public double neckFull;
    public double headFull;
    public double leftArmFull;
    public double rightArmFull;
    public double leftLegFull;
    public double rightLegFull;

    public GladiatorBody() { }

    //Constructor
    public GladiatorBody(double baseHealth = 75)
    {
        int minValue = 90;
        int maxValue = 110;

        double randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.body = Math.Round(baseHealth * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.neck = Math.Round((baseHealth / 15) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.head = Math.Round((baseHealth / 5) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.leftArm = Math.Round((baseHealth / 3.75) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.rightArm = Math.Round((baseHealth / 3.75) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.leftLeg = Math.Round((baseHealth / 2.5) * randomNumber, 2);

        randomNumber = Methods.randomNumber(minValue, maxValue);
        randomNumber = randomNumber / 100;
        this.rightLeg = Math.Round((baseHealth / 2.5) * randomNumber, 2);
        //
        this.bodyFull = this.body;
        this.neckFull = this.neck;
        this.headFull = this.head;
        this.leftArmFull = this.leftArm;
        this.rightArmFull = this.rightArm;
        this.leftLegFull = this.leftLeg;
        this.rightLegFull = this.rightLeg;
    }
}
