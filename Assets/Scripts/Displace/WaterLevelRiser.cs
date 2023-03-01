using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelRiser : MonoBehaviour
{
    float initialWaterHeight;
    Vector3 initialScale;
    Vector3 initialPosition;

    private void OnTriggerEnter(Collider other)
    {
        initializePositionAndScale();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Simulation.ShowAnimations())
        {
            Vector3 otherLocalScale = other.transform.localScale;
            Vector3 otherPosition = other.transform.position;
            float depthBelowInitialWaterHeight = initialWaterHeight - (otherPosition.y - otherLocalScale.y / 2);

            Vector3 dir = new Vector3(0, 1, 0);
            Vector3 waterPosition = initialPosition + dir * depthBelowInitialWaterHeight / 2;
            Vector3 waterLocalScale = initialScale + dir * depthBelowInitialWaterHeight;
            float currentWaterHeight = waterPosition.y + waterLocalScale.y / 2;
            float depthBelowCurrentWaterHeight = currentWaterHeight - (otherPosition.y - otherLocalScale.y / 2);

            if (depthBelowCurrentWaterHeight < otherLocalScale.y)
            {
                transform.position = waterPosition;
                transform.localScale = waterLocalScale;
            }
        }
        else
        {
            Vector3 otherLocalScale = other.transform.localScale;
            Vector3 otherPosition = other.transform.position;
            Vector3 dir = new Vector3(0, 1, 0);
            Vector3 waterPosition = initialPosition + dir * otherLocalScale.y / 4;
            Vector3 waterLocalScale = initialScale + dir * otherLocalScale.y / 2;
            transform.position = waterPosition;
            transform.localScale = waterLocalScale;
        }

    }

    private void initializePositionAndScale()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
        initialWaterHeight = initialPosition.y + initialScale.y / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        initializePositionAndScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
