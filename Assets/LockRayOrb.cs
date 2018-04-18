using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRayOrb : MonoBehaviour {

    public float orbSpeed = 5f;

    private float _startOrbZ;

    private void Start()
    {
        _startOrbZ = transform.localPosition.z;
    }

    internal void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, _startOrbZ);//***
    }
}
