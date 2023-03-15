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
        float oldSize = gameObject.transform.localScale.y;
        gameObject.transform.localScale = new Vector3(size, size, size);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 116 - (120 - size) / 2, gameObject.transform.position.z);
    }
}
