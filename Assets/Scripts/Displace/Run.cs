using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run 
{
    // stores key value pairs of the name of a parameter type (e.g. "SphereSize") and its actual value (e.g. "small")
    // TODO deprecated?
    public Dictionary<string, string> parameterTypeNameToParameterValueName = new Dictionary<string, string>();
    public SphereSize sizeLeft = SphereSize.SMALL;
    public SphereSize sizeRight = SphereSize.SMALL;
    public SphereMaterial materialLeft = SphereMaterial.WOOD;
    public SphereMaterial materialRight = SphereMaterial.LEAD;
    public List<string> correctAnswer;
    public ConstructionTask.EnumConstructionTask constructTask = ConstructionTask.EnumConstructionTask.SMALLER;
    public SpherePredictionSelector.Prediction yokedPrediction = SpherePredictionSelector.Prediction.LEFT;
    public SphereSize yokedConstructSize = SphereSize.SMALL;
    public SphereMaterial yokedConstructMaterial = SphereMaterial.WOOD;
}
