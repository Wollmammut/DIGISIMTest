using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePredictionSelector : MonoBehaviour
{
    public static Prediction prediction;// TODO was protected and not static, what did I have in mind with this? 
    public Prediction predictionOnToggle;
    public enum Prediction
    {
        DEFINITELY_LEFT,
        LEFT,
        UNSURE,
        RIGHT,
        DEFINITELY_RIGHT
    }

    public static Prediction getPredictionFromName(string name)
    {
        switch(name)
        {
            case "<<":
            return Prediction.DEFINITELY_LEFT;
            case "<":
            return Prediction.LEFT;
            case "0":
            return Prediction.UNSURE;
            case ">":
            return Prediction.RIGHT;
            case ">>":
            return Prediction.DEFINITELY_RIGHT;
            default:
            return Prediction.UNSURE;
        }
    }

    public static string getNameForPrediction(SpherePredictionSelector.Prediction prediction)
    {
        string toggleName = "";
        switch(prediction)
        {
            case SpherePredictionSelector.Prediction.DEFINITELY_LEFT:
            toggleName = "definitiv links";
            break; 
            case SpherePredictionSelector.Prediction.LEFT:
            toggleName = "links";
            break; 
            case SpherePredictionSelector.Prediction.UNSURE:
            toggleName = "unsicher";
            break; 
            case SpherePredictionSelector.Prediction.RIGHT:
            toggleName = "rechts";
            break; 
            case SpherePredictionSelector.Prediction.DEFINITELY_RIGHT:
            toggleName = "definitiv rechts";
            break; 
        }
        return toggleName;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked()
    {
        prediction = predictionOnToggle;
    }
}
