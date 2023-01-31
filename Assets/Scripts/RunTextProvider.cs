using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunTextProvider : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>(); // TODO null check
        string newText = text.text.Replace("$run", (SimulationStateManager.getCurrentRunNumber()).ToString());
        newText = newText.Replace("$maxRun", SimulationStateManager.getMaxNumberOfRuns().ToString());
        text.text = newText;
    }
}
