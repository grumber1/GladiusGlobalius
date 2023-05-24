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
    public GameObject DailyRoutineClientGO;
    public GameObject DailyRoutineServerGO;
    public GameObject ServerGameHandlingGO;
    public GameObject StartOfANewGameGO;

    void Awake()
    {
        MainMenuGO.SetActive(true);
        MultiplayerGO.SetActive(false);
        LobbyGO.SetActive(false);
        GameGO.SetActive(false);
        ServerGO.SetActive(false);
        SlaveMarketGO.SetActive(false);
        OwnedGladiatorsGO.SetActive(false);
        DailyRoutineClientGO.SetActive(false);
        DailyRoutineServerGO.SetActive(false);
        ServerGameHandlingGO.SetActive(false);
        StartOfANewGameGO.SetActive(false);
    }

    void Start() { }

    void Update() { }
}
