using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class YokedConstructSim : YokedSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeToggles(true);
        showMaterialToggles(true);
        showPredictButtons(false);
        SphereSize targetSize = currentRun.yokedConstructSize;
        SphereMaterial material = currentRun.yokedConstructMaterial;
        setSizeToggleAsPointerTarget(targetSize);
        setMaterialToggleAsPointerTarget(material);
        setToggleAsTarget(buttonTargets[0]);

        setSphereMaterialParameter(leftSphere, SphereMaterial.NONE);
        showLeftSphereAndPlunger(false);

        if (SimulationStateManager.getCurrentRunNumber() != 0)
        {
            GameObject instructionPanel = GameObject.Find("First Instruction");
            if (instructionPanel != null)
            {
                TextMeshProUGUI text = instructionPanel.GetComponent<TextMeshProUGUI>();
                text.text = "Als Übung kannst du das Kind dabei beobachten, wie es eine Kugel erstellt.";
            }
        }
    }

    void setMaterialToggleAsPointerTarget(SphereMaterial material)
    {
        YokedSim.YokedButtonTarget buttonTarget;

        if (material == SphereMaterial.WOOD)
        {
            buttonTarget = getButtonTargetForButtonName("ToggleWood");
        }
        else if (material == SphereMaterial.STYROFOAM)
        {
            buttonTarget = getButtonTargetForButtonName("ToggleStyrofoam");
        }
        else
        {
            buttonTarget = getButtonTargetForButtonName("ToggleLead");
        }
        buttonTargets.Add(buttonTarget);
    }

    void setSizeToggleAsPointerTarget(SphereSize size)
    {
        YokedSim.YokedButtonTarget buttonTarget;

        if (size == SphereSize.SMALL)
        {
            buttonTarget = getButtonTargetForButtonName("ToogleSmallSize");
        }
        else if (size == SphereSize.MEDIUM)
        {
            buttonTarget = getButtonTargetForButtonName("ToogleMediumSize");
        }
        else
        {
            buttonTarget = getButtonTargetForButtonName("ToogleLargeSize");
        }
        buttonTargets.Add(buttonTarget);
    }

    public override void onStepAdvancement(SimulationStep step, int currentStepIndex)
    {
        if (currentStepIndex > 0)
        {
            showSizeToggles(false);
            showMaterialToggles(false);
            showPredictButtons(true);
            resetButtonTargets();
            SpherePredictionSelector.Prediction yokedPrediction = currentRun.yokedPrediction;
            string toggleName = getToggleNameForPrediction(yokedPrediction);
            YokedSim.YokedButtonTarget buttonTarget = getButtonTargetForButtonName(toggleName);
            setToggleAsTarget(buttonTarget);
        }
    }

    protected override void setupSimulationWithValuesFromCurrentRun()
    {
        // SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        // spl.setValue(currentRun.sizeLeft);
        // SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        // spr.setValue(currentRun.sizeRight);

        // SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        // materialParameterLeft.setValue(currentRun.materialLeft);
        // SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();
        // materialParameterRight.setValue(currentRun.materialRight);
        
        // ConstructionTask constructTask = gameObject.GetComponent<ConstructionTask>();//TODO nulll check
        // int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
        // currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        // constructTask.setTaskSymbol(currentRun.constructTask);

        // SphereSize targetSize = currentRun.yokedConstructSize;
        // SphereMaterial material = currentRun.yokedConstructMaterial;
        // setSizeToggleAsPointerTarget(targetSize);
        // setMaterialToggleAsPointerTarget(material);
        
        // replaceKidsWordsPlaceholderWhith("$size", targetSize.getAdjective());
        // replaceKidsWordsPlaceholderWhith("$material", material.name);
    }
}
