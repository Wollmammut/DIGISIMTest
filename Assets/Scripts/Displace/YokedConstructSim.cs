using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokedConstructSim : YokedSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeAndMaterialToggles(true);
        showPredictButtons(false);
        setSizeToggleAsPointerTarget(SphereSize.MEDIUM);
        setMaterialToggleAsPointerTarget(SphereMaterial.STYROFOAM);
        setToggleAsTarget(buttonTargets[0]);
    }

    void setMaterialToggleAsPointerTarget(SphereMaterial material)
    {
        YokedSim.YokedButtonTarget buttonTarget;

        if (material.name.Equals("wood"))
        {
            buttonTarget = getButtonTargetForButtonName("ToggleWood");
        }
        else if (material.name == "styrofoam")
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

    protected override void setupSimulationWithValuesFromCurrentRun()
    {
        ConstructionTask constructTask = gameObject.GetComponent<ConstructionTask>();//TODO nulll check
        int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
        currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        constructTask.setTaskSymbol(currentRun.constructTask);

        SphereSize targetSize = currentRun.yokedConstructSize;
        SphereMaterial material = currentRun.yokedConstructMaterial;
        setSizeToggleAsPointerTarget(targetSize);
        setMaterialToggleAsPointerTarget(material);
        
        replaceKidsWordsPlaceholderWhith("$size", targetSize.name);
        replaceKidsWordsPlaceholderWhith("$material", material.localizedDisplayName);
    }
}
