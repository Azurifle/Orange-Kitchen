using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public static int chairNo = -1;

    public static bool[] free = new bool[4];

    public GameObject SpwnMark;
    public GameObject Customer;
    public GameObject Door;

    public Transform[] Target;

    private Animator doorAnim;

	// Use this for initialization
	void Start () {
        doorAnim = Door.GetComponent<Animator>();

        for (int i = 3; i >= 0; i--)
            free[i] = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateCustomer ()
    {
        for (int i = 0; i < 4; i++)
        {
            if (free[i])
            {
                chairNo = i;
                free[i] = false;
                
                break;
            }
        }
         Vector3 point = SpwnMark.transform.position;

         point.y += 5;

         GameObject newOne = Instantiate(Customer, point, Quaternion.identity);

         newOne.GetComponent<CustomerScript>().RealOne = false;

         StartCoroutine(DoorActivate());
    }

    IEnumerator DoorActivate()
    {
        doorAnim.SetBool("IsOpen", true);

        yield return new WaitForSeconds(2.0f);

        doorAnim.SetBool("IsOpen", false);
    }
}
