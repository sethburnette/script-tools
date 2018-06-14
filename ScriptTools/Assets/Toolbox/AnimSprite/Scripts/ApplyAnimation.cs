using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAnimation : MonoBehaviour {

    public string animatorPath;
    public Animator buttonAnimator;    

    private Animator objectAnimator;
	
	void Start ()
    {
        objectAnimator = GetComponent<Animator>();
        objectAnimator.runtimeAnimatorController = buttonAnimator.runtimeAnimatorController;
	}	
	
	void Update ()
    {
		
	}
}
