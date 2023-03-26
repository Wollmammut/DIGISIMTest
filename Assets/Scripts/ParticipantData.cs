using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class ParticipantData
{
    public string VPNCode;
    public string age;
    public string sex;
    public System.Object additionalData;

    public List<LoggedClicksWrapper> loggedClicksByRun = new List<LoggedClicksWrapper>();

    [Serializable]
    public struct LoggedClicksWrapper
    {
        public int runNumber;
        public List<LoggedClick> loggedClicks;
        public System.Object additionalRunData;
    }

    public void addLoggedClicksForRunNumber(List<LoggedClick> loggedClicks, int runNumber, System.Object additionalRunData)
    {
        if (loggedClicks == null || loggedClicks.Count == 0)
        {
            return;
        }
        LoggedClicksWrapper wrapper = new LoggedClicksWrapper();
        wrapper.runNumber = runNumber;
        wrapper.loggedClicks = loggedClicks;
        wrapper.additionalRunData = additionalRunData;
        loggedClicksByRun.Add(wrapper);
    }
}
