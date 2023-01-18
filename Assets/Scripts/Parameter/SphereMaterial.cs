using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterial : Parameter<string>
{
    private static Dictionary<string, SphereMaterial> MATERIAL_TO_NAME = new Dictionary<string, SphereMaterial>();
    public static SphereMaterial STYROFOAM;
    public static SphereMaterial WOOD;
    public static SphereMaterial LEAD;
    public string localizedDisplayName;

    public enum EnumSphereMaterial
    {
        STYROFOAM,
        WOOD,
        LEAD
    }

    public SphereMaterial(string name, string value, string localizedDisplayName) : base(name, value)
    {
        this.localizedDisplayName = localizedDisplayName;
    }

    public static SphereMaterial getSphereMaterialFromEnum(SphereMaterial.EnumSphereMaterial e)
    {
        switch (e)
        {
            case EnumSphereMaterial.STYROFOAM: return STYROFOAM;
            case EnumSphereMaterial.WOOD: return WOOD;
            case EnumSphereMaterial.LEAD: return LEAD;
            default:
            return WOOD;
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

    private static SphereMaterial addSphereMaterialParameter(string name, string localizedDisplayName)
    {
        SphereMaterial s = new SphereMaterial(name, name, localizedDisplayName);
        MATERIAL_TO_NAME.Add(name,s);
        return s;
    }

    static SphereMaterial()
    {
        STYROFOAM = addSphereMaterialParameter("styrofoam", "Styropor");
        WOOD = addSphereMaterialParameter("wood", "Holz");
        LEAD = addSphereMaterialParameter("lead", "Blei");
    }
}
