using UnityEngine;

public class BowlFinishing : MonoBehaviour {

    private int _noodleCount = 0;

    public int NoodleCount
    {
        get
        {
            return _noodleCount;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (NoodleCount >= 6)
            return;

        Rigidbody item = collider.GetComponent<Rigidbody>();
        if (item == null || item.isKinematic)
            return;

        Noodle noodle = item.GetComponent<Noodle>();
        if (noodle == null || !noodle.IsCooked())
            return;

        ++_noodleCount;
        item.isKinematic = true;
        collider.enabled = false;
        item.transform.parent = transform;

        switch (NoodleCount)
        {
            case 1:
                item.transform.localEulerAngles = new Vector3(0, -180, 90);
                item.transform.localPosition = new Vector3(-0.17f, 0.5f, 0.02f);
                break;
            case 2:
                item.transform.localEulerAngles = new Vector3(0, 0, 90);
                item.transform.localPosition = new Vector3(0.18f, 0.51f, -0.05f);
                break;
            case 3:
                item.transform.localEulerAngles = new Vector3(0, 0, -79);
                item.transform.localPosition = new Vector3(0.23f, 0.53f, 0.07f);
                break;
            case 4:
                item.transform.localEulerAngles = new Vector3(0, -90, -114);
                item.transform.localPosition = new Vector3(0.14f, 0.57f, -0.4f);
                break;
            case 5:
                item.transform.localEulerAngles = new Vector3(0, 0, 79);
                item.transform.localPosition = new Vector3(-0.24f, 0.5f, -0.13f);
                break;
            default:
                item.transform.localEulerAngles = new Vector3(0, -90, 114);
                item.transform.localPosition = new Vector3(-0.08f, 0.58f, 0.35f);
                break;
        }
    }//OnTriggerEnter
}
