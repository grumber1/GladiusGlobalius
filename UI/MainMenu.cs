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

    void Start() { }

    public void onClickMultiplayer()
    {
        Methods.switchScreen(MainMenuGO, MultiplayerGO);
    }

    public void onClickExit()
    {
        Application.Quit();
    }
}
