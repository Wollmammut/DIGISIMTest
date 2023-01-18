using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class ConclusionProceed : MonoBehaviour
{
    public GameObject errorPopup;
    public GameObject impactToggles;

    void Start()
    {
        errorPopup.SetActive(false);
    }

    public void onClick()
    {
        foreach (Transform toggleGroupTransform in impactToggles.transform)
        {
            ToggleGroup toggleGroup = toggleGroupTransform.GetComponent<ToggleGroup>();
            if (toggleGroup != null)
            {
                IEnumerable<Toggle> activeToggles = toggleGroup.ActiveToggles();
                if (activeToggles == null || !activeToggles.Any())
                {
                    errorPopup.SetActive(true);
                    return;
                }
            }
        } 
        // if (simulation.canProceed())
        // {
        //     SimulationStateManager stateManager = impactToggles.GetComponent<SimulationStateManager>();
        //     stateManager.toggleSimulationActive();
        // }
    }
}
