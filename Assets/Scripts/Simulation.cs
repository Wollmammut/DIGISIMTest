using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class Simulation : MonoBehaviour
{
    protected bool isActive = false;
    protected static bool fancyGraphics = true;
    protected static bool showAnimations = false;
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

    public virtual void reset()
    {

    }

    protected abstract void onActivation();

    public abstract void initialize();

    public virtual void onSimulationStateChanged(SimulationStateManager.SimulationStates oldState, SimulationStateManager.SimulationStates newState)
    {

    }
    
    public virtual void onStepAdvancement(SimulationStep step, int currentStepIndex)
    {

    }

    public abstract void saveData();
    
    public abstract bool canProceed();

    public virtual bool shouldForceAdvanceRun()
    {
        return false;
    }

    public static bool useFancyGraphics()
    {
        return fancyGraphics;
    }

    public static void switchUseFancyGraphics()
    {
        fancyGraphics = !fancyGraphics;
    }

    public static bool ShowAnimations()
    {
        return showAnimations;
    }

    public static void SwitchShowAnimations()
    {
        showAnimations = !showAnimations;
    }

    public virtual int getNumberOfRuns()
    {
        return 0;
    }
}
