using UnityEngine;

public class NormalRay : MonoBehaviour {
    public ChefHand realHand;
    public LineRenderer ray;

    private bool _isGrabbing = false;
    private LockRayItem _grabbed;

    private void Update()
    {
        switch (_isGrabbing)
        {
            case false:
                RaycastHit hit;
                if (realHand.IsPointing() &&
                    Physics.Raycast(transform.position, transform.forward
                                , out hit, ray.GetPosition(1).z, LayerMask.GetMask("Item")))
                {   
                    _grabbed = hit.transform.GetComponent<LockRayItem>();
                    _grabbed.Rayed();
                    _grabbed.transform.SetParent(transform);
                    _isGrabbing = true;
                }
                break;
            case true:
                if (!realHand.IsPointing())
                {
                    _grabbed.transform.SetParent(null);
                    _isGrabbing = false;
                }
                break;
        }//switch _isGrabbing
    }

    private void FixedUpdate()//moving hand
    {
        if (_isGrabbing)
            return;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward
                    , out hit, ray.GetPosition(1).z, LayerMask.GetMask("Item")))
            hit.transform.GetComponent<LockRayItem>().Rayed();
    }
}//class
