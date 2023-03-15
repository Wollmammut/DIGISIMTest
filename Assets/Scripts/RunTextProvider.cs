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
        SimulationSelector.SimulationType currentSimulationType = SimulationSelector.currentSimulationType;
        List<Run> runs;

        Dictionary<SimulationSelector.SimulationType, List<Run>> runsToGetFrom = DisplacementSim.runsBySimulationType;
        if (runsToGetFrom.TryGetValue(currentSimulationType, out runs))
        {
            newText = newText.Replace("$maxRun", runs.Count.ToString());
        }
        text.text = newText;
    }
}
