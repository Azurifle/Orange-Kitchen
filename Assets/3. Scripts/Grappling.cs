using System;
using UnityEngine;

public class Grappling : MonoBehaviour {

    /* Razer Hydra Tutorial____________________________________
    |
    | https://www.slideshare.net/cjros/basic-vr-development-tutorial-integrating-oculus-rift-and-razer-hydra
    |__________________________________________________________*/

    public ChefHand hand;
    public float throwForce = 60.0f;//3.0f

    private bool isGrabBefore = false;
    private Rigidbody grabedRigidbody = null;
    private Vector3 handVelocity, handPrevious;
    
    private void Update()//Release Grab
    {
        if (hand.IsGrabbing() )
            return;

        isGrabBefore = false;

        if (grabedRigidbody)
        {
            grabedRigidbody.transform.SetParent(null);
            grabedRigidbody.isKinematic = false;
            grabedRigidbody.AddForce(handVelocity * throwForce);
            grabedRigidbody = null;
        }
    }

    private void FixedUpdate()//find velocity
    {
        if (!grabedRigidbody || Time.fixedDeltaTime <= 0)
            return;

        handVelocity = (transform.position - handPrevious) / Time.fixedDeltaTime;//Time.deltaTime
        handPrevious = transform.position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (hand.IsGrabbing() )
            isGrabBefore = true;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (isGrabBefore || grabedRigidbody ||
            !hand.IsGrabbing() ||
            !collider.gameObject.CompareTag(HandController.TAG_GRABBABLE))
            return;

        grabedRigidbody = collider.GetComponent<Rigidbody>();
        grabedRigidbody.isKinematic = true;
        grabedRigidbody.transform.SetParent(transform);

        /*
        GameObject[] grabbables = GameObject.FindGameObjectsWithTag("Grabbable");
        float dist
            , nearest = Vector3.Distance(grabbables[0].transform.position, grabCenterPoint.position);
        if (nearest < minGrabDistance)
            closestObject = grabbables[0];

        for (int i = 1; i < grabbables.Length; ++i)
        {
            dist = Vector3.Distance(grabbables[i].transform.position, grabCenterPoint.position);
            if (dist < minGrabDistance && dist < nearest)
            {
                closestObject = grabbables[i];
                nearest = dist;
            }
        }

        if (!closestObject)
            return;

        closestRigidbody = closestObject.GetComponent<Rigidbody>();
        if (closestRigidbody && closestRigidbody.isKinematic)
            return;

        closestRigidbody.isKinematic = true;

        closestObject.transform.SetParent(grabCenterPoint);
        //closestObject.transform.localPosition = Vector3.zero;
        //closestObject.transform.localRotation = Quaternion.identity;

        isHoldingObject = true;*/
    }
    
}