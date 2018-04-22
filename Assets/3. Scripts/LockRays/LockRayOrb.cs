using System.Collections.Generic;
using UnityEngine;

public class LockRayOrb : MonoBehaviour {

    public float orbSpeed = 40f;
    
    private float _startHandZ, _startOrbZ;
    private List<LockRayItem> _lockRayItems;

    private void Start()
    {
        _startOrbZ = transform.localPosition.z;
    }
    
    internal void Setup(float startHandZ, List<LockRayItem> lockRayItems)
    {
        _startHandZ = startHandZ;
        _lockRayItems = lockRayItems;
    }

    internal void SetPosition(float handZ, float maxZ)
    {
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(_startOrbZ + ( 
            (handZ - _startHandZ) * orbSpeed), 0, maxZ));
    }
    
    internal LockRayItem FindNearest()
    {
        LockRayItem nearest = null;
        float minDistance = Mathf.Infinity;
        foreach(LockRayItem item in _lockRayItems)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearest = item;
                minDistance = distance;
            }
        }
        return nearest;
    }
}//class
