using UnityEngine;

public class beforeStart : MonoBehaviour
{
    public GameObject MainMenuGO;
    public GameObject MultiplayerGO;
    public GameObject LobbyGO;
    public GameObject GameGO;
    public GameObject ServerGO;
    public GameObject OwnedBuildingsGO;
    public GameObject OwnedGladiatorsGO;
    public GameObject SlaveMarketGO;
    public GameObject DailyRoutineClientGO;
    public GameObject ServerGameHandlingGO;

    void Awake()
    {
        MainMenuGO.SetActive(true);
        MultiplayerGO.SetActive(false);
        LobbyGO.SetActive(false);
        GameGO.SetActive(false);
        ServerGO.SetActive(false);
        OwnedBuildingsGO.SetActive(false);
        OwnedGladiatorsGO.SetActive(false);
        SlaveMarketGO.SetActive(false);
        DailyRoutineClientGO.SetActive(false);
        ServerGameHandlingGO.SetActive(false);
    }
}
