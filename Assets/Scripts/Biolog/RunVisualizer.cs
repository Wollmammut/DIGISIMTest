using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunVisualizer : MonoBehaviour
{
    public bool showDataFromPreviousRun;
    Dictionary<string, CritterAmountParameter> parametersByName = new Dictionary<string, CritterAmountParameter>();

    // Start is called before the first frame update
    void Start()
    {
        if (showDataFromPreviousRun)
        {
            parametersByName = BiologSim.previousParametersByName;
            if (SimulationStateManager.getCurrentRunNumber() == 0)
            {
                gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            parametersByName = BiologSim.parametersByName;
        }

        foreach(KeyValuePair<string, CritterAmountParameter> entry in parametersByName)
        {
            Transform t = transform.Find(entry.Key);
            if (t != null)
            {
                CritterAmountParameter parameterSetter = t.gameObject.GetComponent<CritterAmountParameter>();
                if (parameterSetter != null)
                {
                    parameterSetter.setValue(entry.Value.getCurrentParameterValue());
                }
            }
        }

    }
}
