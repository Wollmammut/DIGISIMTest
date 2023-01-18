using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpactOnSoilSelector : MonoBehaviour
{
    static Dictionary<string, string> activeToggleByToggleGroup = new Dictionary<string, string>(); 
    EnumImpactOnSoil impactOnSoil;
    GameObject associatedToggle;

    [Serializable]
    public enum EnumImpactOnSoil
    {
        NO_IMPACT,
        UNSURE,
        IMPACT
    }

    void Start()
    {
        string activeToggle;
        activeToggleByToggleGroup.TryGetValue(gameObject.name, out activeToggle);
        if (activeToggle != null)
        {
            Transform t = transform.Find(activeToggle);
            if (t != null)
            {
                Toggle toggle = t.gameObject.GetComponent<Toggle>();
                toggle.isOn = true;
            }
        }
    }

    public void setImpactOnSoil(ImpactOnSoilParameter parameter)
    {
        this.impactOnSoil = parameter.impactOnSoil;
        associatedToggle = parameter.gameObject;
        activeToggleByToggleGroup[gameObject.name] = associatedToggle.name;
    }
}
