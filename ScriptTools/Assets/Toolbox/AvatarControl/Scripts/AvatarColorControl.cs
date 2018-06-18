using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarColorControl : MonoBehaviour {

    public Color newColor = new Color32(64, 64, 64, 160);
    private Renderer[] rends;

	void Start ()
    {        
        rends = GetComponentsInChildren<Renderer>();        
    }
		
	void Update ()
    {   
        foreach (Renderer rend in rends)
        {
            // Only affects Opaque materials
            if (rend.material.GetFloat("_Mode") == 0)
            {
                rend.material.color = newColor;
            }            
        }        
    }
}
