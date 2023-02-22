using System;
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
    public string correctAnswer;
    public ConstructionTask.EnumConstructionTask constructTask = ConstructionTask.EnumConstructionTask.SMALLER;
    public SpherePredictionSelector.Prediction yokedPrediction = SpherePredictionSelector.Prediction.LEFT;
    public SphereSize yokedConstructSize = SphereSize.SMALL;
    public SphereMaterial yokedConstructMaterial = SphereMaterial.WOOD;
    public string trialIdent;

    public bool tryInitializeFromStrings(List<string> strings)
    {
        if (strings.Count != RunDataLoader.ColumnHeaderIndices.GetNames(typeof(RunDataLoader.ColumnHeaderIndices)).Length)
        {
            return false;
        }

        bool initializedCorrectly = true;

        trialIdent = strings[(int)RunDataLoader.ColumnHeaderIndices.TRIAL_IDENT];
        string sizeName = strings[(int)RunDataLoader.ColumnHeaderIndices.SIZE_LEFT];
        initializedCorrectly &= assignSphereSize(ref sizeLeft, sizeName);
        sizeName = strings[(int)RunDataLoader.ColumnHeaderIndices.SIZE_RIGHT];
        initializedCorrectly &= assignSphereSize(ref sizeRight, sizeName);
        
        string materialName = strings[(int)RunDataLoader.ColumnHeaderIndices.MATERIAL_LEFT];
        initializedCorrectly &= assignSphereMaterial(ref materialLeft, materialName);
        materialName = strings[(int)RunDataLoader.ColumnHeaderIndices.MATERIAL_RIGHT];
        initializedCorrectly &= assignSphereMaterial(ref materialRight, materialName);

        correctAnswer = strings[(int)RunDataLoader.ColumnHeaderIndices.CORRECT_ANSWER];

        string constructTaskName = strings[(int)RunDataLoader.ColumnHeaderIndices.CONSTRUCT_TASK];
        constructTask = ConstructionTask.getConstructionTaskByName(constructTaskName);

        yokedPrediction = SpherePredictionSelector.getPredictionFromName( strings[(int)RunDataLoader.ColumnHeaderIndices.YOKED_PREDICTION]);

        string yokedSizeName = strings[(int)RunDataLoader.ColumnHeaderIndices.YOKED_SIZE];
        initializedCorrectly &= assignSphereSize(ref yokedConstructSize, yokedSizeName);

        string yokedMaterialName = strings[(int)RunDataLoader.ColumnHeaderIndices.YOKED_MATERIAL];
        initializedCorrectly &= assignSphereMaterial(ref yokedConstructMaterial, yokedMaterialName);

        return initializedCorrectly | true;
    }

    private bool assignSphereSize(ref SphereSize variableToAssignTo, string name)
    {
        SphereSize size = SphereSize.getSphereSizeParameterFromName(name);
        if (size == null)
        {
            return false;
        }
        variableToAssignTo = size;
        return true;
    }

    private bool assignSphereMaterial(ref SphereMaterial variableToAssignTo, string name)
    {
        SphereMaterial material = SphereMaterial.getSphereMaterialParameterFromName(name);
        if (material == null)
        {
            material = SphereMaterial.NONE;
        }
        variableToAssignTo = material;
        return true;
    }
}
