using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpring : MonoBehaviour {

    public float zeta = 0.4f;
    public float omega = 1.0f;
    
	void Start ()
    {
		
	}
    
    void Spring (float x, float v, float xt, float zeta, float omega, float h)
    {
        float f = 1.0f + 2.0F * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1.0f / (f + hhoo);
        float detX = f * x + h * v + hhoo * xt;
        float detV = v + hoo * (xt - x);
        x = detX * detInv;
        v = detV * detInv;
    }
	
	public void BounceClick (float timeStep)
    {
        Spring(gameObject.transform.localScale.x, 10.0f, gameObject.transform.localScale.x - 10.0f, zeta, omega, timeStep);
    }
    

}
