using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != HandController.LAYER_ITEM)
            return;

        other.GetComponent<Item>().Stick(transform);
    }
}
