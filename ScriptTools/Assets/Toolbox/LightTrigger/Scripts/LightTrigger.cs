using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour {

    private Component[] cameraLights;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentsInChildren<Light>() == null)
        {
            //do nothing
        }
        else
        {
            cameraLights = other.GetComponentsInChildren<Light>();

            foreach (Light cameraLight in cameraLights)
            {
                cameraLight.enabled = true;
            }
        }            
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentsInChildren<Light>() == null)
        {
            //do nothing
        }
        else
        {
            cameraLights = other.GetComponentsInChildren<Light>();

            foreach (Light cameraLight in cameraLights)
            {
                cameraLight.enabled = false;
            }
        }
    }

}
