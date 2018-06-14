using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerToClick : MonoBehaviour {

    // This script is specifically for activating On Click events in a World Space Unity UI button with a trigger volume attached. Useful for creating VR/AR user interfaces.         

    public Color32 pressedColor;        
    private Button button;
    private ColorBlock cb;
    private Color32 originalColor;

    void Start ()
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            Debug.Log("This script needs to be attached to a UI button.");
        }
        else
        {
            button = gameObject.GetComponent<Button>();
            cb = button.colors;
            originalColor = cb.normalColor;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            Debug.Log("This script needs to be attached to a UI button.");
        }
        else
        {
            if (!button.IsInteractable())
            {
                // do nothing
            }
            else
            {
                button.onClick.Invoke();
                cb.normalColor = pressedColor;
                button.colors = cb;
            }            
        }                 
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.GetComponent<Button>() == null)
        {
            Debug.Log("This script needs to be attached to a UI button.");
        }
        else
        {
            cb.normalColor = originalColor;
            button.colors = cb;
        }
    }

}
