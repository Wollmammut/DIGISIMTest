using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    void Start()
    {
        positionHandle(54);
    }

    public void positionHandle(int degrees)
    {
        Transform hand = transform.Find("HandPivot");
        hand.localEulerAngles = new Vector3(0, 0, -degrees);
    }
}
