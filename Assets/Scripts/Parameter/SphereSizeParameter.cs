using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSizeParameter : MonoBehaviour, ParameterSetter<SphereSize.EnumSphereSize>
{
    public void setValue(SphereSize.EnumSphereSize value)
    {   
        SphereSize sphereSize = SphereSize.getSphereSizeFromEnum(value);
        setValue(sphereSize);
    }

    public void setValue(SphereSize value)
    {
        int size = value.getValue();
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
}
