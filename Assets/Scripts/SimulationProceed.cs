using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationProceed : MonoBehaviour
{
    public GameObject errorPopup;
    GameObject simulationGO;

    void Start()
    {
        errorPopup.SetActive(false);
        simulationGO = GameObject.Find("Simulation");
        if (simulationGO == null)
        {
            // TODO some kind of error
        }
    }

    public void onClick()
    {
        Simulation simulation = simulationGO.GetComponent<Simulation>(); // TODO refactor and let either the statemaneger or the sim decide if to proceed? AT the statemaneger does a second check if it has runs...
        if (simulation.canProceed())
        {
            SimulationStateManager stateManager = simulationGO.GetComponent<SimulationStateManager>();
            stateManager.toggleSimulationActive();
        }
        else
        {
            errorPopup.SetActive(true);
        }
    }
}
