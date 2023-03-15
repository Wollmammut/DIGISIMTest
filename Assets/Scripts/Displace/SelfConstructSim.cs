using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfConstructSim : DisplacementSim
{
    public override void initialize()
    {
        base.initialize();

        //setSphereMaterialParameter(leftSphere, SphereMaterial.NONE);
        //setSphereMaterialParameter(rightSphere, SphereMaterial.WOOD);

        //ConstructionTask constructTask = gameObject.GetComponent<ConstructionTask>();//TODO nulll check
        int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
        currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        //constructTask.setTaskSymbol(currentRun.constructTask);

        int maxNumOfRuns = this.getNumberOfRuns();
        if (isTrainingRun)
        {
            showSizeToggles(false);
            showMaterialToggles(false);
            showPredictButtons(true);
            if (currentRunNumber >= maxNumOfRuns / 2)
            {
                setSphereMaterialParameter(leftSphere, SphereMaterial.NONE);
                showLeftSphereAndPlunger(false);
                showSizeToggles(true);
                showPredictButtons(false);
                GameObject instructionPanel = GameObject.Find("First Instruction");
                if (instructionPanel != null)
                {
                    TextMeshProUGUI text = instructionPanel.GetComponent<TextMeshProUGUI>();
                    if (currentRunNumber == maxNumOfRuns / 2)
                    {
                        text.text = "Nun übst du wie man eine Kugel erstellt. Wenn du eine Größe auswählst erscheint die Kugel. Du kannst die Größe dann immernoch ändern. Danach kannst du das Material auswählen.Du sollst die Kugel so erstellen, dass du deine Annahmen über Wasserverdrängung prüfen kannst.";
                    }
                    else
                    {
                        text.text = "Du kannst nun üben, wie man eine Kugel erstellt.";
                    }
                    
                }
            }
            else if (currentRunNumber != 0)
            {
                GameObject instructionPanel = GameObject.Find("First Instruction");
                if (instructionPanel != null)
                {
                    TextMeshProUGUI text = instructionPanel.GetComponent<TextMeshProUGUI>();
                    text.text = "Du kannst nun üben, wie man eine Vorhersage trifft.";
                }
            }
        }
        else
        {
            setSphereMaterialParameter(leftSphere, SphereMaterial.NONE);
            showLeftSphereAndPlunger(false);
            showSizeToggles(true);
            showMaterialToggles(false);
            showPredictButtons(false);
        }
    }

    public override bool shouldForceAdvanceRun()
    {
        int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
        currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        int maxNumOfRuns = this.getNumberOfRuns();
        return isTrainingRun && currentRunNumber >= maxNumOfRuns / 2;
    }

    public override void onStepAdvancement(SimulationStep step, int currentStepIndex)
    {
        // if (isTrainingRun && currentStepIndex >= getStateManager().getNumberOfSteps() / 2)
        // {
        //     showSizeAndMaterialToggles(true);
        //     showPredictButtons(false);
        // }
        if (!isTrainingRun && currentStepIndex > 0)
        {
            showSizeToggles(false);
            showMaterialToggles(false);
            showPredictButtons(true);
        }
    }
}
