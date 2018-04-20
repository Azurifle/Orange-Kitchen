using System.Collections.Generic;
using UnityEngine;


public class LockRay : MonoBehaviour
{
    public ChefHand realHand;
    public Transform virtualHand;
    public LineRenderer ray;
    public LockRayOrb orb;

    /*__Mode________________________________________________________________________
    |                                                                               |
    | 0 = Normal after 3 release point                                              |
    | 1 = Raycasting after 0 hold point                                             |
    | 2 = Selecting (move ord) after 1 release point (if nothing then go to mode 0) |
    | 3 = grab object after 2 hold point                                            |
    |______________________________________________________________________________*/
    private int _mode = 0;
    private const int NORMAL = 0, RAYCASTING = 1, SELECTING = 2, GRABBING = 3;
    
    private LockRayItem _selected;

    private void Update()
    {
        switch (_mode)
        {
            case NORMAL: if (realHand.IsPointing())//could change to use listener instead
                {
                    ray.enabled = true;
                    _mode = RAYCASTING;
                }
                break;
            case RAYCASTING: if (!realHand.IsPointing())
                {
                    RaycastHit[] rayeds = Physics.RaycastAll(transform.position, transform.forward
                        , ray.GetPosition(1).z, LayerMask.GetMask("Item"));

                    if (rayeds.Length <= 0)
                    {
                        ray.enabled = false;
                        _mode = NORMAL;
                        break;
                    }
                    
                    foreach (RaycastHit rayed in rayeds)
                    {
                        rayed.transform.GetComponent<LockRayItem>().Locked();
                    }

                    orb.gameObject.SetActive(true);
                    transform.SetParent(null);
                    _mode = SELECTING;
                }
                break;
            case SELECTING: if (realHand.IsPointing())
                {
                    //_selected = orb.FindNearest();

                    _selected.transform.SetParent(transform);
                    transform.SetParent(virtualHand);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    orb.gameObject.SetActive(false);
                    _mode = GRABBING;
                }
                break;
            case GRABBING: if (!realHand.IsPointing())
                {
                    _selected.transform.SetParent(null);
                    ray.enabled = false;
                    _mode = NORMAL;
                }
                break;
        }//switch mode
    }

    private void FixedUpdate()//moving hand
    {
        switch (_mode)
        {
            case RAYCASTING:
                RaycastHit[] rayeds = Physics.RaycastAll(transform.position, transform.forward
                    , ray.GetPosition(1).z, LayerMask.GetMask("Item"));
                if (rayeds.Length <= 0)
                {
                    break;
                }
                
                foreach (RaycastHit rayed in rayeds)
                {
                    rayed.transform.GetComponent<LockRayItem>().Rayed();
                }
                break;

            case SELECTING: orb.SetPosition(realHand.transform.localPosition.z, ray.GetPosition(1).z);
                //_selected = orb.FindNearest();
                _selected.Selected();
                break;
        }
    }

    private void SelectClosestTarget()
    {/*
        _index = 0;
        SwitchToTargetedColor(0);

        float minDistance = (_items[0].transform.position - orb.position).magnitude;
        for (int i = 1; i < _items.Length; ++i)
        {
            SwitchToTargetedColor(i);
            if ( (_items[i].transform.position - orb.position).magnitude < minDistance)
            {
                _index = i;
            }
        }
        
        _itemColors[_index].material = selectedColor;*/
        }
     /*
     private void SwitchToTargetedColor(int i)
     {
         _itemColors.Add(_items[i].transform.GetComponent<MeshRenderer>());
         _itemColors[i].material = targetedColor;
     }
     */
}//class