using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject SlaveMarketGO;
    public GameObject OwnedGladiatorsGO;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

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
}
