using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class DisplacementSim : Simulation
{
    public GameObject panelForAIKidsWords;
    public GameObject leftSphere;
    public GameObject rightSphere;
    Vector3 leftSphereStartPosition;
    Vector3 leftSphereTargetPosition;
    Vector3 rightSphereStartPosition;
    Vector3 rightSphereTargetPosition;
    float fractionOfLoweringTimePassed;
    static List<Run> runs = new List<Run>();
    protected Run currentRun;

    protected override void Start()
    {
        // TODO load data for runs somewhere
        base.Start();
        
    }

    public override void initialize()
    {
        // TODO just for testing
        Run run = new Run();
        runs.Add(run);
        // TODO testing end

        currentRun = getCurrentRun(); // TODO null check

        // SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        // spl.setValue(currentRun.sizeLeft);
        // SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        // spr.setValue(currentRun.sizeRight);

        // SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        // SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();

        leftSphereStartPosition = leftSphere.transform.position;
        leftSphereTargetPosition = leftSphereStartPosition - new Vector3(0, 150, 0);
        rightSphereStartPosition = rightSphere.transform.position;
        rightSphereTargetPosition = rightSphereStartPosition - new Vector3(0, 150, 0);
    }

    public Run getCurrentRun()
    {
        if (currentRun == null)
        {
            int currentRunNumber = SimulationStateManager.getCurrentRunNumber();
            return getRunForRunNumber(currentRunNumber); // TODO null check
        }
        else
        {
            return currentRun;
        }
    }

    protected Run getRunForRunNumber(int runNumber)
    {
       return runs[runNumber]; // TODO bounds check
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
        leftSphere.transform.position = Vector3.Lerp(leftSphereStartPosition, leftSphereTargetPosition, fractionOfLoweringTimePassed);
        rightSphere.transform.position = Vector3.Lerp(rightSphereStartPosition, rightSphereTargetPosition, fractionOfLoweringTimePassed);
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
        SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        spl.setValue(currentRun.sizeLeft);
        SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        spr.setValue(currentRun.sizeRight);

        SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        materialParameterLeft.setValue(currentRun.materialLeft);
        SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();
        materialParameterRight.setValue(currentRun.materialRight);
        }
    }
}
