using System;
using UnityEngine;

public class LockedRayHand : ChefHand
{
    public Transform lockSphere;

    private RaycastHit[] _hits;
    private bool _isLocking = false;

    private new void Update()//override ChefHand Update
    {
        base.Update();
        if (!_isLocking && IsGrabbing())
        {
            _hits = Physics.RaycastAll(transform.position, transform.forward, 30);
            _isLocking = true;
            handModel.SetParent(null);
            foreach (RaycastHit hit in _hits)
                if (hit.collider.CompareTag(TAG_GRABBABLE))
                {

                }
        }
        else if(_isLocking && !IsGrabbing())
        {
            _isLocking = false;
            handModel.SetParent(transform);
            handModel.localPosition = new Vector3(0, 0.02f, -0.1f);
            handModel.localRotation = Quaternion.identity;
        }
        //1 not lock ray
        //2 
    }
}