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
    private static SimulationStates currentSimulationState = SimulationStates.PREVIEW;
    [SerializeField]
    private List<SimulationStep> steps = new List<SimulationStep>();
    [SerializeField]
    private List<SimulationStep> trainingSteps = new List<SimulationStep>();
    private List<SimulationStep> activeSteps = new List<SimulationStep>();
    protected static int currentStepIndex = 0;
    protected SimulationStep currentStep;
    protected static bool isTrainingRun = false;

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
        activeSteps = isTrainingRun ? trainingSteps: steps;
        foreach (SimulationStep step in trainingSteps)
        {
            GameObject g = step.correspondingTextObject;
            g.SetActive(false);
        }
        foreach (SimulationStep step in steps)
        {
            GameObject g = step.correspondingTextObject;
            g.SetActive(false);
        }
        if (activeSteps.Count > 0)
        {
            currentStep = activeSteps[currentStepIndex];
            currentStep.setProceedButtonText();
            currentStep.setCorrespondingTextObjectActive(true);
            currentSimulationState = currentStep.currentState;
        }
        initializeSimulation();
    }

    public bool hasNextStep()
    {
        return activeSteps.Count - 1 > currentStepIndex;
    }

    void deactivateAllInstructions()
    {
        foreach (SimulationStep step in activeSteps)
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
        currentStep = activeSteps[++currentStepIndex];
        currentStep.correspondingTextObject.SetActive(true);
        currentStep.setProceedButtonText();
        SimulationStates oldState = currentSimulationState;
        currentSimulationState = currentStep.currentState;
        Simulation simulation = getSimulationComponent();
        simulation.onSimulationStateChanged(oldState, currentSimulationState);
        simulation.onStepAdvancement(currentStep, currentStepIndex);
    }

    public void toggleSimulationActive()
    {
        if(!getSimulationComponent().canProceed())
        {
            return;
        }
        if (!getSimulationComponent().shouldForceAdvanceRun() && hasNextStep())
        {
            activateNextStep();
        }
        else
        {
            advanceToNextRun();
        }
    }

    public void advanceToNextRun()
    {
        currentStepIndex = 0;
        Simulation simulation = getSimulationComponent();
        simulation.saveData();
        ClickLogger.clear();
        if (simulation.getNumberOfRuns() - 1 > runNumber)
        {
            currentStepIndex = 0;
            ++runNumber;
            if (isTrainingRun)
            {
                SimulationSelector.continueToSimulation(true);
            }
            else
            {
                SceneManager.LoadScene("RunScene");// TODO errof if scenetowtichto == null
            }
        }
        else
        {
            runNumber = 0;
            if (isTrainingRun)
            {
                isTrainingRun = false;
                SceneManager.LoadScene("IntermissionScene");// TODO errof if scenetowtichto == null
            }
            else
            {
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

    public int getMaxNumberOfRuns()
    {
        Simulation simulation = getSimulationComponent();
        return simulation.getNumberOfRuns();
    }

    public static void setMaxNumberOfRuns(int maxNumber)
    {
        maxNumberOfRuns = maxNumber;
    }

    public static void setTrainingMode(bool training)
    {
        isTrainingRun = training;
    }

    public bool isTrainingMode()
    {
        return isTrainingRun;
    }

    public int getNumberOfSteps()
    {
        return activeSteps.Count;
    }
}
