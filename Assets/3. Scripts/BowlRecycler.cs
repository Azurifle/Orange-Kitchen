using UnityEngine;

public class BowlRecycler : MonoBehaviour {
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != HandController.LAYER_ITEM)
            return;

        if (!other.GetComponent<Rigidbody>().isKinematic)//not being grab
            Destroy(other.gameObject, 1f);
    }
}
