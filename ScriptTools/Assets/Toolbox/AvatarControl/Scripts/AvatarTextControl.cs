using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvatarTextControl : MonoBehaviour
{
    protected Camera mainCamera;
    public string customText;
    public bool maintainTextSize = false;
    public float textScale = 1.0f;
    private Text playerName;    
    private Vector3 initialScale;

    void Start()
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            playerName = gameObject.GetComponent<Text>();
        }
        else
        {
            Debug.Log("This script needs to be attached to a UI object with text.");
        }

        initialScale = transform.localScale;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }        
    }

    void Update()
    {
        // Replaces the text with whatever string is specified for customText (8-12 characters seems like a good limit)        
        playerName.text = customText;

        if (Camera.current != null && Camera.current.enabled == true)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

            // If active, this keeps the text the same relative size despite a change in distance from the viewer
            if (maintainTextSize == true)
            {
                Plane plane = new Plane(mainCamera.transform.forward, mainCamera.transform.position);
                float dist = plane.GetDistanceToPoint(transform.position);
                transform.localScale = initialScale * dist * textScale;
            }
        }
        // This else statement is necessary in order to keep the direction and scale shifts smooth
        else
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);

            if (maintainTextSize == true)
            {
                Plane plane = new Plane(mainCamera.transform.forward, mainCamera.transform.position);
                float dist = plane.GetDistanceToPoint(transform.position);
                transform.localScale = initialScale * dist * textScale;
            }
        }
    }
}
