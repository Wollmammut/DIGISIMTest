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
    protected GameObject predictButtons;
    protected GameObject sizeButtons;
    protected GameObject materialButtons;
    Vector3 leftSphereStartPosition;
    Vector3 leftSphereTargetPosition;
    Vector3 rightSphereStartPosition;
    Vector3 rightSphereTargetPosition;
    Vector3 leftPlungerStartPosition;
    Vector3 leftPlungerTargetPosition;
    Vector3 rightPlungerStartPosition;
    Vector3 rightPlungerTargetPosition;
    float secondsToLowerSpheres = 5;
    float fractionOfLoweringTimePassed;
    float secondsWaited = 0;
    static List<Run> runs = new List<Run>();
    public static Dictionary<SimulationSelector.SimulationType, List<Run>> runsBySimulationType = new Dictionary<SimulationSelector.SimulationType, List<Run>>();
    static Dictionary<SimulationSelector.SimulationType, List<Run>> trainingRunsBySimulationType = new Dictionary<SimulationSelector.SimulationType, List<Run>>();

    protected Run currentRun;
    protected bool isTrainingRun;

    protected override void Start()
    {
        base.Start();
        //secondsToLowerSpheres = showAnimations ? 5 : 0;
    }

    void Update()
    {
        SimulationStateManager stateManager = GetComponent<SimulationStateManager>(); // TODO null check
        SimulationStateManager.SimulationStates currentState = stateManager.getCurrentSimulationState();
        if (currentState == SimulationStateManager.SimulationStates.ACTIVE)
        {
            float secondsToWait = secondsToLowerSpheres + 3.5f;
            secondsWaited += Time.deltaTime;
            if (secondsWaited > secondsToWait) // not a very good solution
            {
                showCursor();
                stateManager.toggleSimulationActive();
            }
        }
    }

    public override void initialize()
    {
        // TODO just for testing
        Run run = new Run();
        runs.Add(run);
        // TODO testing end
        isTrainingRun = getStateManager().isTrainingMode();
        currentRun = getCurrentRun();

        // setSphereSizeParameter(leftSphere, SphereSize.MEDIUM);
        // setSphereSizeParameter(rightSphere, SphereSize.MEDIUM);
        // setSphereMaterialParameter(leftSphere, SphereMaterial.WOOD);
        // setSphereMaterialParameter(rightSphere, SphereMaterial.WOOD);

        setSphereSizeParameter(leftSphere, currentRun.sizeLeft);
        setSphereSizeParameter(rightSphere, currentRun.sizeRight);

        setSphereMaterialParameter(leftSphere, currentRun.materialLeft);
        setSphereMaterialParameter(rightSphere, currentRun.materialRight);

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

        predictButtons = GameObject.Find("PredictButtons");
        sizeButtons = GameObject.Find("SizeToggleGroup");
        materialButtons = GameObject.Find("MaterialToggleGroup");
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

        Dictionary<SimulationSelector.SimulationType, List<Run>> runsToGetFrom = isTrainingRun ? trainingRunsBySimulationType : runsBySimulationType;
        if (runsToGetFrom.TryGetValue(currentSimulationType, out runs))
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
        hideCursor();
        if (ShowAnimations())
        {
            fractionOfLoweringTimePassed += Time.deltaTime/secondsToLowerSpheres;
            leftSphere.transform.position = Vector3.Lerp(leftSphereStartPosition, leftSphereTargetPosition, fractionOfLoweringTimePassed);
            rightSphere.transform.position = Vector3.Lerp(rightSphereStartPosition, rightSphereTargetPosition, fractionOfLoweringTimePassed);
            leftPlunger.transform.position = Vector3.Lerp(leftPlungerStartPosition, leftPlungerTargetPosition, fractionOfLoweringTimePassed);
            rightPlunger.transform.position = Vector3.Lerp(rightPlungerStartPosition, rightPlungerTargetPosition, fractionOfLoweringTimePassed);
        }
        else
        {
            leftSphere.transform.position = leftSphereTargetPosition;
            rightSphere.transform.position = rightSphereTargetPosition;
            leftPlunger.transform.position = leftPlungerTargetPosition;
            rightPlunger.transform.position = rightPlungerTargetPosition;
        }
    }

    protected override void onActivation()
    {
    }

    protected void showPredictButtons(bool show)
    {
        if(predictButtons == null)
        {
            //TODO error
        }
        else
        {
            if (predictButtons.activeSelf == show) return;
            predictButtons.SetActive(show);
            if (show)
            {
                ToggleGroup toggleGroup = predictButtons.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
                foreach(Transform child in predictButtons.transform)
                {
                    Toggle t = child.GetComponent<Toggle>();
                    t.SetIsOnWithoutNotify(false);
                }
            }
        }
    }

    protected void showSizeToggles(bool show)
    {
        if(sizeButtons == null)
        {
            //TODO error
        }
        else
        {
            if (sizeButtons.activeSelf == show) return;
            sizeButtons.SetActive(show);
            if (show)
            {
                ToggleGroup toggleGroup = sizeButtons.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
            }
        }
    }

    public void showMaterialToggles(bool show)
    {
        if(materialButtons == null)
        {
            //TODO error
        }
        else
        {
            if (materialButtons.activeSelf == show) return;
            materialButtons.SetActive(show);
            if (show)
            {
                ToggleGroup toggleGroup = materialButtons.GetComponent<ToggleGroup>();// TODO null check
                toggleGroup.SetAllTogglesOff();
            }
        }
    }

    public void setToggleGroupInteractable(bool interactable, string groupName)
    {
        GameObject toggleGroupGO = GameObject.Find(groupName); 
        if (toggleGroupGO == null)
        {
            return;
        }
        foreach(Transform child in toggleGroupGO.transform)
        {
            Toggle t = child.GetComponent<Toggle>();
            if (t != null)
            {
                t.interactable = interactable;
                Debug.Log(t.interactable);
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

    public override void onStepAdvancement(SimulationStep step, int currentStepIndex)
    {

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

    public static void addTainingRuns(Dictionary<SimulationSelector.SimulationType, List<Run>> runsIn)
    {
        trainingRunsBySimulationType = runsIn;
    }

    public static void addRuns(Dictionary<SimulationSelector.SimulationType, List<Run>> runsIn)
    {
        runsBySimulationType = runsIn;
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

    public override void reset()
    {
        setSphereSizeParameter(leftSphere, SphereSize.MEDIUM);
        setSphereSizeParameter(rightSphere, SphereSize.MEDIUM);
        setSphereMaterialParameter(leftSphere, SphereMaterial.WOOD);
        setSphereMaterialParameter(rightSphere, SphereMaterial.WOOD);
    }

    protected SimulationStateManager getStateManager()
    {
        return gameObject.GetComponent<SimulationStateManager>();
    }

    public void showLeftSphereAndPlunger(bool show)
    {
        leftSphere.SetActive(show);
        leftPlunger.SetActive(show);
    }

    public override int getNumberOfRuns()
    {
        SimulationSelector.SimulationType currentSimulationType = SimulationSelector.currentSimulationType;
        List<Run> runs;

        Dictionary<SimulationSelector.SimulationType, List<Run>> runsToGetFrom = isTrainingRun ? trainingRunsBySimulationType : runsBySimulationType;
        if (runsToGetFrom.TryGetValue(currentSimulationType, out runs))
        {
            return runs.Count;
        }
        return 0;
    }
}
