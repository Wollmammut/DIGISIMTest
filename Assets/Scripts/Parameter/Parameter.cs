using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Parameter<T> 
{
    public string name;
    public T value;

    public Parameter(string name, T value)
    {
        this.name = name;
        this.value = value;
    }

    public T getValue()
    {
        return value;
    }
}
