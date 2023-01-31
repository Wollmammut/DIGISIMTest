using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataEntrySceneSwitcher : MonoBehaviour
{
    public GameObject errorPopup;
    public string sceneToSwitchTo;

    public void tryToProceed()
    {
        if (ParticipantDataSaver.isAllDataSet())
        {
            SceneManager.LoadScene(sceneToSwitchTo); // TODO error of scene does not exist
        }
        else
        {
            errorPopup.SetActive(true);
            return;
        }
    }
}
