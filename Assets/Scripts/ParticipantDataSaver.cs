using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParticipantDataSaver : MonoBehaviour
{
    private static string VPNCode;
    private static string age;
    private static ParticipantSex sex;
    private static int continueFlag;
    private const int VPN_FLAG = 1 << 0;
    private const int AGE_FLAG = 1 << 1;
    private const int RUNS_FLAG = 1 << 2;

    enum ParticipantSex
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
}
