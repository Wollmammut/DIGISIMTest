using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Obsolete("Use ButtonConnectedGameObject instead?", true)]
public class SimulationParameterChanger<T> : MonoBehaviour
{
    public T parameterValueToSetTo;

    void Start()
    {
        
    }

    public void onClick()
    {
        GameObject simulation = GameObject.Find("Simulation");
        if (simulation == null)
        {
            // TODO error
        }
        ParameterSetter<T> parameter = simulation.GetComponent<ParameterSetter<T>>();
        // TODO error when connectedGameObject is null/not set in unity inspector
        if (parameter != null)
        {
            parameter.setValue(parameterValueToSetTo);
        }
        else
        { 
            // TODO error when parameter is null
            Debug.Log("error, could not find parameter. Has target object parameter script added?" + this);
        }
    }
}
