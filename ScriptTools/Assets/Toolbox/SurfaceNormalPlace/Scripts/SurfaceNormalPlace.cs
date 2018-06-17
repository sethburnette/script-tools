using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurfaceNormalPlace : MonoBehaviour {

    public GameObject[] objectsToPlace;
    public bool randomRotation = true;
    public float offsetFromSurface = 0.0f;

    private Transform surfaceTransform;
    private GameObject randomObject;   

    void Update()
    {
        if (objectsToPlace.Length == 0)
        {
            // do nothing
        }
        else
        {
            var arraySize = objectsToPlace.Length;
            randomObject = objectsToPlace[Random.Range(0, objectsToPlace.Length)];
        }                       

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                surfaceTransform = hit.transform;

                for (int i = 0; i < 1; i++)
                {
                    if (randomRotation == true)
                    {                            
                        GameObject objPlaced = Instantiate(randomObject, hit.point, Quaternion.LookRotation(hit.normal) * Quaternion.FromToRotation(Vector3.forward, -Vector3.up) * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));                            
                        objPlaced.transform.parent = surfaceTransform;

                        if (offsetFromSurface != 0)
                        {
                            objPlaced.transform.Translate(Vector3.up * offsetFromSurface);
                        }
                    }
                    else
                    {                            
                        GameObject objPlaced = Instantiate(randomObject, hit.point, Quaternion.LookRotation(hit.normal) * Quaternion.FromToRotation(Vector3.forward, -Vector3.up));                            
                        objPlaced.transform.parent = surfaceTransform;

                        if (offsetFromSurface != 0)
                        {
                            objPlaced.transform.Translate(Vector3.up * offsetFromSurface);
                        }                        
                    }                   
                }               
            }
        }            
    }
}
