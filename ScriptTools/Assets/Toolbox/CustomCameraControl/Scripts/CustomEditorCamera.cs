
using UnityEngine;
using System.Collections;


[AddComponentMenu("Camera-Control/3D Software Camera Style")]
public class CustomEditorCamera : MonoBehaviour {

    // This script allows for camera control similar to a 3D design application, with the added function of centering the view on selected objects
    // Note that it currently requires the extra step of manually creating a custom Tag called "Selected"

    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;
    public float smoothTime = 0.5f;
    public float maxSpeed = 50.0f;
    public bool partHasBeenClicked = false;

    
    private string originalTag;
    private GameObject centerPoint;
    private GameObject oldCamTarget;
    private Transform selectedTransform;
    private Vector3 velocity = Vector3.zero;
    private GameObject[] selections;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    void Start()
    {
        Init();
        centerPoint = new GameObject("CenterOfSelected");
        centerPoint.transform.position = new Vector3(0, 0, 0);
    }

    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        oldCamTarget = GameObject.Find("Cam Target");
        Destroy(oldCamTarget);

        // If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        // Grab the current rotations as starting points
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopCoroutine("MoveToSelected");
            StopCoroutine("MoveToCenter");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                selectedTransform = hit.transform;
                originalTag = selectedTransform.gameObject.tag;
                selectedTransform.gameObject.tag = "Selected";
                StartCoroutine("MoveToCenter");
                partHasBeenClicked = true;
            }

            else
            {
                oldCamTarget = GameObject.Find("Cam Target");
                Destroy(oldCamTarget);                

                selections = GameObject.FindGameObjectsWithTag("Selected");
                foreach (GameObject selection in selections)
                {
                    if (selection != null)
                    {
                        selection.gameObject.tag = originalTag;                        
                    }
                }

                partHasBeenClicked = false;
            }
        }

        if (!target)
        {
            Init();
        }
        else
        {
            // If Control and Alt and Middle button: zoom
            if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl))
            {
                desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate * 0.125f * Mathf.Abs(desiredDistance);
            }

            // Right mouse = orbit
            else if (Input.GetMouseButton(1))
            {
                xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                ////////OrbitAngle

                // Clamp the vertical axis for the orbit
                yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
                // Set camera rotation 
                desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
                currentRotation = transform.rotation;

                rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                transform.rotation = rotation;
            }

            // Middle mouse button = drag view
            else if (Input.GetMouseButton(2))
            {

                // Prevent the "sticky" connection to selected objects.
                StopCoroutine("MoveToSelected");
                StopCoroutine("MoveToCenter");

                // Grab the rotation of the camera so we can move in a psuedo local XY space
                target.rotation = transform.rotation;
                target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
                target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
            }

            ////////Orbit Position

            // Affect the desired Zoom distance if we roll the scrollwheel
            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
            // Clamp the zoom min/max
            desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
            // For smoothing of the zoom, lerp distance
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

            // Calculate position based on the new currentDistance 
            position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
            transform.position = position;
        }
    }

    private IEnumerator MoveToSelected()
    {

        while (target != selectedTransform)
        {
            target.position = Vector3.SmoothDamp(target.position, selectedTransform.position, ref velocity, smoothTime, maxSpeed);

            yield return null;
        }

        StopCoroutine("MoveToSelected");
    }

    private IEnumerator MoveToCenter()
    {

        selections = GameObject.FindGameObjectsWithTag("Selected");

        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < selections.Length; i++)
        {
            if (!selections[i].gameObject.activeSelf)
                continue;

            averagePos += selections[i].transform.position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        // Debug.Log("The center of the selected objects is " + averagePos);

        centerPoint.transform.position = averagePos;

        while (target != centerPoint.transform)
        {
            target.position = Vector3.SmoothDamp(target.position, centerPoint.transform.position, ref velocity, smoothTime, maxSpeed);

            yield return null;
        }

        StopCoroutine("MoveToCenter");
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

