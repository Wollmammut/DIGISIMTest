using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class SimulationStep
{
    public GameObject correspondingTextObject;
    public SimulationStateManager.SimulationStates currentState;
    public string proceedButtonText;

    public void setCorrespondingTextObjectActive(bool active)
    {
        if (correspondingTextObject != null)
        {
            correspondingTextObject.SetActive(active);
        }
    }

    public void setProceedButtonText()
    {
        GameObject button = GameObject.Find("SimulationButton");
        if (button != null)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = proceedButtonText;
        }
    }
}
