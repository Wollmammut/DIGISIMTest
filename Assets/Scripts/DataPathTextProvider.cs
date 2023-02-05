using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataPathTextProvider : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Application.persistentDataPath;
    }
}
