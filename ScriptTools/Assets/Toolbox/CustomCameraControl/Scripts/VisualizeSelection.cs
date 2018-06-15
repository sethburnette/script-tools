using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeSelection : MonoBehaviour {

    public Material highlightMaterial;    
    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = gameObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (gameObject.tag == "Selected")
        {
            gameObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = originalMaterial;
        }
    }


}
