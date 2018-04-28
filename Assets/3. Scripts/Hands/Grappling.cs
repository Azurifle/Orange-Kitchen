using UnityEngine;
using System.Collections.Generic;

public class Grappling : MonoBehaviour {

    /* Razer Hydra Tutorial____________________________________
    |
    | https://www.slideshare.net/cjros/basic-vr-development-tutorial-integrating-oculus-rift-and-razer-hydra
    |__________________________________________________________*/

    public ChefHand hand;
    public float throwForce = 60.0f;//3.0f

    private const int NOT_GRAB = 0, GRAB_BEFORE = 1, GRABBING = 2;
    private int _mode = NOT_GRAB;
    
    private Vector3 _handVelocity, _handPrevious;
    private List<Item> _inRangeItems;
    private Item _nearestItem = null;

    private void Start()
    {
        _inRangeItems = new List<Item>();
    }

    private void Update()
    {
        switch (_mode)
        {
            case NOT_GRAB:
                _nearestItem = FindNearest();
                if (_nearestItem)
                {
                    _nearestItem.Hightlight();

                    if (hand.IsGrabbing())
                    {
                        _nearestItem.Grabbed(transform);
                        _mode = GRABBING;
                    }
                }
                else if (hand.IsGrabbing())
                    _mode = GRAB_BEFORE;
                break;
            case GRAB_BEFORE:
                if (!hand.IsGrabbing())
                    _mode = NOT_GRAB;
                break;
            case GRABBING:
                if (!hand.IsGrabbing())
                {
                    _nearestItem.Throwed(_handVelocity * throwForce);
                    _mode = NOT_GRAB;
                }
                break;
        }
    }

    private Item FindNearest()
    {
        Item nearest = null;
        float minDistance = Mathf.Infinity;
        foreach (Item item in _inRangeItems)
        {
            if (!item || item.gameObject.layer != HandController.LAYER_ITEM)
            {
                _inRangeItems.Remove(item);
                return null;
            }
                
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearest = item;
                minDistance = distance;
            }
        }
        return nearest;
    }

    private void FixedUpdate()//find velocity
    {
        if (_mode == GRABBING)
        {
            _handVelocity = (transform.position - _handPrevious) / Time.fixedDeltaTime;
            _handPrevious = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == HandController.LAYER_ITEM)
            _inRangeItems.Add(other.GetComponent<Item>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == HandController.LAYER_ITEM)
            _inRangeItems.Remove(other.GetComponent<Item>());
    }
    
}//class