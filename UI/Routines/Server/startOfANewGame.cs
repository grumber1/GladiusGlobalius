using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class startOfANewGame : MonoBehaviour
{
    public TMP_Text vornamenTMPText;
    public TMP_Text nachnamenTMPText;

    void Start()
    {
        Debug.Log("StartOfANewGame Active");
        GladiatorNameGenerator.vornamen = vornamenTMPText.text.Split("\n");
        GladiatorNameGenerator.nachnamen = nachnamenTMPText.text.Split("\n");
    }
}
