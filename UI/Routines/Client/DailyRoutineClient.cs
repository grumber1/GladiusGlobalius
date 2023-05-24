using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyRoutineClient : MonoBehaviour
{
    public GameObject FadeImageGO;
    public Image fadeOutImage;

    void Start()
    {
        // StartCoroutine(fadeIn(FadeImageGO, fadeOutImage));
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

    IEnumerator OneSecondWait()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("1s");
    }
}
