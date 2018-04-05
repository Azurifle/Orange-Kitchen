using UnityEngine;
using System.Collections.Generic;

public class HandController : SixenseObjectController
{      		
    public Transform    handModel;

    public static int LAYER_ITEM = 8, LAYER_HAND = 9;
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
        if (collider.gameObject.layer == LAYER_ITEM)
            return;

        collidings.Add(collider);
        handModel.SetParent(null);
    }

    //if realHand !isColliding make handModel snap to realHand and become child
    private void OnTriggerExit(Collider collider)
    {
        collidings.Remove(collider);

        if (collidings.Count == 0)
        {
            handModel.SetParent(transform);
            handModel.localPosition = new Vector3(0, 0.02f, -0.1f);
            handModel.localRotation = Quaternion.identity;
        }
    }
    
}//class