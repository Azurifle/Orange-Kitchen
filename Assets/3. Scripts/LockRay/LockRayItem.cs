using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LockRayItem : MonoBehaviour
{
    public Material targetedColor, selectedColor;

    private MeshRenderer meshRenderer;
    private Material originalColor;
    private float _countDown = 0;

    private const int NO_COUNTDOWN = 0, RAYED = 1, SELECTED = 2;
    private int _mode = NO_COUNTDOWN;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material;
    }

    private void Update()
    {
        switch (_mode)
        {
            case RAYED: _countDown -= Time.deltaTime;
                if (_countDown <= 0)
                {
                    meshRenderer.material = originalColor;
                    _mode = NO_COUNTDOWN;
                }
                break;
            case SELECTED: _countDown -= Time.deltaTime;
                if (_countDown <= 0)
                {
                    meshRenderer.material = targetedColor;
                    _mode = NO_COUNTDOWN;
                }
                break;
        }
    }

    internal void Rayed()
    {
        if (_mode == NO_COUNTDOWN)
        {
            meshRenderer.material = targetedColor;
            _mode = RAYED;
        }
        _countDown = 0.1f;
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
        _countDown = 0.1f;
    }
}//class