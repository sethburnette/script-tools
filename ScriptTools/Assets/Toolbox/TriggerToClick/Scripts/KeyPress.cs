using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPress : MonoBehaviour {

    public GameObject display;
    private Text displayText;
    private string buttonName;

    void Start()
    {
        buttonName = gameObject.name;
        if (display.GetComponent<Text>() != null)
        {
            displayText = display.GetComponent<Text>();
        }
    }

	public void SayMyName ()
    {
        if (display != null)
        {
            displayText.text = buttonName;
        }        
	}
}
