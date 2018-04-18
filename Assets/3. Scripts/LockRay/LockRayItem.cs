using System.Collections.Generic;
using UnityEngine;

public class LockRayItem : MonoBehaviour
{
    public Material selectedColor, targetedColor;

    private MeshRenderer meshRenderer;
    private Material originalColor;
    private float _countDown = 0;

    private const int NO_COUNTDOWN = 0, RAYED = 1, SELECTED = 2;
    private int _mode = NO_COUNTDOWN;

    private List<LockRayItem> _lockRayItems;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material;
    }

    private void Update()
    {
        switch (_mode)
        {
            case RAYED:
                CountDownTo(originalColor);
                break;
            case SELECTED:
                CountDownTo(targetedColor);
                break;
        }
    }

    private void CountDownTo(Material color)
    {
        _countDown -= Time.deltaTime;
        if (_countDown <= 0)
        {
            meshRenderer.material = color;
            _mode = NO_COUNTDOWN;
        }
    }

    internal void Rayed(ref List<LockRayItem> lockRayItems)
    {
        _lockRayItems = lockRayItems;
        _lockRayItems.Add(this);//***
        if (_mode == NO_COUNTDOWN)
        {
            meshRenderer.material = targetedColor;
            _mode = RAYED;
        }
        _countDown = 0.1f;
    }

    internal void Selected()
    {
        if (_mode == NO_COUNTDOWN)
        {
            meshRenderer.material = selectedColor;
            _mode = SELECTED;
        }
        _countDown = 0.1f;
    }
}
