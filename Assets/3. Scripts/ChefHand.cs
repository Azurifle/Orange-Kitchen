using UnityEngine;

public class ChefHand : Chef_ObjectController
{
    public SixenseInput.Controller  m_controller = null;
    public Animator                 m_animator;

	private float m_fLastTriggerVal;
    private enum HandAnimation { Idle, Fist, Point, GripBall, HoldBook };

    //private methods
    private void Update()//override SixenseObjectController Update
    {
		if ( m_controller == null )
			m_controller = SixenseInput.GetController( Hand );
		else
            UpdateObject(m_controller);//changed by Dandy
	}

    private void FixedUpdate()
    {
        if (isHoldingObject)
            FindVelocity();
    }

    private void UpdateHandAnimation()
	{
        if (m_controller.GetButton(SixenseButtons.TRIGGER) )// Fist or Point
        {
            if (m_controller.GetButton(SixenseButtons.BUMPER) )
            {
                SetAnimation(HandAnimation.Fist);

                float fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, m_controller.Trigger, 0.1f);
                m_animator.SetFloat("FistAmount", fTriggerVal);
                m_fLastTriggerVal = fTriggerVal;

                return;
            }// Fist

            SetAnimation(HandAnimation.Point);
            return;
        }

        switch (Hand)//GripBall or HoldBook or Idle
        {
            case SixenseHands.RIGHT: if (m_controller.GetButton(SixenseButtons.TWO))
                    SetAnimation(HandAnimation.GripBall);
                else if (m_controller.GetButton(SixenseButtons.THREE))
                    SetAnimation(HandAnimation.HoldBook);
                else
                    SetAnimation(HandAnimation.Idle);
                break;
            default: if (m_controller.GetButton(SixenseButtons.ONE) )
                    SetAnimation(HandAnimation.GripBall);
                else if (m_controller.GetButton(SixenseButtons.FOUR) )
                    SetAnimation(HandAnimation.HoldBook);
                else
                    SetAnimation(HandAnimation.Idle);
                break;
        }
    }

    private void SetAnimation(HandAnimation selected)
    {
        m_animator.SetBool(selected.ToString(), true);

        foreach (HandAnimation state in System.Enum.GetValues(typeof(HandAnimation) ) )
            if(state != selected)
                m_animator.SetBool(state.ToString(), false);
    }

    //getters
    public Quaternion InitialRotation
	{
		get { return m_initialRotation; }
	}	
	public Vector3 InitialPosition
	{
		get { return m_initialPosition; }
	}

    /* Razer Hydra Tutorial____________________________________
    |
    | https://www.slideshare.net/cjros/basic-vr-development-tutorial-integrating-oculus-rift-and-razer-hydra
    |__________________________________________________________*/

    public Transform grabCenterPoint;
    public float minGrabDistance = 0.25f, throwForce = 60.0f;//3.0f
    // Grabbable object must be within this distance from hand colliders to be picked up 
    // Force multiplyer for throwing objects
    
    private bool        isHoldingObject = false, isGrabBefore = false;
    private GameObject  closestObject = null;
    private Rigidbody   closestRigidbody = null;
    //private Grappling   closestGrappling = null;
    // Script attached to grabbed object with grappling data on that object

    private Vector3 handVector, handPrevious;

    protected override void UpdateObject(SixenseInput.Controller controller)
    {
        if (controller.Enabled)//m_animator != null
        {
            UpdateHandAnimation();//UpdateAnimationInput(controller);

            if (!m_controller.GetButton(SixenseButtons.TRIGGER))
                isGrabBefore = false;

            UpdateActionInput(m_controller); //Action update
        }

        base.UpdateObject(controller);//press start & move hand
    }

    private void UpdateActionInput(SixenseInput.Controller controller)//***
    {
        if (isHoldingObject)
        {
            if (!controller.GetButton(SixenseButtons.TRIGGER))
            {
                closestObject.transform.SetParent(null);
                closestRigidbody.isKinematic = false;
                Throw();
                isHoldingObject = false;
                closestObject = null;
                return;
            }
        }
        else if(controller.GetButton(SixenseButtons.TRIGGER) )
        {
            GameObject[] grabbables = GameObject.FindGameObjectsWithTag("Grabbable");
            float dist
                , nearest = Vector3.Distance(grabbables[0].transform.position, grabCenterPoint.position);
            if (nearest < minGrabDistance)
                closestObject = grabbables[0];

            for (int i = 1; i < grabbables.Length; ++i)
            {
                dist = Vector3.Distance(grabbables[i].transform.position, grabCenterPoint.position);
                if (dist < minGrabDistance && dist < nearest)
                {
                    closestObject = grabbables[i];
                    nearest = dist;
                }
            }

            if (!closestObject)
                return;

            closestRigidbody = closestObject.GetComponent<Rigidbody>();
            if (closestRigidbody && closestRigidbody.isKinematic)
                return;

            closestRigidbody.isKinematic = true;
                        
            closestObject.transform.SetParent(grabCenterPoint);
            /*closestObject.transform.localPosition = Vector3.zero;
            closestObject.transform.localRotation = Quaternion.identity;*/

            isHoldingObject = true;
        }
    }

    //Calculate velocity of hand 
    private void FindVelocity()
    {
        if (Time.fixedDeltaTime != 0)//Time.deltaTime
        {
            handVector = (transform.position - handPrevious) / Time.fixedDeltaTime;//Time.deltaTime
            handPrevious = transform.position;
        }

        //handVelocity = Vector3.Magnitude(handVector);
    }

    //Throw the held object once player lets go based on hand velocity 
    private void Throw()
    {
        if (closestRigidbody)
        {
            //Vector3 dir = (closestObject.transform.position - transform.position).normalized;
            closestRigidbody.AddForce(/*dir * handVelocity*/ handVector * throwForce);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(m_controller.GetButton(SixenseButtons.TRIGGER))
            isGrabBefore = true;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (isHoldingObject || isGrabBefore || 
            !m_controller.GetButton(SixenseButtons.TRIGGER) || 
            !collision.gameObject.CompareTag(grabbableTag))
            return;

        //grab
    }//***grab

}//class