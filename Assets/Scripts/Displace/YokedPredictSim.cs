using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YokedPredictSim : YokedSim
{
    protected override void Start()
    {
        //SpherePredictionSelector.Prediction prediction = currentRun.YokedPrediction;
    }

    public override void initialize()
    {
        base.initialize();
        showSizeToggles(false);
        showMaterialToggles(false);
        showPredictButtons(true);
        SpherePredictionSelector.Prediction yokedPrediction = SpherePredictionSelector.Prediction.UNSURE;
        string toggleName = getToggleNameForPrediction(yokedPrediction);
        YokedSim.YokedButtonTarget buttonTarget = getButtonTargetForButtonName(toggleName);
        setToggleAsTarget(buttonTarget);
        buttonTargets.Add(buttonTarget);
    }

    string getToggleNameForPrediction(SpherePredictionSelector.Prediction prediction)
    {
        string toggleName = "";
        switch(prediction)
        {
            case SpherePredictionSelector.Prediction.DEFINITELY_LEFT:
            toggleName = "PredictDefinitelyLeftToggle";
            break; 
            case SpherePredictionSelector.Prediction.LEFT:
            toggleName = "PredictLeftToggle";
            break; 
            case SpherePredictionSelector.Prediction.UNSURE:
            toggleName = "PredictUnsureToggle";
            break; 
            case SpherePredictionSelector.Prediction.RIGHT:
            toggleName = "PredictRightToggle";
            break; 
            case SpherePredictionSelector.Prediction.DEFINITELY_RIGHT:
            toggleName = "PredictDefinitelyRightToggle";
            break; 
        }
        return toggleName;
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
