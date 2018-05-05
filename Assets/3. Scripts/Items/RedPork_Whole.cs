using UnityEngine;

public class RedPork_Whole : Item
{
    public Transform prefab_Middle;
    public Transform prefab_Head;

    private void OnCollisionStay(Collision other)
    {
        if (isSpawner || !other.gameObject.CompareTag("Knife"))
            return;

        CreatePiece(prefab_Middle, 0, 90);
        CreatePiece(prefab_Middle, 0.1936035f, 90);
        CreatePiece(prefab_Middle, -0.1936035f, 90);
        CreatePiece(prefab_Middle, -0.3872375f, 90);
        CreatePiece(prefab_Middle, -0.387207f, 90);
        CreatePiece(prefab_Head, 0.4517517f, 90);
        CreatePiece(prefab_Head, -0.4517517f, -90);

        Destroy(gameObject);
    }

    private void CreatePiece(Transform prefab, float posZ, float rotateY)
    {
        Transform newItem = Instantiate(prefab, transform);
        newItem.localPosition = new Vector3(0, 0, posZ);
        newItem.localEulerAngles = new Vector3(0, rotateY, 0);
        newItem.SetParent(null);
    }
}
