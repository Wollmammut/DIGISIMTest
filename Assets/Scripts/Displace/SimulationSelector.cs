using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationSelector : MonoBehaviour
{
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
                //SceneManager.LoadScene("SelfPredictInstructionScene");
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("SelfConstructScene", "SelfConstructInstructionScene");
                //SceneManager.LoadScene("SelfConstructInstructionScene");
            }
        }
        else
        {
            if(predictConstruct == PredictConstruct.PREDICT)
            {
                currentSimulationScenes = new SimulationScenes("YokedPredictScene", "YokedPredictInstructionScene");
                //SceneManager.LoadScene("YokedPredictInstructionScene");
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("YokedConstructScene", "YokedConstructInstructionScene");
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
