using UnityEngine;

public class HeaterScript : MonoBehaviour {

    public GameObject fire;
    public ChefHand leftHand;
    public ChefHand rightHand;

    private Color alpha;
    private bool isHeat = false;
    private bool isOn = false;

	// Use this for initialization
	void Start () {
        alpha = fire.GetComponent<MeshRenderer>().materials[0].color;
        alpha.a = 0;
        fire.GetComponent<MeshRenderer>().materials[0].color = alpha;
	}
	
	// Update is called once per frame
	void Update () {
		if (isOn)
        {
            if (alpha.a < 1)
                alpha.a += 0.01f;
            else
                isHeat = true;
        }
        else
        {
            if (alpha.a > 0)
                alpha.a -= 0.01f;
            else
                isHeat = false;
        }

        fire.GetComponent<MeshRenderer>().materials[0].color = alpha;
    }

    private void OnMouseDown()
    {
        isOn = !isOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == HandController.LAYER_HAND
            && (leftHand.IsPointing() || rightHand.IsPointing()))
            isOn = !isOn;
    }
}
