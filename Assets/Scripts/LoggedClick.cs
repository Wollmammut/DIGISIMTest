using System;

public class LoggedClick
{
    TimeSpan time;
    string clickedObjectName;

    public LoggedClick(TimeSpan time, string clickedObjectName)
    {
        this.time = time;
        this.clickedObjectName = clickedObjectName;
    } 
}