using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour {

    public GameObject Door;
    public GameObject SpwnMark;
    public GameObject WhiteBoard;
    public GameObject[] Table;
    public GameObject[] SeatMark;

    public bool RealOne;

    private Animator doorAnim;
    private Animator customerAnim;

    private SpawnScript spwn;
    private WhiteboardScript wBoard;

    private Vector3 targetPos;

    private bool atSeat = false;
    private bool order = false;
    private bool isleaving = false;
    private bool ready = false;
    private bool timeCount = false;
    private bool timeOut = false;
    private bool waitToLeave = false;

    private int target;

    private float timeLeft = 120.0f;

    // Use this for initialization
    void Start () {
        doorAnim = Door.GetComponent<Animator>();
        spwn = SpwnMark.GetComponent<SpawnScript>();
        wBoard = WhiteBoard.GetComponent<WhiteboardScript>();
        customerAnim = this.GetComponent<Animator>();

        target = SpawnScript.chairNo;
    }
	
	// Update is called once per frame
	void Update () {
        if (!RealOne)
        {
            if (Vector3.Distance(transform.position, SeatMark[target].transform.position) <= 4.0f) atSeat = true;
            if (Vector3.Distance(transform.position, spwn.Target[target].transform.position) <= 4.0f) ready = true;
            if (timeCount) timeLeft -= Time.deltaTime;
            if (timeLeft < 0 && !wBoard.notes[target].GetComponent<FoodCheckerScript>().isDeliver) timeOut = true;

            if (!atSeat && !ready)
            {
                targetPos = new Vector3(SeatMark[target].transform.position.x, 0, SeatMark[target].transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 10 * Time.deltaTime);
                transform.LookAt(SeatMark[target].transform.position);
                transform.Rotate(0, transform.rotation.y, 0);
                customerAnim.SetBool("IsWalk", true);
            }
            else if (atSeat && Vector3.Distance(transform.position, spwn.Target[target].transform.position) >= 0.5f & !ready)
            {
                targetPos = new Vector3(spwn.Target[target].transform.position.x, 0, spwn.Target[target].transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, 10 * Time.deltaTime);
                transform.LookAt(new Vector3(Table[target].transform.position.x , 0, Table[target].transform.position.z));
                transform.Rotate(0, transform.rotation.y, 0);
                customerAnim.SetBool("IsWalk", true);
            }

            if (ready)
            {
                if (!order)
                {
                    order = true;
                    timeCount = true;
                    customerAnim.SetBool("IsWalk", false);

                    wBoard.TakeOrder(target);
                }

                if (wBoard.notes[target].GetComponent<FoodCheckerScript>().isDeliver || timeOut)
                {
                    if (!isleaving)
                    {
                        if (!waitToLeave)
                            StartCoroutine(WaitToLeave());
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, SeatMark[target].transform.position,
                                10 * Time.deltaTime);
                            transform.LookAt(SeatMark[target].transform.position);
                            customerAnim.SetBool("IsWalk", true);
                        }

                        if (Vector3.Distance(transform.position, SeatMark[target].transform.position) <= 4.0f)
                        {
                            isleaving = true;

                            StartCoroutine(Leaving());
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, spwn.SpwnMark.transform.position, 
                            10 * Time.deltaTime);
                        transform.LookAt(spwn.SpwnMark.transform.position);
                        customerAnim.SetBool("IsWalk", true);
                    }
                }
                else
                {
                    targetPos = new Vector3(spwn.Target[target].transform.position.x, 0, spwn.Target[target].transform.position.z);
                    transform.position = targetPos;
                    transform.LookAt(new Vector3(Table[target].transform.position.x, 0, Table[target].transform.position.z));
                    transform.Rotate(0, transform.rotation.y, 0);
                    customerAnim.SetBool("IsWalk", false);
                }
            }
        }
    }

    IEnumerator WaitToLeave ()
    {
        yield return new WaitForSeconds(2.0f);

        waitToLeave = true;
    }

    IEnumerator Leaving()
    {
        yield return new WaitForSeconds(1.0f);

        spwn.Door.GetComponent<Animator>().SetBool("IsOpen", true);

        yield return new WaitForSeconds(2.0f);

        spwn.Door.GetComponent<Animator>().SetBool("IsOpen", false);

        Destroy(gameObject, 1);
    }
}
