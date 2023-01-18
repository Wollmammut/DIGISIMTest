using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ParameterSetter<T>
{
    public void setValue(T value);
}