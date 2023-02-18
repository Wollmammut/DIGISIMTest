using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        string matName = "";
        if (meshRenderer != null)
        {
            if (meshRenderer.material != null)
            {
                matName = meshRenderer.material.name.Replace(" (Instance)",""); 
            }
            else
            {
                // TODO error
            }

            if (Simulation.useFancyGraphics())
            {
                matName = "Materials/Fancy/" + matName;
            }
            else
            {
                matName = "Materials/" + matName;
            }
            UnityEngine.Material newMaterial = Resources.Load<UnityEngine.Material>(matName);
            meshRenderer.material = newMaterial;
        }
        else
        {
            // TODO error
        }
    }
}

