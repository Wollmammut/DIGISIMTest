using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ParticipantDataSaver : MonoBehaviour
{
    public string dataName;

    public void Start()
    {
        TMP_InputField input = gameObject.GetComponent<TMP_InputField>();
        input.onEndEdit.AddListener(saveDataToPlayerPrefs);
    }
    public void saveDataToPlayerPrefs(string data)
    {
        PlayerPrefs.SetString(dataName, data);
        print(data);
    }
}
