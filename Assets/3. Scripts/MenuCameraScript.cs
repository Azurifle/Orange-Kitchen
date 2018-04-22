using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour {

    public GameObject[] CameraMark;
    public GameObject CameraLookAt;

    private int node = 0;

    private bool plus = true;

	// Use this for initialization
	void Start () {
        transform.position = CameraMark[0].transform.position;
        transform.LookAt(CameraLookAt.transform);
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, CameraMark[node].transform.position) <= 0.5f)
        {
            if (plus)
                node++;
            else
                node--;
        }

        if (node > 2)
        {
            node = 1;
            plus = false;
        }
        else if (node < 0)
        {
            node = 1;
            plus = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, CameraMark[node].transform.position, 3 * Time.deltaTime);
        transform.LookAt(CameraLookAt.transform);
    }
}
