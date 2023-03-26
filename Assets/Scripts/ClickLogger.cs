using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ClickLogger : MonoBehaviour
{
    public static Stopwatch timer = new Stopwatch();
    private static List<LoggedClick> loggedClicks = new List<LoggedClick>();
    public bool onlyLogFirstClick = true;
    private int amountOfClicksLogged = 0;

    public static void clear()
    {
        loggedClicks = new List<LoggedClick>();
    }

    public static void logTime(string timeStampName)
    {
        TimeSpan time = timer.Elapsed;
        LoggedClick click = new LoggedClick(time, timeStampName);
        loggedClicks.Add(click);
    }

    public void logClick()
    {
       if (Input.GetMouseButtonUp(0))
       {
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
            }
       }
    }

    public static List<LoggedClick> getLoggedClicks()
    {
        return loggedClicks;
    }
    
    public static void startLogger()
    {
        timer.Restart();
    }
}
