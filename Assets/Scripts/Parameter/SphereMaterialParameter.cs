using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterialParameter : MonoBehaviour, ParameterSetter<SphereMaterial.EnumSphereMaterial>
{
    public void setValue(SphereMaterial.EnumSphereMaterial value)
    {   
        SphereMaterial material = SphereMaterial.getSphereMaterialFromEnum(value);
        setValue(material);

    }
    public void setValue(SphereMaterial value)
    {      
        string matName = value.getValue();
        if (Simulation.useFancyGraphics())
        {
            matName = "Materials/Fancy/" + matName;
        }
        else
        {
            matName = "Materials/" + matName;
        }
        UnityEngine.Material newMaterial = Resources.Load<UnityEngine.Material>(matName);
        if (newMaterial == null)
        {
            //TODO some kind of error
        }
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = newMaterial;

    }
}
