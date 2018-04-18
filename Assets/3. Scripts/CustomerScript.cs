using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour {

    public GameObject Door;
    public GameObject SpwnMark;
    public GameObject[] SeatMark;

    public bool RealOne;

    private Animator doorAnim;

    private SpawnScript spwn;

    private bool atSeat = false;

    private int target;

    // Use this for initialization
    void Start () {
        doorAnim = Door.GetComponent<Animator>();
        spwn = SpwnMark.GetComponent<SpawnScript>();
        target = SpawnScript.chairNo;
    }
	
	// Update is called once per frame
	void Update () {
        if (!RealOne)
        {
            if (Vector3.Distance(transform.position, SeatMark[target].transform.position) <= 4.0f) atSeat = true;

            if (!atSeat)
            {
                transform.position = Vector3.MoveTowards(transform.position, SeatMark[target].transform.position, 10 * Time.deltaTime);
            }
            else if (atSeat && Vector3.Distance(transform.position, spwn.Target[target].transform.position) >= 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, spwn.Target[target].transform.position, 10 * Time.deltaTime);
            }
        }
    }
}
