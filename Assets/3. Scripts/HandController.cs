using UnityEngine;
using System.Collections.Generic;

public class HandController : SixenseObjectController
{      		
    public Transform    handModel;

    public static string TAG_GRABBABLE = "Grabbable", TAG_Hand = "Hand";
    private List<Collider> collidings;

    private void Reset()
    {
        Sensitivity = new Vector3(0.004f, 0.004f, 0.004f);//0.002f
    }

    protected override void Start()
    {
        base.Start();
        collidings = new List<Collider>();
    }

    //if realHand isColliding make handModel out of its child
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(TAG_GRABBABLE) || collider.gameObject.CompareTag(TAG_Hand))
            return;

        collidings.Add(collider);
        handModel.SetParent(null);
    }

    //if realHand !isColliding make handModel snap to realHand and become child
    private void OnTriggerExit(Collider collider)
    {
        if (!collidings.Contains(collider))
            return;

        collidings.Remove(collider);

        if (collidings.Count == 0)
        {
            handModel.SetParent(transform);
            handModel.localPosition = new Vector3(0, 0.02f, -0.1f);
            handModel.localRotation = Quaternion.identity;
        }
    }
    
}//class