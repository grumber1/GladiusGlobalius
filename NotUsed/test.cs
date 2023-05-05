using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void transorm()
    {

        Vector3 y = this.transform.position;
        y.x = 5;
        this.transform.position = y;
        Debug.Log(this.transform.position.x);
    }
}
