using UnityEngine;

public class Chef_ObjectController : SixenseObjectController
{      		
    public Transform    handModel;
    
    private const string grabbableTag = "Grabbable", handTag = "Hand";

    public void Reset()
    {
        Sensitivity = new Vector3(0.004f, 0.004f, 0.004f);//0.002f
    }

    //if realHand isColliding make handModel out of its child
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(grabbableTag) || collider.gameObject.CompareTag(handTag))
            return;
        
        handModel.SetParent(null);
    }

    //if realHand !isColliding make handModel snap to realHand and become child
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(grabbableTag) || collider.gameObject.CompareTag(handTag))
            return;
        
        handModel.SetParent(transform);
        handModel.localPosition = new Vector3(0, 0.02f, -0.1f);
        handModel.localRotation = Quaternion.identity;
    }
    
}//class