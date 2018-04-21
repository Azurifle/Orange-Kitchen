using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardScript : MonoBehaviour {

    public GameObject[] notes;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeOrder (int i)
    {
        notes[i].GetComponent<FoodCheckerScript>().isDeliver = false;
        notes[i].GetComponent<FoodCheckerScript>().seatNo = i + 1;
        notes[i].GetComponent<FoodCheckerScript>().noodleReq = 2;
        notes[i].GetComponent<FoodCheckerScript>().porkReq = 5;
        notes[i].GetComponent<FoodCheckerScript>().soupReq = 1;
    }

    public void FinishOrder (int i)
    {
        notes[i].GetComponent<FoodCheckerScript>().isDeliver = true;
        SpawnScript.free[i] = true;
    }
}
