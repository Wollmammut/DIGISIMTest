using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ParticipantData
{
    public string VPNCode;
    public string age;
    public string sex;
    public List<LoggedClicksWrapper> loggedClicksByRun = new List<LoggedClicksWrapper>();
    public AdditionalApplicationData additionalData;
    
    [Serializable]
    public struct LoggedClicksWrapper
    {
        public int runNumber;
        public List<LoggedClick> loggedClicks;
    }

    public void addLoggedClicksForRunNumber(List<LoggedClick> loggedClicks, int runNumber)
    {
        LoggedClicksWrapper wrapper = new LoggedClicksWrapper();
        wrapper.runNumber = runNumber;
        wrapper.loggedClicks = loggedClicks;
        loggedClicksByRun.Add(wrapper);
    }
}
