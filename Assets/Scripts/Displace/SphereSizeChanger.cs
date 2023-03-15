using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSizeChanger : ButtonConnectedGameObject<SphereSize.EnumSphereSize>
{
    public override void onClick()
    {
        base.onClick();
        GameObject simO = GameObject.Find("Simulation");
        if (simO != null)
        {
            DisplacementSim sim = simO.GetComponent<DisplacementSim>();
            if (sim != null)
            {
                sim.showMaterialToggles(true);
            }
        }
    }
    public void showMaterialButtons()
    {
        
    }
}
