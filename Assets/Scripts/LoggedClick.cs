using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class LoggedClick
{
    // [SerializeField]
    [JsonProperty]
    string time;
    // [SerializeField]
    [JsonProperty]
    string clickedObjectName;

    public LoggedClick(TimeSpan time, string clickedObjectName)
    {
        this.time = time.ToString();
        this.clickedObjectName = clickedObjectName;
    } 
}