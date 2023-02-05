using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ClickLogger : MonoBehaviour
{
    public static Stopwatch timer = new Stopwatch();
    public static List<LoggedClick> loggedClicks = new List<LoggedClick>();
    public bool onlyLogFirstClick = false;
    private int amountOfClicksLogged = 0;

    public static void logTime(string timeStampName)
    {
        TimeSpan time = timer.Elapsed;
        LoggedClick click = new LoggedClick(time, timeStampName);
        loggedClicks.Add(click);
    }

    public void logClick()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        if (onlyLogFirstClick && amountOfClicksLogged > 1)
        {
            return;
        }
        else
        {
            TimeSpan time = timer.Elapsed;
            string name = transform.name;
            LoggedClick click = new LoggedClick(time, name);
            loggedClicks.Add(click);
            ++amountOfClicksLogged;
            //UnityEngine.Debug.Log(transform.name + " " + time.ToString());
        }
    }

    static ClickLogger()
    {
        timer.Start();
    }
}
