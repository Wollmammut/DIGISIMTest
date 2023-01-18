using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    public GameObject nextPanelToDisplay;

    public void changeScreen()
    {
        if(nextPanelToDisplay == null) // TODO this will not work as a way to switch over to the simulation, use scene switcher on last panel instead
        {
            startSimulation();
        }
        else
        {
            nextPanelToDisplay.SetActive(true);
        }
        GameObject parent = gameObject.transform.parent.gameObject;
        parent.SetActive(false);
    }

    void startSimulation()
    {
        GameObject sim = GameObject.Find("Simulation");
        SimulationStateManager simulationStateManager = sim.GetComponent<SimulationStateManager>();
        simulationStateManager.initializeSimulation();
    }
}
