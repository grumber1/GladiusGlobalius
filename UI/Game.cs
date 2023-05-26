using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject SlaveMarketGO;
    public GameObject OwnedGladiatorsGO;
    public GameObject DailyRoutineClientGO;
    public GameObject FadeImageGO;
    public TMP_Text playerDetailsText;
    public TMP_Text dayText;
    public Image fadeOutImage;

    void Start()
    {
        // FadeImageGO.SetActive(true);
        // StartCoroutine(fadeIn(FadeImageGO, fadeOutImage));
    }

    void Update()
    {
        checkForNextDay();
        fillPlayerDetails();
        dayText.text = "Day: " + MultiplayerManagerClient.day;
    }

    public void onClickSlaveMarket()
    {
        SlaveMarketGO.SetActive(true);
        OwnedGladiatorsGO.SetActive(false);
    }

    public void onClickGladiators()
    {
        OwnedGladiatorsGO.SetActive(true);
        SlaveMarketGO.SetActive(false);
    }

    public void onClickNextDay()
    {
        // TODO readyfornextround zurücksetzen
        // StartCoroutine(fadeOut(FadeImageGO, fadeOutImage));
        MultiplayerManagerClient.player.readyForNextRound = true;
        MyTCPClient.sendObjectToServer(
            Messages.Client.MultiplayerManager.ConnectedPlayers.syncPlayer,
            MultiplayerManagerClient.player
        );
    }

    private void checkForNextDay()
    {
        bool nextDay = true;
        MultiplayerManagerClient.connectedPlayers.ForEach(
            (connectedPlayer) =>
            {
                if (connectedPlayer.readyForNextRound == false)
                {
                    nextDay = false;
                }
            }
        );
        if (nextDay == true)
        {
            MultiplayerManagerClient.player.readyForNextRound = false;
            nextDay = false;
            DailyRoutineClientGO.SetActive(false);
            DailyRoutineClientGO.SetActive(true);
        }
    }

    private void fillPlayerDetails()
    {
        string playerText = "Name: " + MultiplayerManagerClient.player.name + "\n";
        string goldText = "Gold: " + MultiplayerManagerClient.player.gold + "\n";

        playerDetailsText.text = playerText + goldText;
    }

    IEnumerator fadeOut(GameObject FadeImageGO, Image image)
    {
        FadeImageGO.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime * (float)0.5)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    IEnumerator fadeIn(GameObject FadeImageGO, Image image)
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime * (float)0.5)
        {
            // set color with i as alpha
            image.color = new Color(0, 0, 0, i);
            yield return null;
        }
        FadeImageGO.SetActive(false);
    }
}
