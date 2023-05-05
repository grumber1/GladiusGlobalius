using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreNames : MonoBehaviour
{
    public TMP_Text Names;
    public TMP_Text PreNames;

    void Start(){
        string[] crewMemberNames = Names.text.Split('\n');
        string[] crewMemberPreNames = PreNames.text.Split('\n');

        NamesList.crewMemberNames = crewMemberNames;
        NamesList.crewMemberPreNames = crewMemberPreNames;
    }   
}
