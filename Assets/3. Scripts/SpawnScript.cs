using System.Collections;
using UnityEngine;

public class SpawnScript : MonoBehaviour {
    public static int chairNo = -1;
    public static int SpawnCnt = 0;

    public static bool[] free = new bool[4];

    public GameObject SpwnMark;
    public GameObject Customer;
    public GameObject Boss;
    public GameObject Door;

    public Transform[] Target;

    private int _boss_time = 7;

    private Animator doorAnim;

	// Use this for initialization
	void Start () {
        doorAnim = Door.GetComponent<Animator>();

        for (int i = 3; i >= 0; i--)
            free[i] = true;

        StartCoroutine(AutoSpawn());
	}

    public void CreateCustomer ()
    {
        bool chairFree = false;
        bool bossFree = false;

        for (int i = 0; i < 4; i++)
        {
            if (free[i] && SpawnCnt < _boss_time)
            {
                chairNo = i;
                free[i] = false;
                chairFree = true;
                SpawnCnt++;

                break;
            }
            else if (free[i] && SpawnCnt >= _boss_time)
            {
                chairNo = i;
                free[i] = false;
                bossFree = true;
                SpawnCnt = 0;

                break;
            }
        }

        if (chairFree)
        {
            Vector3 point = SpwnMark.transform.position;
            GameObject newOne = Instantiate(Customer, point, Quaternion.identity);

            newOne.GetComponent<CustomerScript>().RealOne = false;

            StartCoroutine(DoorActivate());
        }
        else if (bossFree)
        {
            Vector3 point = SpwnMark.transform.position;
            GameObject newOne = Instantiate(Boss, point, Quaternion.identity);//Customer

            newOne.GetComponent<CustomerScript>().RealOne = false;
            _boss_time = Random.Range(1, 5);

            StartCoroutine(DoorActivate());
        }
    }

    IEnumerator DoorActivate()
    {
        doorAnim.SetBool("IsOpen", true);

        yield return new WaitForSeconds(2.0f);

        doorAnim.SetBool("IsOpen", false);
    }

    IEnumerator AutoSpawn ()
    {
        float f;

        while (true)
        {
            f = Random.Range(2, 11);

            yield return new WaitForSeconds(f);

            CreateCustomer();
        }
    }
}
