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
    public static string simulationActor; // you know, if c# enums weren't as shit as they are those two fields could be enum properties... and I refuse to make another stupid switch over enums. Fix yo enums, c#...
    public static string simulationActivity;

    public static void setSelf()
    {
        selfYoked = SelfYoked.SELF;
        simulationActor = "self";
    }

    public static void setYoked()
    {
        selfYoked = SelfYoked.YOKED;
        simulationActor = "yoked";
    }

    public static void setPredict()
    {
        predictConstruct = PredictConstruct.PREDICT;
        simulationActivity = "predict";
    }

    public static void setConstruct()
    {
        predictConstruct = PredictConstruct.CONSTRUCT;
        simulationActivity = "construct";
    }

    private static void setSceneNames()
    {
        //TODO error handling when scenes are not found
        // yeah, I know, bitflags...
        if (selfYoked == SelfYoked.SELF)
        {
            setSelf();
            if(predictConstruct == PredictConstruct.PREDICT)
            {
                currentSimulationScenes = new SimulationScenes("SelfPredictScene", "SelfPredictInstructionScene");
                currentSimulationType = SimulationType.SELF_PREDICT;
                setPredict();
                //SceneManager.LoadScene("SelfPredictInstructionScene");
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("SelfConstructScene", "SelfConstructInstructionScene");
                currentSimulationType = SimulationType.SELF_CONSTRUCT;
                //SceneManager.LoadScene("SelfConstructInstructionScene");
                setConstruct();
            }
        }
        else
        {
            setYoked();
            if(predictConstruct == PredictConstruct.PREDICT)
            {
                currentSimulationScenes = new SimulationScenes("YokedPredictScene", "YokedPredictInstructionScene");
                currentSimulationType = SimulationType.YOKED_PREDICT;
                //SceneManager.LoadScene("YokedPredictInstructionScene");
                setPredict();
            }
            else
            {
                currentSimulationScenes = new SimulationScenes("YokedConstructScene", "YokedConstructInstructionScene");
                currentSimulationType = SimulationType.YOKED_CONSTRUCT;
                //SceneManager.LoadScene("YokedConstructInstructionScene");
                setConstruct();
            }
        }
    }

    public static void continueToSimulationInstruction()
    {
        setSceneNames();
        SceneManager.LoadScene(currentSimulationScenes.instructionScene);
    }
    public static void continueToSimulation(bool trainingMode)
    {
        SimulationStateManager.setTrainingMode(trainingMode);
        setSceneNames();
        SceneManager.LoadScene(currentSimulationScenes.simulationScene);
    }
}
