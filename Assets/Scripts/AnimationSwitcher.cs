using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSwitcher : MonoBehaviour
{
    void Start()
    {
        Toggle t = GetComponent<Toggle>();
        if (t.isOn != Simulation.ShowAnimations())
        {
            switchAnimationStyle();
        }
        t.isOn = Simulation.ShowAnimations();
    }

    public void switchAnimationStyle()
    {
        Simulation.SwitchShowAnimations();
    }
}
