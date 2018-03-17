using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorScript : MonoBehaviour {

    public Animator anim;

    private bool onSwitch = false;
    
	void Update () {
        if ( Input.GetMouseButtonDown(0) && onSwitch)
        {
            if (anim.GetBool("On"))
                anim.SetBool("On", false);
            else
                anim.SetBool("On", true);
        }
    }

    private void OnMouseEnter()
    {
        onSwitch = true;
    }

    private void OnMouseExit()
    {
        onSwitch = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(HandController.TAG_Hand))
            anim.SetBool("On", !anim.GetBool("On"));
    }
}
