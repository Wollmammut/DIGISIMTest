using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstructionTask : MonoBehaviour
{
    public GameObject constructionTaskSymbol;

    public enum EnumConstructionTask
    {
        SMALLER,
        EQUAL,
        BIGGER
    }

    public void setTaskSymbol(EnumConstructionTask task)
    {
        TextMeshProUGUI text = constructionTaskSymbol.GetComponent<TextMeshProUGUI>();// TODO null check
        switch(task)
        {
            case EnumConstructionTask.SMALLER:
            text.text = "<";
            break;
            case EnumConstructionTask.EQUAL:
            text.text = "=";
            break;
            case EnumConstructionTask.BIGGER:
            text.text = ">";
            break;
        }
    }
}
