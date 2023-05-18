using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    void Start() { }

    void transorm()
    {
        Vector3 y = this.transform.position;
        y.x = 5;
        this.transform.position = y;
        Debug.Log(this.transform.position.x);
    }
}
