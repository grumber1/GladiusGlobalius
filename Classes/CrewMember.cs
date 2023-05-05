using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Pilot
// {
// public string name;
// public int age;
// public string enrolledWith;

// public int primarySkill;
// public int secondarySkill;


// //Constructor
//     public Pilot(string newName, int newAge, string newEnrolledWith, int newPrimarySkill, int newSecondarySkill)
//     {
//         name = newName;
//         age = newAge;
//         enrolledWith = newEnrolledWith;
//         primarySkill = newPrimarySkill;
//         secondarySkill = newSecondarySkill;
//     }
// }

public class CrewMember
{
public string completeName;
public int age;
public string role;
public string enrolledWith;
public int primarySkill;
public int secondarySkill;
public string status = "OK";
public int health = 100;


//Constructor
    public CrewMember(int newAge, int newPrimarySkill, int newSecondarySkill){
        string[] roles = new string[] { "Pilot", "Navigator", "Bombenschuetze", "Bordschuetze", "Funker", "Mechaniker"};

        completeName = NameGenerator.GenerateCrewMemberName();
        age = newAge;
        role = roles[Methods.randomNumber(0,roles.Length - 1)];
        enrolledWith = " ";
        primarySkill = newPrimarySkill;
        secondarySkill = newSecondarySkill;
    }
}