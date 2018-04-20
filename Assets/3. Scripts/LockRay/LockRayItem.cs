using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LockRayItem : MonoBehaviour
{
    public Material targetedColor, selectedColor;
    
    private const int NO_COUNTDOWN = 0, RAYED = 1, SELECTED = 2, MAX_COUNTDOWN = 2;
    private int _mode = NO_COUNTDOWN;

    private MeshRenderer meshRenderer;
    private Material originalColor;
    private int _countDown = 0;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material;
    }

    private void FixedUpdate()
    {
        switch (_mode)
        {
            case RAYED: --_countDown;
                if (_countDown <= 0)
                {
                    ToOriginalColor();
                }
                break;
            case SELECTED: --_countDown;
                if (_countDown <= 0)
                {
                    _mode = NO_COUNTDOWN;
                    meshRenderer.material = targetedColor;
                }
                break;
        }
    }

    internal void ToOriginalColor()
    {
        _mode = NO_COUNTDOWN;
        meshRenderer.material = originalColor;
    }

    internal void Rayed()
    {
        if (_mode == NO_COUNTDOWN)
        {
            meshRenderer.material = targetedColor;
            _mode = RAYED;
        }
        _countDown = MAX_COUNTDOWN;
    }

    internal void Locked()
    {
        meshRenderer.material = targetedColor;
        _mode = NO_COUNTDOWN;
    }

    internal void Selected()
    {
        if (_mode == NO_COUNTDOWN)
        {
            meshRenderer.material = selectedColor;
            _mode = SELECTED;
        }
        _countDown = MAX_COUNTDOWN;
    }
}//class