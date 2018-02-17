using System;
using UnityEngine;

public class Grappling : MonoBehaviour {
    
    internal Vector3 GetPosition(SixenseHands hand)
    {
        throw new NotImplementedException();
    }

    internal Vector3 GetRotation(SixenseHands hand)
    {
        throw new NotImplementedException();
    }
}

/*not being use yet
    
    bool isGrabbedByLeft = false, isGrabbedByRight = false;

    private void OnCollisionStay(Collision collision)
    {
        if (isGrabbedByLeft || isGrabbedByRight)
            return;

        if (collision.gameObject.CompareTag("Hand"))
        {
            if (SixenseInput.Controllers[0].GetButton(SixenseButtons.TRIGGER))
            {
                Debug.Log("grab by left");
                isGrabbedByLeft = true;
                transform.SetParent(collision.transform);
            }
            else if (SixenseInput.Controllers[1].GetButton(SixenseButtons.TRIGGER))
            {
                Debug.Log("grab by right");
                isGrabbedByRight = true;
                transform.SetParent(collision.transform);
            }
        }
    }*/
