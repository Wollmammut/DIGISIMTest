using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunTextProvider : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>(); // TODO null check
        string newText = text.text.Replace("$run", (SimulationStateManager.runNumber + 1).ToString());
        newText = newText.Replace("$maxRun", SimulationStateManager.maxNumberOfRuns.ToString());
        text.text = newText;
    }
}
