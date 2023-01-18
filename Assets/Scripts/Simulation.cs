using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class Simulation : MonoBehaviour
{
    protected bool isActive = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setActive()
    {
        isActive = true;
        onActivation();
    }

    protected abstract void onActivation();

    public abstract void initialize();

    public virtual void onSimulationStateChanged(SimulationStateManager.SimulationStates oldState, SimulationStateManager.SimulationStates newState)
    {

    }

    public virtual bool canProceed()
    {
        return true; // TODO make abstract and add code for displace sim (biolog overrides this method already)
    }
}
