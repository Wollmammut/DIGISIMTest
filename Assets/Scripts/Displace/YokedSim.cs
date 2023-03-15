using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YokedSim : DisplacementSim
{
    public GameObject pointer;
    protected GameObject buttonToMoveTo;
    protected float fractionOfTimePassed;
    protected Vector3 pointerStartPosition;
    protected Vector3 buttonPosition;
    protected bool hasReachedTarget = false;
    protected List<YokedButtonTarget> buttonTargets = new List<YokedButtonTarget>();
    protected int buttonTargetsIndex = 0;

    public struct YokedButtonTarget
    {
        public GameObject buttonToMoveTo;
        public Vector3 buttonPosition;
    }

    public override void initialize()
    {
        base.initialize();
        hideCursor();
    }

    protected void movePointerToTarget()
    {
        float secondsToReachTarget = 5; // take 5s to get to button;
        fractionOfTimePassed += Time.deltaTime/secondsToReachTarget;
        pointer.transform.position = Vector3.Lerp(pointerStartPosition, buttonPosition, fractionOfTimePassed);

        if (Vector3.Distance(pointer.transform.position, buttonPosition) < 1)
        {
            hasReachedTarget = true;
        }
    }

    protected override void FixedUpdate()
    {
        SimulationStateManager stateManager = GetComponent<SimulationStateManager>(); // TODO null check
        SimulationStateManager.SimulationStates currentState = stateManager.getCurrentSimulationState();
        if (currentState == SimulationStateManager.SimulationStates.DEMO)
        {
            if (hasReachedTarget)
            {
                Toggle toggle = buttonToMoveTo.GetComponent<Toggle>();
                if (toggle != null)
                {
                    toggle.isOn = true;
                    hasReachedTarget = false;
                    chooseNewTarget();
                }
            }
            else
            {
                movePointerToTarget();
            }
        }
        base.FixedUpdate();
    }

    protected YokedButtonTarget getButtonTargetForButtonName(string buttonName)
    {
        buttonToMoveTo = GameObject.Find(buttonName); // TODO error if no toggle found
        buttonPosition = new Vector3(buttonToMoveTo.transform.position.x, buttonToMoveTo.transform.position.y, -200);
        YokedButtonTarget target = new YokedButtonTarget();
        target.buttonToMoveTo = buttonToMoveTo;
        target.buttonPosition = buttonPosition;
        return target;
    }

    protected void setToggleAsTarget(YokedButtonTarget buttonTarget)
    {
        buttonToMoveTo = buttonTarget.buttonToMoveTo; 
        buttonPosition = buttonTarget.buttonPosition;
        pointerStartPosition = pointer.transform.position;
        fractionOfTimePassed = 0;
    }

    protected virtual void chooseNewTarget()
    {
        if (buttonTargets.Count > buttonTargetsIndex + 1)
        {
            hideCursor();
            YokedButtonTarget target = buttonTargets[++buttonTargetsIndex];
            setToggleAsTarget(target);
        }
        else
        {
            showCursor();
        }
    }

    public override void onSimulationStateChanged(SimulationStateManager.SimulationStates oldState, SimulationStateManager.SimulationStates newState)
    {
        base.onSimulationStateChanged(oldState, newState);
        if (newState == SimulationStateManager.SimulationStates.DEMO)
        {
            hideCursor();
        }
        if(oldState == SimulationStateManager.SimulationStates.INTERMISSION)
        {
        //SphereSizeParameter spl = leftSphere.GetComponent<SphereSizeParameter>();
        //spl.setValue(currentRun.sizeLeft);
        //SphereSizeParameter spr = rightSphere.GetComponent<SphereSizeParameter>();
        //spr.setValue(currentRun.sizeRight);

        //SphereMaterialParameter materialParameterLeft = leftSphere.GetComponent<SphereMaterialParameter>();
        //SphereMaterialParameter materialParameterRight = rightSphere.GetComponent<SphereMaterialParameter>();

        resetButtonTargets(); 
        setupSimulationWithValuesFromCurrentRun();
        setToggleAsTarget(buttonTargets[0]);
        }
    }

    protected void resetButtonTargets()
    {
        buttonTargets.Clear(); 
        buttonTargetsIndex = 0;
        hasReachedTarget = false;
    }

    protected virtual void setupSimulationWithValuesFromCurrentRun()
    {

    }

    protected string getToggleNameForPrediction(SpherePredictionSelector.Prediction prediction)
    {
        string toggleName = "";
        switch(prediction)
        {
            case SpherePredictionSelector.Prediction.DEFINITELY_LEFT:
            toggleName = "PredictDefinitelyLeftToggle";
            break; 
            case SpherePredictionSelector.Prediction.LEFT:
            toggleName = "PredictLeftToggle";
            break; 
            case SpherePredictionSelector.Prediction.UNSURE:
            toggleName = "PredictUnsureToggle";
            break; 
            case SpherePredictionSelector.Prediction.RIGHT:
            toggleName = "PredictRightToggle";
            break; 
            case SpherePredictionSelector.Prediction.DEFINITELY_RIGHT:
            toggleName = "PredictDefinitelyRightToggle";
            break; 
        }
        return toggleName;
    }
}
