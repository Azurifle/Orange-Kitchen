using System.Collections.Generic;
using UnityEngine;

public class LockedRay : MonoBehaviour
{
    public ChefHand realHand;
    public LineRenderer ray;
    public Transform virtualHand;
    public Material selectedMat;
    public Transform orb;
    public float changeSelectDist = 0.1f;

    private Rigidbody _grabedRigidbody = null;
    private int _mode = 0;
    private const int NORMAL = 0, RAYCASTING = 1, SELECTING = 2, GRABBING = 3;
    private List<Transform> _hits;
    private Material normalMat;
    private float handVelocity, handPrevious;
    private int index = 0;
    private MeshRenderer itemColor;
    /*__Mode________________________________________________________________________
    |                                                                               |
    | 0 = Normal after 3 release point                                              |
    | 1 = Raycasting after 0 hold point                                             |
    | 2 = Selecting (move ord) after 1 release point (if nothing then go to mode 0) |
    | 3 = grab object after 2 hold point                                            |
    |______________________________________________________________________________*/

    private void Start()
    {
        _hits = new List<Transform>();
    }

    private void Update()
    {
        switch (_mode)
        {
            case NORMAL: if (realHand.IsGrabbing())
                {
                    ray.enabled = true;
                    _mode = RAYCASTING;
                }
                return;
            case RAYCASTING: if (realHand.IsGrabbing())
                    return;

                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 30);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag(HandController.TAG_GRABBABLE))
                        _hits.Add(hit.transform);
                }

                if (_hits.Count <= 0)
                {
                    _mode = NORMAL;
                    return;
                }

                SwitchColorToSelected(0);
                transform.SetParent(null);
                _mode = SELECTING;
                return;
            case SELECTING: if (realHand.IsGrabbing())
                {
                    /*
                    transform.SetParent(virtualHand);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    _mode = GRABBING;*/
                }
                else if(index < _hits.Count-1 && handVelocity > changeSelectDist)//forward
                {
                    itemColor.material = normalMat;
                    ++index;
                    Debug.Log("index: "+index+"/"+ _hits.Count);
                    SwitchColorToSelected(index);
                }
                else if (index > 0 && handVelocity < changeSelectDist)
                {
                    itemColor.material = normalMat;
                    --index;
                    Debug.Log("index: " + index + "/" + _hits.Count);
                    SwitchColorToSelected(index);
                }
                return;
        }
    }

    private void FixedUpdate()//find velocity
    {
        if (_mode != SELECTING || Time.fixedDeltaTime <= 0)
            return;
        
        handVelocity = (realHand.transform.position.z - handPrevious) / Time.fixedDeltaTime;
        orb.position = new Vector3(orb.position.x, orb.position.y
            , orb.position.z + handVelocity);
        handPrevious = realHand.transform.position.z;
        Debug.Log("velo "+ handVelocity);
    }

    private void SwitchColorToSelected(int index)
    {
        itemColor = _hits[index].GetComponent<MeshRenderer>();
        normalMat = itemColor.material;
        itemColor.material = selectedMat;
    }
}