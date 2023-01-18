using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataEntrySceneSwitcher : MonoBehaviour
{
    public GameObject errorPopup;
    public string sceneToSwitchTo;

    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void tryToProceed()
    {
        if (PlayerPrefs.GetString("vpn") == "0" || PlayerPrefs.GetString("vpn") == "")
        {
            errorPopup.SetActive(true);
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneToSwitchTo);
        }
    }
}
