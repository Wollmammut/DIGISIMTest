using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public abstract class DisplacementSim : Simulation
{
    public GameObject panelForAIKidsWords;
    public GameObject leftSphere;
    public GameObject rightSphere;
    protected GameObject leftPlunger;
    protected GameObject rightPlunger;
    Vector3 leftSphereStartPosition;
    Vector3 leftSphereTargetPosition;
    Vector3 rightSphereStartPosition;
    Vector3 rightSphereTargetPosition;
    Vector3 leftPlungerStartPosition;
    Vector3 leftPlungerTargetPosition;
    Vector3 rightPlungerStartPosition;
    Vector3 rightPlungerTargetPosition;
    float fractionOfLoweringTimePassed;
    static List<Run> runs = new List<Run>();
    static Dictionary<SimulationSelector.SimulationType, List<Run>> runsBySimulationType = new Dictionary<SimulationSelector.SimulationType, List<Run>>();
    protected Run currentRun;

    protected override void Start()
    {
        base.Start();
    }

    public override void initialize()
    {
        // TODO just for testing
        Run run = new Run();
        runs.Add(run);
        // TODO testing end

        currentRun = getCurrentRun();

        setSphereSizeParameter(leftSphere, SphereSize.MEDIUM);
        setSphereSizeParameter(rightSphere, SphereSize.MEDIUM);
        setSphereMaterialParameter(leftSphere, SphereMaterial.WOOD);
        setSphereMaterialParameter(rightSphere, SphereMaterial.WOOD);

        leftSphereStartPosition = leftSphere.transform.position;
        leftSphereTargetPosition = leftSphereStartPosition - new Vector3(0, 150, 0);
        rightSphereStartPosition = rightSphere.transform.position;
        rightSphereTargetPosition = rightSphereStartPosition - new Vector3(0, 150, 0);

        leftPlunger = GameObject.Find("LeftPlunger");
        rightPlunger = GameObject.Find("RightPlunger");
        leftPlungerStartPosition = leftPlunger.transform.position;
        leftPlungerTargetPosition = leftPlungerStartPosition - new Vector3(0, 150, 0);
        rightPlungerStartPosition = rightPlunger.transform.position;
        rightPlungerTargetPosition = rightPlungerStartPosition - new Vector3(0, 150, 0);
    }

    public Run getCurrentRun()
    {
        if (currentRun == null)
        {
            int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
            currentRun = getRunForRunNumber(currentRunNumber); // TODO null check
        }
        return currentRun;
    }

    protected Run getRunForRunNumber(int runNumber)
    {
        SimulationSelector.SimulationType currentSimulationType = SimulationSelector.currentSimulationType;
        List<Run> runs;
        if (runsBySimulationType.TryGetValue(currentSimulationType, out runs))
        {
            if (runs.Count > runNumber)
            {
                return runs[runNumber];
            }
            else
            {
                int index = runNumber % runs.Count;
                return runs[index];
            }
        }
        return new Run();
    }

    protected virtual void FixedUpdate()
    {
        SimulationStateManager stateManager = GetComponent<SimulationStateManager>(); // TODO null check
        SimulationStateManager.SimulationStates currentState = stateManager.getCurrentSimulationState();
        if (currentState == SimulationStateManager.SimulationStates.ACTIVE)
        {
            lowerSpheres();
        }
    }

    protected void lowerSpheres()
    {
        float secondsToReachTarget = 10; // take 10 s to lower spheres;
        fractionOfLoweringTimePassed += Time.deltaTime/secondsToReachTarget;
        // if (fractionOfLoweringTimePassed > 1) // not a very good solution
        // {
        //     showCursor();
        // }
        // else
        // {
        //     hideCursor();
        // }
        leftSphere.transform.position = Vector3.Lerp(leftSphereStartPosition, leftSphereTargetPosition, fractionOfLoweringTimePassed);
        rightSphere.transform.position = Vector3.Lerp(rightSphereStartPosition, rightSphereTargetPosition, fractionOfLoweringTimePassed);
        leftPlunger.transform.position = Vector3.Lerp(leftPlungerStartPosition, leftPlungerTargetPosition, fractionOfLoweringTimePassed);
        rightPlunger.transform.position = Vector3.Lerp(rightPlungerStartPosition, rightPlungerTargetPosition, fractionOfLoweringTimePassed);
    }

    protected override void onActivation()
    {
    }

    protected void showPredictButtons(bool show)
    {
        GameObject toggles = GameObject.Find("PredictButtons");
        if(toggles == null)
        {
            //TODO error
        }
        else
        {
            toggles.SetActive(show);
            if (show)
            {
                ToggleGroup toggleGroup = toggles.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
                foreach(Transform child in toggles.transform)
                {
                    Toggle t = child.GetComponent<Toggle>();
                    t.SetIsOnWithoutNotify(false);
                }
            }
        }
    }

    protected void showSizeAndMaterialToggles(bool show)
    {
        GameObject toggles = GameObject.Find("SizeAndMaterialToggles");
        if(toggles == null)
        {
            //TODO error
        }
        else
        {
            toggles.SetActive(show);
            if (show)
            {
                GameObject toggleGroupGO = GameObject.Find("SizeToggleGroup"); // TODO null check
                ToggleGroup toggleGroup = toggleGroupGO.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
                toggleGroupGO = GameObject.Find("MaterialToggleGroup"); // TODO null check
                toggleGroup = toggleGroupGO.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
            }
        }
    }

    public void replaceKidsWordsPlaceholderWhith(string placeHolder, string textToDisplay)
    {
        TextMeshProUGUI text = panelForAIKidsWords.GetComponent<TextMeshProUGUI>(); // TODO null check
        string newText = text.text.Replace(placeHolder, textToDisplay);
        text.text = newText;
    }

    public void setTextForAIKidsWords(string textToDisplay)
    {
        TextMeshProUGUI text = panelForAIKidsWords.GetComponent<TextMeshProUGUI>(); // TODO null check
        text.text = textToDisplay;
    }

    public override void onSimulationStateChanged(SimulationStateManager.SimulationStates oldState, SimulationStateManager.SimulationStates newState)
    {
        if(oldState == SimulationStateManager.SimulationStates.INTERMISSION)
        {
            setSphereSizeParameter(leftSphere, currentRun.sizeLeft);
            setSphereSizeParameter(rightSphere, currentRun.sizeRight);

            setSphereMaterialParameter(leftSphere, currentRun.materialLeft);
            setSphereMaterialParameter(rightSphere, currentRun.materialRight);
        }
    }

    protected static void setSphereSizeParameter(GameObject sphere, SphereSize size)
    {
        SphereSizeParameter sizeParameter = sphere.GetComponent<SphereSizeParameter>();
        if (sizeParameter != null)
        {
            sizeParameter.setValue(size);
        }
        else
        {
            // TODO error
        }
    }

    protected static void setSphereMaterialParameter(GameObject sphere, SphereMaterial material)
    {
        Debug.Log(material.name);
        SphereMaterialParameter materialParameter = sphere.GetComponent<SphereMaterialParameter>();
        if (materialParameter != null)
        {
            materialParameter.setValue(material);
        }
        else
        {
            // TODO error
        }
    }

    public static void addNewRunForSimulationType(SimulationSelector.SimulationType type, Run run)
    {
        List<Run> runs;
        if (!runsBySimulationType.TryGetValue(type, out runs))
        {
            runs = new List<Run>();
            runsBySimulationType.Add(type, runs);
        }
        runs.Add(run);
    }

    protected virtual bool isToggleGroupActive(string groupName)
    {
        GameObject go = GameObject.Find(groupName);
        if (go != null)
        {
            ToggleGroup toggleGroup = go.transform.GetComponent<ToggleGroup>();
            if (toggleGroup != null)
            {
                IEnumerable<Toggle> activeToggles = toggleGroup.ActiveToggles();
                if (activeToggles == null || !activeToggles.Any())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public override bool canProceed()
    {
        return isToggleGroupActive("PredictButtons") && isToggleGroupActive("MaterialToggleGroup") && isToggleGroupActive("SizeToggleGroup");
    }

    public void hideCursor()
    {
       Cursor.lockState = CursorLockMode.Locked;

    }

    public void showCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public override void saveData()
    {
        ParticipantDataDisplace data = new ParticipantDataDisplace();
        data.trialIdent = currentRun.trialIdent;
        data.conditionActor = SimulationSelector.simulationActor;
        data.conditionActivity = SimulationSelector.simulationActivity;
        ParticipantDataLogger.saveParticipantData(data);
    }
}
