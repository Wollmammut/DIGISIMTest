using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using System;
using NReco.Csv;
using System.Xml;

public class RunDataLoader : MonoBehaviour
{
    private int NUM_COLUMS = Enum.GetValues(typeof(ColumnHeaderIndices)).Length;
    public enum ColumnHeaderIndices
    {
        TRIAL_IDENT,
        ACTIVITY,
        ACTOR,
        SIZE_LEFT,
        SIZE_RIGHT,
        MATERIAL_LEFT,
        MATERIAL_RIGHT,
        //CORRECT_ANSWER,
        //CONSTRUCT_TASK,
        YOKED_PREDICTION,
        YOKED_SIZE,
        YOKED_MATERIAL
    }

    public void loadRunData()
    {
        ReadXMLFile(Path.Combine(Application.persistentDataPath, "inputx.xml"));
        List<string> readText = readRunInputs("input");
        if (readText == null || (readText.Count % NUM_COLUMS) != 0)
        {
            //TOTO error
        }
        else
        {
            Dictionary<SimulationSelector.SimulationType, List<Run>> runsBySimulationType = parseInputStings(readText);
            DisplacementSim.addRuns(runsBySimulationType);
        }
        List<string> readTrialText = readRunInputs("trial_input");
        if (readTrialText == null || (readTrialText.Count % NUM_COLUMS) != 0)
        {
            //TOTO error
        }
        else
        {
            Dictionary<SimulationSelector.SimulationType, List<Run>> trainingRunsBySimulationType = parseInputStings(readTrialText);
            DisplacementSim.addTainingRuns(trainingRunsBySimulationType);
        }
    }

    private Dictionary<SimulationSelector.SimulationType, List<Run>> parseInputStings(List<string> inputs)
    {
        int row = 1;
        int numRows = inputs.Count / NUM_COLUMS;
        int loadedRuns = 0;
        Dictionary<SimulationSelector.SimulationType, List<Run>> runsBySimulationType = new Dictionary<SimulationSelector.SimulationType, List<Run>>();
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
                    addNewRunForSimulationType(runsBySimulationType, simulationType, runToAdd);
                    ++loadedRuns;
                }
            }
            ++row;
        }
        Debug.Log("Loaded trial data for " + loadedRuns + " trials");
        return runsBySimulationType;
    }


    private static void addNewRunForSimulationType(Dictionary<SimulationSelector.SimulationType, List<Run>> runsBySimulationType, SimulationSelector.SimulationType type, Run run)
    {
        List<Run> runs;
        if (!runsBySimulationType.TryGetValue(type, out runs))
        {
            runs = new List<Run>();
            runsBySimulationType.Add(type, runs);
        }
        runs.Add(run);
    }

    public static bool contrainsString(string source, string stringToContain)
    {
        return source.IndexOf(stringToContain, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private List<string> readRunInputs(String fileName)
    {
        List<string> values = new List<string>();

        var filePath = Path.Combine(Application.persistentDataPath, fileName + ".csv");

        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("Error reading {0}\nFile does not exist!", filePath); // TODO show error box
            return null;
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

    static void ReadXMLFile(string filePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        XmlNode node = doc.DocumentElement.SelectSingleNode("/Worksheet/Table");
        
        //XmlNode root = doc.SelectSingleNode("//Worksheet[@tName='Tabelle1']");
        XmlNodeList list = doc.SelectNodes("//Worksheet");
        //Debug.Log(root.Name);
        //Display the contents of the child nodes.
        // if (list.HasChildNodes)
        // {
            // for (int i = 0; i < root.ChildNodes.Count; i++)
            // {
                foreach (XmlNode n in list)
                {
                    Debug.Log(n);
      }
                
            // }
        // }
    }
}
