using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfConstructSim : DisplacementSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeAndMaterialToggles(true);
        showPredictButtons(false);

        setSphereMaterialParameter(leftSphere, SphereMaterial.NONE);
        setSphereMaterialParameter(rightSphere, SphereMaterial.WOOD);

        ConstructionTask constructTask = gameObject.GetComponent<ConstructionTask>();//TODO nulll check
        int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
        currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        constructTask.setTaskSymbol(currentRun.constructTask);
    }
}
