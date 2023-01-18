using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimulationStep
{
    public GameObject correspondingTextObject;
    public SimulationStateManager.SimulationStates currentState;

    public void setCorrespondingTextObjectActive(bool active)
    {
        if (correspondingTextObject != null)
        {
            correspondingTextObject.SetActive(active);
        }
    }
}
