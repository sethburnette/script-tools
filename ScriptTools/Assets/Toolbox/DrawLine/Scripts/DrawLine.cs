using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {

    public Transform lineStart;
    public Transform lineEnd;
    public float lineWidth = 0.1f;
    public Material customMaterial;

    private LineRenderer lr;

    private void Start()
    {
        if (lineStart && lineEnd != null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
            lr.useWorldSpace = true;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            if (customMaterial == null)
            {
                lr.material = new Material(Shader.Find("UI/Unlit/Text"));
            }
            else
            {
                lr.material = customMaterial;
            }
        }        
    }

    void Update()
    {        
        if (lineStart && lineEnd != null)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = lineStart.position;
            positions[1] = lineEnd.position;
            lr.positionCount = positions.Length;
            lr.SetPositions(positions);
        }       
    }

}
