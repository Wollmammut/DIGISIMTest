using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterial : Parameter<string>
{
    private static Dictionary<string, SphereMaterial> MATERIAL_TO_NAME = new Dictionary<string, SphereMaterial>();
    public static SphereMaterial NONE;
    public static SphereMaterial STYROFOAM;
    public static SphereMaterial WOOD;
    public static SphereMaterial LEAD;

    public enum EnumSphereMaterial
    {
        NONE,
        STYROFOAM,
        WOOD,
        LEAD
    }

    public SphereMaterial(string name, string value) : base(name, value)
    {

    }

    public static SphereMaterial getSphereMaterialFromEnum(SphereMaterial.EnumSphereMaterial e)
    {
        switch (e)
        {
            case EnumSphereMaterial.NONE: return NONE;
            case EnumSphereMaterial.STYROFOAM: return STYROFOAM;
            case EnumSphereMaterial.WOOD: return WOOD;
            case EnumSphereMaterial.LEAD: return LEAD;
            default:
            return NONE;
        }
    }

    public static SphereMaterial getSphereMaterialParameterFromName(string name)
    {
        SphereMaterial s;
        if (!MATERIAL_TO_NAME.TryGetValue(name, out s))
        {
            // TODO error
            return null;
        }
        else
        {
            return s;
        }
    }

    private static SphereMaterial addSphereMaterialParameter(string name)
    {
        SphereMaterial s = new SphereMaterial(name, name);
        MATERIAL_TO_NAME.Add(name,s);
        return s;
    }

    static SphereMaterial()
    {
        NONE = addSphereMaterialParameter("None");
        STYROFOAM = addSphereMaterialParameter("polystyrene");
        WOOD = addSphereMaterialParameter("wood");
        LEAD = addSphereMaterialParameter("lead");
    }
}
