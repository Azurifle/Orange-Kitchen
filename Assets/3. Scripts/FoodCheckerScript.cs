using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCheckerScript : MonoBehaviour
{
    [HideInInspector] public bool isDeliver = true;

    [HideInInspector] public int seatNo;

    [HideInInspector] public int noodleReq;
    [HideInInspector] public int porkReq;
    [HideInInspector] public int soupReq;
    [HideInInspector] public int wontonReq;
    [HideInInspector] public int veggyReq;

    [HideInInspector] public int noodleCnt = 0;
    [HideInInspector] public int porkCnt = 0;
    [HideInInspector] public int soupCnt = 0;
    [HideInInspector] public int wontonCnt = 0;
    [HideInInspector] public int veggyCnt = 0;

    [HideInInspector] public GameObject bowl;

    public GameObject text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDeliver)
        {
            text.GetComponent<TextMesh>().text = "";
        }
        else
        {
            string t;

            if (soupReq == 1) t = "Yes";
            else t = "No";

            text.GetComponent<TextMesh>().text = "Table: " + seatNo.ToString() + 
                                                "\nNoodle: " + noodleReq.ToString() + 
                                                "\nPork: " + porkReq.ToString() +
                                                "\nSoup: " + t;
        }
    }
}

