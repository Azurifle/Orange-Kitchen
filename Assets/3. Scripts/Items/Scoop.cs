using UnityEngine;

public class Scoop : Item
{
    public GameObject soup;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 4 && CompareTag("Untagged"))//water
        {
            tag = "Soup";
            soup.SetActive(true);
        }
    }

    internal void Pour()
    {
        tag = "Untagged";
        soup.SetActive(false);
    }
}
