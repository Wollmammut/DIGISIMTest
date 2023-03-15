using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfPredictSim : DisplacementSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeToggles(false);
        showMaterialToggles(false);
        showPredictButtons(true);

        if (SimulationStateManager.getCurrentRunNumber() != 0)
        {
            GameObject instructionPanel = GameObject.Find("First Instruction");
            if (instructionPanel != null)
            {
                TextMeshProUGUI text = instructionPanel.GetComponent<TextMeshProUGUI>();
                text.text = "Du kannst nun üben, wie man eine Vorhersage trifft.";
            }
        }
    }
}
