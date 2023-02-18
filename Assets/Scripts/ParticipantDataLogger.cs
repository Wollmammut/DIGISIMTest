using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticipantDataLogger : MonoBehaviour
{
    private static string VPNCode;
    private static string age;
    private static ParticipantSex sex;
    private static int continueFlag;
    private const int VPN_FLAG = 1 << 0;
    private const int AGE_FLAG = 1 << 1;
    private const int RUNS_FLAG = 1 << 2;
    private static ParticipantData participantData;

    public enum ParticipantSex
    {
        MALE,
        FEMALE,
        DIVERS
    }

    private void Start()
    {
        VPNCode = "";
        age = "";
        sex = ParticipantSex.MALE;
        continueFlag = 0;
        participantData = new ParticipantData();
    }

    public void setVPNCode(string data)
    {
        VPNCode = data;
        continueFlag |= VPN_FLAG;
    }

    public void setNumberOfRuns(string data)
    {
        int n;
        bool isNumeric = int.TryParse(data, out n);
        if (isNumeric && n > 0)
        {
            SimulationStateManager.setMaxNumberOfRuns(n);
            continueFlag |= RUNS_FLAG;
        }
    }

    public void setAge(string data)
    {
        age = data;
        continueFlag |= AGE_FLAG;
    }

    public void setSex(int enumId)
    {
        switch (enumId)
        {
            case 0:
            sex = ParticipantSex.MALE;
            break;
            case 1:
            sex = ParticipantSex.FEMALE;
            break;
            default:
            sex = ParticipantSex.DIVERS;
            break;
        }
    }

    public static bool isAllDataSet()
    {
        return continueFlag == (AGE_FLAG | RUNS_FLAG | VPN_FLAG);
    }

    public static string getNameForSex(ParticipantSex participantSex)
    {
        switch (participantSex)
        {
            case ParticipantSex.MALE:
            return "male";
            case ParticipantSex.FEMALE:
            return "female";
            default:
            return "divers";
        }
    }

    public static void saveParticipantData(AdditionalApplicationData additionalData)
    {
        participantData.age = age;
        participantData.VPNCode = VPNCode;
        participantData.sex = getNameForSex(sex);

        participantData.additionalData = additionalData;
        
        participantData.addLoggedClicksForRunNumber(ClickLogger.getLoggedClicks(), SimulationStateManager.getCurrentRunNumber());

        string jsonString = JsonUtility.ToJson(participantData, true);
        writeRunJson(VPNCode + "_" + DateTime.Today, jsonString);

//         JsonSerializer serializer = new JsonSerializer();
// serializer.Converters.Add(new JavaScriptDateTimeConverter());
// serializer.NullValueHandling = NullValueHandling.Ignore;

// using (StreamWriter sw = new StreamWriter(Path.Combine(Application.persistentDataPath, VPNCode + "_" + DateTime.Today)))
// using (JsonWriter writer = new JsonTextWriter(sw))
// {
//     serializer.Serialize(writer, data);
// }

    }

    private static void writeRunJson(string fileName, string jsonString)
    {
        var filePath = Path.Combine(Application.persistentDataPath, fileName);

        using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            using (var writer = new StreamWriter(file, Encoding.UTF8))
            {
                writer.Write(jsonString);
            }
        }
    }
}
