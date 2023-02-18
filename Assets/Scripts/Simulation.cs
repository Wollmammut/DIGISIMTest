using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class Simulation : MonoBehaviour
{
    protected bool isActive = false;
    protected static bool fancyGraphics = true;

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

    public abstract void saveData();
    
    public abstract bool canProceed();

    public static bool useFancyGraphics()
    {
        return fancyGraphics;
    }

    public static void switchUseFancyGraphics()
    {
        fancyGraphics = !fancyGraphics;
    }
}
