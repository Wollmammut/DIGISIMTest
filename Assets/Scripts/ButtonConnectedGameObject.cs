using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonConnectedGameObject<T> : MonoBehaviour
{
    public GameObject connectedGameObject;
    public T parameterValueToSetTo;

    void Start()
    {
        
    }

    public void onClick()
    {
        ParameterSetter<T> parameter = connectedGameObject.GetComponent<ParameterSetter<T>>();
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
