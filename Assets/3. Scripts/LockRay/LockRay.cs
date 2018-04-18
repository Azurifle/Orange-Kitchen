﻿using System.Collections.Generic;
using UnityEngine;

public class LockRay : MonoBehaviour
{
    public ChefHand realHand;
    public Transform virtualHand;
    public LineRenderer ray;
    public LockRayOrb orb;
    
    private List<LockRayItem> _lockRayItems;
    private float _startHandZ, _startOrbZ, _lastHandZ;
    private int _index = 0, _mode = 0;
    private const int NORMAL = 0, RAYCASTING = 1, SELECTING = 2, GRABBING = 3;
    /*__Mode________________________________________________________________________
    |                                                                               |
    | 0 = Normal after 3 release point                                              |
    | 1 = Raycasting after 0 hold point                                             |
    | 2 = Selecting (move ord) after 1 release point (if nothing then go to mode 0) |
    | 3 = grab object after 2 hold point                                            |
    |______________________________________________________________________________*/

    private void Start()
    {
        _lockRayItems = new List<LockRayItem>();
    }

    private void Update()
    {
        switch (_mode)
        {
            case NORMAL: if (realHand.IsGrabbing())//could change to use listener instead
                {
                    ray.enabled = true;
                    _lockRayItems.Clear();
                    _mode = RAYCASTING;
                }
                break;
            case RAYCASTING: if (!realHand.IsGrabbing())
                {
                    if (_lockRayItems == null || _lockRayItems.Count <= 0)
                    {
                        ray.enabled = false;
                        _mode = NORMAL;
                        break;
                    }

                    SetupSelection();
                    _mode = SELECTING;
                }
                break;
            case SELECTING: if (realHand.IsGrabbing())
                {/*
                    foreach (MeshRenderer item in _itemColors)
                    {
                        item.material = originalColor;
                    }*/

                    _lockRayItems[_index].transform.SetParent(transform);
                    transform.SetParent(virtualHand);
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    orb.gameObject.SetActive(false);
                    _mode = GRABBING;
                }
                break;
            case GRABBING: if (!realHand.IsGrabbing())
                {
                    _lockRayItems[_index].transform.SetParent(null);
                    ray.enabled = false;
                    _mode = NORMAL;
                }
                break;
        }//switch mode
    }

    private void FixedUpdate()//moving hand ***
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
                
                foreach (RaycastHit hit in rayeds)//tell items that you have been rayed!
                {
                    hit.transform.GetComponent<LockRayItem>().Rayed(ref _lockRayItems);//***
                }
                break;

            case SELECTING: /*orb.localPosition = new Vector3(0, 0, Mathf.Clamp(_startOrbZ + (
                (realHand.transform.localPosition.z - _startHandZ) * orbSpeed), 0, ray.GetPosition(1).z));
                
                int oldIndex = _index;
                _index = 0;
                
                float minDistance = (_lockRayItems[0].transform.position - orb.position).magnitude;
                for (int i = 1; i < _lockRayItems.Count; ++i)
                {
                    if ((_lockRayItems[i].transform.position - orb.position).magnitude < minDistance)
                    {
                        _index = i;
                    }
                }

                if (oldIndex != _index)
                {
                    //_itemColors[oldIndex].material = targetedColor;
                    //_itemColors[_index].material = selectedColor;
                }*/
                break;
        }
    }

    private void SetupSelection()
    {
        _startHandZ = realHand.transform.localPosition.z;
        orb.gameObject.SetActive(true);
        orb.ResetPosition();
        transform.SetParent(null);
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