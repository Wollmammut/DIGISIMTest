using System;
using UnityEngine;

[Serializable]
public class LoggedClick
{
    [SerializeField]
    string time;
    [SerializeField]
    string clickedObjectName;

    public LoggedClick(TimeSpan time, string clickedObjectName)
    {
        this.time = time.ToString();
        this.clickedObjectName = clickedObjectName;
    } 
}