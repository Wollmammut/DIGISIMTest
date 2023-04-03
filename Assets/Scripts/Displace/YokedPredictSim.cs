using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class YokedPredictSim : YokedSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeToggles(false);
        showMaterialToggles(false);
        showPredictButtons(true);
        // SpherePredictionSelector.Prediction yokedPrediction = SpherePredictionSelector.Prediction.UNSURE;
        // string toggleName = getToggleNameForPrediction(yokedPrediction);
        // YokedSim.YokedButtonTarget buttonTarget = getButtonTargetForButtonName(toggleName);
        // setToggleAsTarget(buttonTarget);
        // buttonTargets.Add(buttonTarget);

        SpherePredictionSelector.Prediction yokedPrediction = currentRun.yokedPrediction;
        string toggleName = getToggleNameForPrediction(yokedPrediction);
        YokedSim.YokedButtonTarget buttonTarget = getButtonTargetForButtonName(toggleName);
        setToggleAsTarget(buttonTarget);
        buttonTargets.Add(buttonTarget);
        //setTextForAIKidsWords(getTextForPrediction(yokedPrediction));

        if (SimulationStateManager.getCurrentRunNumber() != 0)
        {
            GameObject instructionPanel = GameObject.Find("First Instruction");
            if (instructionPanel != null)
            {
                TextMeshProUGUI text = instructionPanel.GetComponent<TextMeshProUGUI>();
                text.text = "Als Übung kannst du das Kind dabei beobachten wie es eine Vorhersage trifft.";
            }
        }
    }

    string getTextForPrediction(SpherePredictionSelector.Prediction prediction)
    {
        string text = "";
        switch(prediction)
        {
            case SpherePredictionSelector.Prediction.DEFINITELY_LEFT:
            text = "\"Ich bin mir sehr sicher, dass die linke Kugel mehr Wasser verdrängt\"";
            break; 
            case SpherePredictionSelector.Prediction.LEFT:
            text = "\"Ich bin mir sicher, dass die linke Kugel mehr Wasser verdrängt\"";
            break; 
            case SpherePredictionSelector.Prediction.UNSURE:
            text = "\"Ich bin mir unsicher, welche Kugel mehr Wasser verdrängt\"";
            break; 
            case SpherePredictionSelector.Prediction.RIGHT:
            text = "\"Ich bin mir sicher, dass die rechte Kugel mehr Wasser verdrängt\"";
            break; 
            case SpherePredictionSelector.Prediction.DEFINITELY_RIGHT:
            text = "\"Ich bin mir sehr sicher, dass die rechte Kugel mehr Wasser verdrängt\"";
            break; 
        }
        return text;
    }
    protected override void setupSimulationWithValuesFromCurrentRun()
    {
        SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        spl.setValue(currentRun.sizeLeft);
        SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        spr.setValue(currentRun.sizeRight);

        SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        materialParameterLeft.setValue(currentRun.materialLeft);
        SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();
        materialParameterRight.setValue(currentRun.materialRight);
        
        SpherePredictionSelector.Prediction yokedPrediction = currentRun.yokedPrediction;
        string toggleName = getToggleNameForPrediction(yokedPrediction);
        YokedSim.YokedButtonTarget buttonTarget = getButtonTargetForButtonName(toggleName);
        setToggleAsTarget(buttonTarget);
        buttonTargets.Add(buttonTarget);
        setTextForAIKidsWords(getTextForPrediction(yokedPrediction));
    }
}
