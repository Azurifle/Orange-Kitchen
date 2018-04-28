using UnityEngine;
using System.Collections.Generic;

public class BowlFinishing : MonoBehaviour {

    public GameObject soup;

    private int _noodleCount = 0, _redPorkCount = 0;
    private bool _hasSoup = false, _hasOrangeFishBall = false;
    
    public int NoodleCount
    {
        get
        {
            return _noodleCount;
        }
    }

    public int RedPorkCount
    {
        get
        {
            return _redPorkCount;
        }
    }

    public bool HasOrangeFishBall
    {
        get
        {
            return _hasOrangeFishBall;
        }
    }

    public bool HasSoup
    {
        get
        {
            return _hasSoup;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Noodle") && NoodleCount < 6)
        {
            Rigidbody item = other.GetComponent<Rigidbody>();
            if (item == null || item.isKinematic)
                return;

            Noodle noodle = item.GetComponent<Noodle>();
            if (noodle == null || !noodle.IsCooked())
                return;

            ++_noodleCount;
            other.tag = "Untagged";
            other.gameObject.layer = 0;
            item.isKinematic = true;
            other.enabled = false;
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
        }
        else if (other.CompareTag("RedPork") && _redPorkCount < 5)
        {
            Rigidbody item = other.GetComponent<Rigidbody>();
            if (item == null || item.isKinematic)
                return;

            ++_redPorkCount;
            other.tag = "Untagged";
            other.gameObject.layer = 0;
            item.isKinematic = true;
            other.enabled = false;
            item.transform.parent = transform;

            switch (_redPorkCount)
            {
                case 1:
                    item.transform.localEulerAngles = new Vector3(0, -180, 90);
                    item.transform.localPosition = new Vector3(-0.17f, 0.55f, 0.02f);
                    break;
                case 2:
                    item.transform.localEulerAngles = new Vector3(0, 0, 90);
                    item.transform.localPosition = new Vector3(0.18f, 0.56f, -0.05f);
                    break;
                case 3:
                    item.transform.localEulerAngles = new Vector3(0, 0, -79);
                    item.transform.localPosition = new Vector3(0.23f, 0.58f, 0.07f);
                    break;
                case 4:
                    item.transform.localEulerAngles = new Vector3(0, -90, -114);
                    item.transform.localPosition = new Vector3(0.14f, 0.62f, -0.4f);
                    break;
                default:
                    item.transform.localEulerAngles = new Vector3(0, -90, 114);
                    item.transform.localPosition = new Vector3(-0.08f, 0.63f, 0.35f);
                    break;
            }
        }
        else if (other.CompareTag("OrangeFishBall") && !_hasOrangeFishBall)
        {
            Rigidbody item = other.GetComponent<Rigidbody>();
            if (item == null || item.isKinematic)
                return;

            _hasOrangeFishBall = true;
            other.tag = "Untagged";
            other.gameObject.layer = 0;
            item.isKinematic = true;
            other.enabled = false;
            item.transform.parent = transform;
            item.transform.localEulerAngles = new Vector3(90, 0, 0);
            item.transform.localPosition = new Vector3(0f, 0.68f, 0f);
        }
        else if (other.CompareTag("Soup") && !_hasSoup)
        {
            other.GetComponent<Scoop>().Pour();

            _hasSoup = true;
            soup.SetActive(true);
        }

    }//OnTriggerEnter
}
