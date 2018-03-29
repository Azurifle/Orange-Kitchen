using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float sensitivity = 9F//15
        , maxLeft = -45F, maxRight = 225F
        , maxUp = -80F, maxDown = 80F
        
        , rotateHorizon, rotateVertical;

    private void Start()
    {
        rotateHorizon = transform.localEulerAngles.y;
        rotateVertical = transform.localEulerAngles.x;
    }

    private void Update()
    {
        rotateHorizon += sensitivity * Input.GetAxis("Mouse X");
        rotateHorizon = Mathf.Clamp(rotateHorizon, maxLeft, maxRight);

        rotateVertical -= sensitivity * Input.GetAxis("Mouse Y");
        rotateVertical = Mathf.Clamp(rotateVertical, maxUp, maxDown);

        transform.localEulerAngles = new Vector3(rotateVertical, rotateHorizon, 0);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(new Vector3(0, 0, 0.3f) );
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(new Vector3(0, 0, -0.3f));

        if (Input.GetKey(KeyCode.A))
            transform.Translate(new Vector3(-0.3f, 0, 0));
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(new Vector3(0.3f, 0, 0));
    }
}