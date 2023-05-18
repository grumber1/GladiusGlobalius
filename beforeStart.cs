using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beforeStart : MonoBehaviour
{
    public GameObject MainMenuGO;
    public GameObject MultiplayerGO;
    public GameObject LobbyGO;
    public GameObject GameGO;
    public GameObject ServerGO;
    public GameObject SlaveMarketGO;
    public GameObject OwnedGladiatorsGO;

    void Awake()
    {
        MainMenuGO.SetActive(true);
        MultiplayerGO.SetActive(false);
        LobbyGO.SetActive(false);
        GameGO.SetActive(false);
        ServerGO.SetActive(false);
        SlaveMarketGO.SetActive(false);
        OwnedGladiatorsGO.SetActive(false);
    }

    void Start() { }

    void Update() { }
}
