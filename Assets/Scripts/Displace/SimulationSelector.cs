using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationSelector : MonoBehaviour
{
    public enum SimulationType
    {
        SELF_CONSTRUCT,
        SELF_PREDICT,
        YOKED_CONSTRUCT,
        YOKED_PREDICT
    }
    public enum SelfYoked
    {
        SELF,
        YOKED
    }
    public enum PredictConstruct
    {
        PREDICT,
        CONSTRUCT
    }

    public readonly struct SimulationScenes
    {
        public readonly string simulationScene;
        public readonly string instructionScene;

        public SimulationScenes(string simulationScene, string instructionScene)
        {
            this.simulationScene = simulationScene;
            this.instructionScene = instructionScene;
        }
    }

    public static SimulationType currentSimulationType;
    public static SelfYoked selfYoked;
    public static PredictConstruct predictConstruct;
    public static SimulationScenes currentSimulationScenes = new SimulationScenes("SelfPredictScene", "SelfPredictInstructionScene");

    public void setSelf()
    {
        selfYoked = SelfYoked.SELF;
    }

    public void setYoked()
    {
        selfYoked = SelfYoked.YOKED;
    }

    public void setPredict()
    {
        predictConstruct = PredictConstruct.PREDICT;
    }

    public void setConstruct()
    {
        predictConstruct = PredictConstruct.CONSTRUCT;
    }

    private static void setSceneNames()
    {
        //TODO error handling when scenes are not found
        // yeah, I know, bitflags...
        if (selfYoked == SelfYoked.SELF)
        {
            if(predictConstruct == PredictConstruct.PREDICT)
            {
                currentSimulationScenes = new SimulationScenes("SelfPredictScene", "SelfPredictInstructionScene");
                currentSimulationType = SimulationType.SELF_PREDICT;
                //SceneManager.LoadScene("SelfPredictInstructionScene");
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("SelfConstructScene", "SelfConstructInstructionScene");
                currentSimulationType = SimulationType.SELF_CONSTRUCT;
                //SceneManager.LoadScene("SelfConstructInstructionScene");
            }
        }
        else
        {
            if(predictConstruct == PredictConstruct.PREDICT)
            {
                currentSimulationScenes = new SimulationScenes("YokedPredictScene", "YokedPredictInstructionScene");
                currentSimulationType = SimulationType.YOKED_PREDICT;
                //SceneManager.LoadScene("YokedPredictInstructionScene");
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("YokedConstructScene", "YokedConstructInstructionScene");
                currentSimulationType = SimulationType.YOKED_CONSTRUCT;
                //SceneManager.LoadScene("YokedConstructInstructionScene");
            }
        }
    }

    public static void continueToSimulationInstruction()
    {
        setSceneNames();
        SceneManager.LoadScene(currentSimulationScenes.instructionScene);
    }
    public static void continueToSimulation()
    {
        setSceneNames();
        SceneManager.LoadScene(currentSimulationScenes.simulationScene);
    }
}
