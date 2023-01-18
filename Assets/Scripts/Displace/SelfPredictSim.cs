using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPredictSim : DisplacementSim
{
    public override void initialize()
    {
        base.initialize();
        showSizeAndMaterialToggles(false);
        showPredictButtons(true);

        SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        spl.setValue(currentRun.sizeLeft);
        SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        spr.setValue(currentRun.sizeRight);

        SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        materialParameterLeft.setValue(currentRun.materialLeft);
        SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();
        materialParameterRight.setValue(currentRun.materialRight);
    }
}
