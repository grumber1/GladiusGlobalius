using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class GladiatorBody
{
    public double body;
    public double leftArm;
    public double rightArm;
    public double leftLeg;
    public double rightLeg;
    public double neck;
    public double head;

    public GladiatorBody() { }

    //Constructor
    public GladiatorBody(
        double newBody = 75,
        double newLeftArm = 15,
        double newRightArm = 15,
        double newLeftLeg = 25,
        double newRightLeg = 25,
        double newNeck = 5,
        double newHead = 12
    )
    {
        this.body = newBody;
        this.leftArm = newLeftArm;
        this.rightArm = newRightArm;
        this.leftLeg = newLeftLeg;
        this.rightLeg = newRightLeg;
        this.neck = newNeck;
        this.head = newHead;
    }
}
