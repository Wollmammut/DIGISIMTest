using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SimulationStateManager : MonoBehaviour
{
    private static int runNumber = 0;
    private static int maxNumberOfRuns = 1;
    public static SimulationStates currentSimulationState = SimulationStates.PREVIEW;
    public List<SimulationStep> steps = new List<SimulationStep>();
    protected static int currentStepIndex = 0;
    protected SimulationStep currentStep;
    public int stepIndexToSwitchToAfterInstructions;

    [Serializable]
    public enum SimulationStates
    {
        ACTIVE,
        PAUSED,
        PREVIEW,
        DEMO,
        INTERMISSION
    }

    protected void Start()
    {
        foreach (SimulationStep step in steps)
        {
            GameObject g = step.correspondingTextObject;
            g.SetActive(false);
        }
        if (steps.Count > 0)
        {
            if (runNumber > 0)
            {
                currentStepIndex = stepIndexToSwitchToAfterInstructions - 1;
            }
            currentStep = steps[currentStepIndex];
            currentStep.setProceedButtonText();
            currentStep.setCorrespondingTextObjectActive(true);
            currentSimulationState = currentStep.currentState;
        }
        initializeSimulation();
        if (currentSimulationState == SimulationStates.INTERMISSION)
        {
            activateNextStep();
        }
    }

    public bool hasNextStep()
    {
        return steps.Count - 1 > currentStepIndex;
    }

    void deactivateAllInstructions()
    {
        foreach (SimulationStep step in steps)
        {
            step.correspondingTextObject.SetActive(false);
        }
    }

    void changeStateTo(SimulationStates state)
    {
        currentSimulationState = state;
    }

    public static int getCurrentRunNumber()
    {
        return runNumber;
    }

    public SimulationStates getCurrentSimulationState()
    {
        return currentSimulationState;
    }

    void activateNextStep()
    {
        SimulationStep oldStep = currentStep;
        oldStep.setCorrespondingTextObjectActive(false);
        if (oldStep.resetSimulationAfterStep)
        {
            getSimulationComponent().reset();
        }
        currentStep = steps[++currentStepIndex];
        currentStep.correspondingTextObject.SetActive(true);
        currentStep.setProceedButtonText();
        SimulationStates oldState = currentSimulationState;
        currentSimulationState = currentStep.currentState;
        Simulation simulation = getSimulationComponent();
        simulation.onSimulationStateChanged(oldState, currentSimulationState);
    }

    public void toggleSimulationActive()
    {
        if(!getSimulationComponent().canProceed())
        {
            return;
        }
        if (hasNextStep())
        {
            activateNextStep();

            if(currentSimulationState == SimulationStates.INTERMISSION)
            {
                //currentSimulationState = SimulationStates.PAUSED;
                SceneManager.LoadScene("IntermissionScene");// TODO errof if scenetowtichto == null
            }
        }
        else
        {
            currentStepIndex = 0;
            Simulation simulation = getSimulationComponent();
            simulation.saveData();
            ClickLogger.clear();
            if (maxNumberOfRuns - 1 > runNumber)
            {
                currentStepIndex = 0;
                ++runNumber;
                SceneManager.LoadScene("RunScene");// TODO errof if scenetowtichto == null
            }
            else
            {
                runNumber = 0;
                SceneManager.LoadScene("EndScene");// TODO errof if scenetowtichto == null
            }
        }
    }

    private Simulation getSimulationComponent()
    {
        return gameObject.GetComponent<Simulation>();
    }

    public void initializeSimulation()
    {  
        getSimulationComponent().initialize();
    }

    public void addSimulationComponent<T>(T simulationComponent) where T : Simulation
    {
        T sc = gameObject.AddComponent(simulationComponent.GetType()) as T;
    }

    public static int getMaxNumberOfRuns()
    {
        return maxNumberOfRuns;
    }

    public static void setMaxNumberOfRuns(int maxNumber)
    {
        maxNumberOfRuns = maxNumber;
    }
}
