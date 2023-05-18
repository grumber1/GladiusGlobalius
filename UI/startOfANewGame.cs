using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class startOfANewGame : MonoBehaviour
{
    public TMP_Text vornamenTMPText;
    public TMP_Text nachnamenTMPText;

    // Start is called before the first frame update
    void Start()
    {
        GladiatorNameGenerator.vornamen = vornamenTMPText.text.Split("\n");
        GladiatorNameGenerator.nachnamen = nachnamenTMPText.text.Split("\n");
    }

    // Update is called once per frame
    void Update() { }
}
