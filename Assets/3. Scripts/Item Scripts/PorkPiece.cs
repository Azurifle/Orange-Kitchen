using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorkPiece : MonoBehaviour {//not working yet

    public bool isFree = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (isFree)
            return;
        
        Knife knife = collision.collider.GetComponent<Knife>();
        if (knife == null)
            return;

        //gameObject.tag = HandController.TAG_GRABBABLE;
        transform.parent = null;
        collision.rigidbody.isKinematic = false;
        isFree = true;
    }
}
