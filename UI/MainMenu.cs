using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.IO;
using System;
using System.Xml.Serialization;
using System.Text;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuGO;
    public GameObject MultiplayerGO;

    void Start()
    {
        // StartCoroutine("OneSecondWait");

        Gladiator gladiator = Methods.createNewGladiator(
            "marcel",
            75,
            10,
            Weapons.fist,
            Weapons.knife
        );

        double number = Methods.randomNumber(90, 110);
        double result = number / 100;

        Debug.Log(gladiator.body.body);
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
