using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologSim : Simulation
{
    public GameObject gauge;
    public static Dictionary<string, CritterAmountParameter> parametersByName = new Dictionary<string, CritterAmountParameter>(); // TODO maybe make generic and put into simulation?
    public static Dictionary<string, CritterAmountParameter> previousParametersByName = new Dictionary<string, CritterAmountParameter>();

    protected override void onActivation()
    {

    }

    public override void initialize()
    {

    }

    public override bool canProceed()
    {
        Transform t = transform.Find("Organisms");
        GameObject organisms = t.gameObject;
        foreach (Transform organismTransform in organisms.transform)
        {
            if (!organismTransform.gameObject.activeSelf)
            {
                return false;
            }
            // CritterAmountParameter amountParameter = organismTransform.gameObject.GetComponent<CritterAmountParameter>();
            // if (amountParameter == null)
            // {
            //     //TOTO error
            // }
            // if (amountParameter.getCurrentParameterValue() == null)
            // {
            //     return false;
            // }
        }
        return true;
    }

    public override void onSimulationStateChanged(SimulationStateManager.SimulationStates oldState, SimulationStateManager.SimulationStates newState)
    {
        if (newState == SimulationStateManager.SimulationStates.ACTIVE)
        {
            setGauge();
        }
    }

    void Update()
    {
        setGauge();
    }
    void setGauge()
    {
        int soilQuality = 2;
            foreach(CritterAmountParameter parameter in parametersByName.Values)
            {
                soilQuality += 1 * parameter.getEffectOnSoilQuality();
            }
            int minSoilQuality = 1;
            int maxSoilQuality = 5;
            soilQuality = Math.Clamp(soilQuality, minSoilQuality, maxSoilQuality);

            gauge.SetActive(true);
            Gauge g = gauge.GetComponent<Gauge>();
            if (g != null)
            {
                g.positionHandle(getGaugeAngleForSoilQuality(soilQuality));
            }
    }

    public static int getGaugeAngleForSoilQuality(int soilQuality)
    {
        int minAngle = 18;
        int maxAngle = 162;
        int anglePerSoilQuality = 36;
        return Math.Clamp(soilQuality * anglePerSoilQuality, minAngle, maxAngle);
    }

    public static void setOrganismAmountParameter(string name, CritterAmountParameter parameter)
    {
        parametersByName[name] = parameter;
    }

    public override void saveData()
    {
       // TODO 
    }
}
