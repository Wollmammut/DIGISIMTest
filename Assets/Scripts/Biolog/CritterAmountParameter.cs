using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterAmountParameter : MonoBehaviour, ParameterSetter<CritterAmount.EnumCritterAmount>
{
    public int effectMultiplier;

    protected CritterAmount.EnumCritterAmount critterAmount;

    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void setValue(CritterAmount.EnumCritterAmount value)
    {   
        critterAmount = value;
        gameObject.SetActive(true);
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Transform child = transform.GetChild(i); // TODO catch null error
            if (value == CritterAmount.EnumCritterAmount.FEW)
            {
                child.gameObject.SetActive(i % 2 == 0);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
        BiologSim.setOrganismAmountParameter(gameObject.name, this);
    }

    public int getEffectOnSoilQuality()
    {
        if (critterAmount == CritterAmount.EnumCritterAmount.MANY)
        {
            return effectMultiplier;
        }
        else
        {
            return 0;
        }
    }

    public CritterAmount.EnumCritterAmount getCurrentParameterValue() // TODO make ParameterSetter an abstract class and make this a method of it returnung T
    {
        return critterAmount;
    }
}
