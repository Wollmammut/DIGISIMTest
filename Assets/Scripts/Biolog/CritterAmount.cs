using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterAmount : Parameter<string>
{
    public static CritterAmount FEW = new CritterAmount("wenige");
    public static CritterAmount MANY = new CritterAmount("viele");

    public enum EnumCritterAmount
    {
        FEW,
        MANY
    }

    public CritterAmount(string name) : base(name, name)
    {

    }

    public static CritterAmount getCritterAmountFromEnum(CritterAmount.EnumCritterAmount enumCritterAmount)
    {
        if(enumCritterAmount == CritterAmount.EnumCritterAmount.FEW)
        {
            return FEW;
        }
        else
        {
            return MANY;
        }
    }
}