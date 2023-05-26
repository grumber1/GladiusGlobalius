using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System;
using System.Xml.Serialization;
using System.Text;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuGO;
    public GameObject MultiplayerGO;
    public TMP_Text vornamenTMPText;
    public TMP_Text nachnamenTMPText;

    void Start()
    {
        GladiatorNameGenerator.vornamen = vornamenTMPText.text.Split("\n");
        GladiatorNameGenerator.nachnamen = nachnamenTMPText.text.Split("\n");
    }

    public void onClickMultiplayer()
    {
        Methods.switchScreen(MainMenuGO, MultiplayerGO);
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    IEnumerator OneSecondWait()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("1s");
        StartCoroutine("OneSecondWait");
    }
}
