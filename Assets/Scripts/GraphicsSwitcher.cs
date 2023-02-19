using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSwitcher : MonoBehaviour
{
    void Start()
    {
        Toggle t = GetComponent<Toggle>();
        if (t.isOn != Simulation.useFancyGraphics())
        {
            switchGraphicsStyle();
        }
        t.isOn = Simulation.useFancyGraphics();
    }

    public void switchGraphicsStyle()
    {
        Simulation.switchUseFancyGraphics();
    }
}
