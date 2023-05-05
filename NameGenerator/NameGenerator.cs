using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class NameGenerator
{
    public static string GenerateCrewMemberName(){
        string[] crewMemberNames = NamesList.crewMemberNames;
        string[] crewMemberPreNames = NamesList.crewMemberPreNames;

        int randomNumber = Methods.randomNumber(0,crewMemberNames.Length - 1);
        string crewMemberName = crewMemberNames[randomNumber];

        randomNumber = Methods.randomNumber(0,crewMemberNames.Length - 1);
        string crewMemberPreName = crewMemberNames[randomNumber];

        return crewMemberPreName + " " + crewMemberName;
    }   
}
