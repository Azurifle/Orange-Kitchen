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

    public void TakeOrder ()
    {
        if (notes[0].GetComponent<FoodCheckerScript>().isDeliver)
        {
            notes[0].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[0].GetComponent<FoodCheckerScript>().seatNo = 1;
            notes[0].GetComponent<FoodCheckerScript>().noodleReq = 2;
            notes[0].GetComponent<FoodCheckerScript>().porkReq = 5;
            notes[0].GetComponent<FoodCheckerScript>().soupReq = 1;
        }
        else if (notes[1].GetComponent<FoodCheckerScript>().isDeliver)
        {
            notes[1].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[1].GetComponent<FoodCheckerScript>().seatNo = 2;
        }
        else if (notes[2].GetComponent<FoodCheckerScript>().isDeliver)
        {
            notes[2].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[2].GetComponent<FoodCheckerScript>().seatNo = 3;
        }
        else if (notes[3].GetComponent<FoodCheckerScript>().isDeliver)
        {
            notes[3].GetComponent<FoodCheckerScript>().isDeliver = false;
            notes[3].GetComponent<FoodCheckerScript>().seatNo = 4;
        }
    }
}
