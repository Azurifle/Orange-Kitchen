using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorScript : MonoBehaviour {

    public Animator anim;

    private bool onSwitch = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && onSwitch)
            anim.SetTrigger("Push");
	}

    private void OnMouseEnter()
    {
        onSwitch = true;
    }

    private void OnMouseExit()
    {
        onSwitch = false;
    }
}
