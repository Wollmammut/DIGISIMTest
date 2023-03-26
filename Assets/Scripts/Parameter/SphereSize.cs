using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSize : Parameter<int>
{
    private static Dictionary<string, SphereSize> SIZE_TO_NAME = new Dictionary<string, SphereSize>();
    public static SphereSize SMALL;
    public static SphereSize MEDIUM;
    public static SphereSize BIG;

    private string adjective;

    public enum EnumSphereSize
    {
        SMALL,
        MEDIUM,
        BIG
    }

    public SphereSize(string name, string adjective, int value) : base(name, value)
    {
        this.adjective = adjective;
    }

    public static SphereSize getSphereSizeFromEnum(SphereSize.EnumSphereSize e)
    {
        switch (e)
        {
            case EnumSphereSize.SMALL: return SMALL;
            case EnumSphereSize.MEDIUM: return MEDIUM;
            case EnumSphereSize.BIG: return BIG;
            default:
            return SMALL;
        }
    }

    public static SphereSize getSphereSizeParameterFromName(string name)
    {
        SphereSize s;
        if (!SIZE_TO_NAME.TryGetValue(name, out s))
        {
            // TODO error
            return null;
        }
        else
        {
            return s;
        }
    }

    private static SphereSize addSphereSizeParameter(string name, string adjective, int diameter)
    {
        SphereSize s = new SphereSize(name, adjective, diameter);
        SIZE_TO_NAME.Add(name,s);
        return s;
    }

    public string getAdjective()
    {
        return adjective;
    }
    
    static SphereSize()
    {
        SMALL = addSphereSizeParameter("small", "kleine", 40);
        MEDIUM = addSphereSizeParameter("medium", "mittlere", 80);
        BIG = addSphereSizeParameter("large", "große", 120);
    }
}
