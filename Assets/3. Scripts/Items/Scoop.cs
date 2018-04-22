using UnityEngine;

public class Scoop : Item
{
    public GameObject soup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4 && tag == null)//water
        {
            tag = "Soup";
            soup.SetActive(true);
        }
    }

    internal void Pour()
    {
        tag = null;
        soup.SetActive(false);
    }
}
