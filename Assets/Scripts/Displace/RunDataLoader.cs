using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using System;
using NReco.Csv;

public class RunDataLoader : MonoBehaviour
{
    private const int NUM_COLUMS = 12;
    public enum ColumnHeaderIndices
    {
        TRIAL_IDENT,
        ACTIVITY,
        ACTOR,
        SIZE_LEFT,
        SIZE_RIGHT,
        MATERIAL_LEFT,
        MATERIAL_RIGHT,
        CORRECT_ANSWER,
        CONSTRUCT_TASK,
        YOKED_PREDICTION,
        YOKED_SIZE,
        YOKED_MATERIAL
    }

    public void loadRunData()
    {
        List<string> readText = readRunInputs();
        if (readText == null || (readText.Count % NUM_COLUMS) != 0)
        {
            //TOTO error
        }
        else
        {
            parseInputStings(readText);
        }
    }

    private void parseInputStings(List<string> inputs)
    {
        int row = 1;
        int numRows = inputs.Count / NUM_COLUMS;
        int loadedRuns = 0;
        while (row < numRows)
        {
            List<string> rowData = inputs.GetRange(NUM_COLUMS * row, NUM_COLUMS);
            string trialIdent = rowData[0];
            SimulationSelector.SimulationType simulationType = SimulationSelector.SimulationType.SELF_PREDICT;
            if (!string.IsNullOrEmpty(trialIdent))
            {
                string activity = rowData[1];
                string actor = rowData[2];
                if (contrainsString(activity, "predict"))
                {
                    if (contrainsString(actor, "yoked"))
                    {
                        simulationType = SimulationSelector.SimulationType.YOKED_PREDICT;
                    }
                    else if (contrainsString(actor, "self"))
                    {
                        simulationType = SimulationSelector.SimulationType.SELF_PREDICT;
                    }
                }
                else if (contrainsString(activity, "construct"))
                {
                    if (contrainsString(actor, "self"))
                    {
                        simulationType = SimulationSelector.SimulationType.SELF_CONSTRUCT;
                    }
                    else if (contrainsString(actor, "yoked"))
                    {
                        simulationType = SimulationSelector.SimulationType.YOKED_CONSTRUCT;
                    }
                }
                else
                {
                    Debug.LogErrorFormat("Could not load trial data from row: " + row);
                    continue;
                }

                Run runToAdd = new Run();
                if (runToAdd.tryInitializeFromStrings(rowData))
                {
                    DisplacementSim.addNewRunForSimulationType(simulationType, runToAdd);
                    ++loadedRuns;
                }
            }
            ++row;
        }
        Debug.LogErrorFormat("Loaded trial data for " + loadedRuns + " trials");
    }

    public static bool contrainsString(string source, string stringToContain)
    {
        return source.IndexOf(stringToContain, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private List<string> readRunInputs()
    {
        List<string> values = new List<string>();

        var filePath = Path.Combine(Application.persistentDataPath, "input.csv");

        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("Error reading {0}\nFile does not exist!", filePath); // TODO show error box
        }

        using (var file = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var streamRdr = new StreamReader(file)) 
            {
                var csvReader = new CsvReader(streamRdr, ",");
                while (csvReader.Read()) 
                {   
                    for (int i = 0; i < csvReader.FieldsCount; ++i) 
                    {
                        string val = csvReader[i];
                        values.Add(val);
                    }
                } 
            }     
        }
        return values;
    }

    public void writeRunData()
    {
        var filePath = Path.Combine(Application.persistentDataPath, "test");

        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (var writer = new StreamWriter(file, Encoding.UTF8))
            {
                writer.Write("testtesttest");
            }
        }

        Debug.LogFormat("Written to {0}", filePath);
    }
}
