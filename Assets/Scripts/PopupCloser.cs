using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloser : MonoBehaviour
{
    public void closeSelf()
    {
        gameObject.SetActive(false);
    }
}
