using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorScript : MonoBehaviour {

    public Animator anim;
    public ChefHand leftHand;
    public ChefHand rightHand;

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
        if (other.gameObject.layer == HandController.LAYER_HAND 
            && (leftHand.IsPointing() || rightHand.IsPointing() ))
            anim.SetBool("On", !anim.GetBool("On"));
    }
}
