using UnityEngine;

public class RazerLook_n_Fly : MonoBehaviour {

    public ChefHand leftHand, rightHand;
    //public float sensitivity = 9F//15

    private float _rotateHorizon, _rotateVertical;

    private void Start()
    {
        _rotateHorizon = transform.localEulerAngles.y;
        _rotateVertical = transform.localEulerAngles.x;
    }
    
    private void FixedUpdate()
    {
        //razer right stick for look
        _rotateHorizon += rightHand.GetJoyStickX();
        _rotateVertical -= rightHand.GetJoyStickY();
        transform.localEulerAngles = new Vector3(_rotateVertical, _rotateHorizon, 0);

        //razer left stick for fly
        transform.Translate(new Vector3(leftHand.GetJoyStickX(), 0, 0));
        transform.Translate(new Vector3(0, 0, leftHand.GetJoyStickY()) );
    }
}