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
    }
}
