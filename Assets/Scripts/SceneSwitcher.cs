using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToSwitchTo;

    public void NextScene()
    {
        SceneManager.LoadScene(sceneToSwitchTo);// TODO errof if scenetowtichto == null
    }
}
