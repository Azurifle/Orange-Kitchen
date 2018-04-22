using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    public Vector3 grabbedPosition = Vector3.zero;
    public Vector3 grabbedRotation = Vector3.zero;
    public bool isSpawner = false;
    public Transform prefab;
    
    private int _countDown = 0;
    private bool _isHightlight = false;
    private new Rigidbody rigidbody;
    private Behaviour halo;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        halo = (Behaviour) GetComponent("Halo");
    }

    private void Update()
    {
        if (!_isHightlight)
            return;

        --_countDown;
        if (_countDown <= 0)
        {
            _isHightlight = false;
            halo.enabled = false;
        }
    }

    internal void Hightlight()
    {
        if (!_isHightlight)
        {
            halo.enabled = true;
            _isHightlight = true;
        }
        _countDown = 2;
    }

    internal void Grabbed(Transform grabCenterPoint)
    {
        Vector3 oldPosition = transform.position;
        Quaternion oldRotation = transform.rotation;

        rigidbody.isKinematic = true;
        transform.SetParent(grabCenterPoint);
        transform.localPosition = grabbedPosition;
        transform.localEulerAngles = grabbedRotation;

        if (isSpawner)
        {
            isSpawner = false;
            Instantiate(prefab, oldPosition, oldRotation);
        }
    }

    internal void Throwed(Vector3 vector3)
    {
        transform.SetParent(null);
        rigidbody.isKinematic = false;
        rigidbody.AddForce(vector3);
    }
}
