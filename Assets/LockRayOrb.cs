using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class LockRayOrb : MonoBehaviour {

    public float orbSpeed = 5f;
    public SphereCollider _scanner;
    
    private float _startHandZ, _startOrbZ, _lastHandZ;
    private LockRayItem _nearest;
    private bool _isScanning = false;

    private void Start()
    {
        _scanner = GetComponent<SphereCollider>();
        _startOrbZ = transform.localPosition.z;
    }

    private void FixedUpdate()
    {
        _scanner.radius += 0.1f;
    }

    internal void Setup(float startHandZ)
    {
        _startHandZ = startHandZ;
        transform.localPosition = new Vector3(0, 0, _startOrbZ);
        _isScanning = true;
    }

    internal void SetPosition(float handZ, float maxZ)
    {
        transform.localPosition = new Vector3(0, 0, Mathf.Clamp(_startOrbZ + ( 
            (handZ - _startHandZ) * orbSpeed), 0, maxZ));
        _scanner.radius = 0.5f;//******************************************************************
    }
    /*
    internal LockRayItem FindNearest()
    {
        _scanner.radius = 0.5f;
        _nearest = null;
        return null;
    }*/
}
