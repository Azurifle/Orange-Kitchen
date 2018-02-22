using UnityEngine;

public class ChefHand : Chef_ObjectController
{
    public SixenseInput.Controller  m_controller = null;
    public Animator                 m_animator;

	float m_fLastTriggerVal;
    
    void Update()//override SixenseObjectController Update
    {
		if ( m_controller == null )
		{
			m_controller = SixenseInput.GetController( Hand );
		}
		else if ( m_animator != null )
		{
            //UpdateHandAnimation();
            UpdateObject(m_controller);//was added by Dandy
        }
	}

    private void FixedUpdate()
    {
        if (m_controller != null && m_controller.Enabled)
            UpdateActionInput(m_controller); //Action update
    }
    
    protected void UpdateHandAnimation()
	{
        if (m_controller.GetButton(SixenseButtons.TRIGGER) )// Fist or Point
        {
            m_animator.SetBool("Idle", false);
            m_animator.SetBool("GripBall", false);
            m_animator.SetBool("HoldBook", false);

            if (m_controller.GetButton(SixenseButtons.BUMPER) )
            {
                m_animator.SetBool("Point", false);
                m_animator.SetBool("Fist", true);

                float fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, m_controller.Trigger, 0.1f);
                m_animator.SetFloat("FistAmount", fTriggerVal);
                m_fLastTriggerVal = fTriggerVal;

                return;
            }// Fist

            m_animator.SetBool("Fist", false);
            m_animator.SetBool("Point", true);

            return;
        }

        m_animator.SetBool("Fist", false);
        m_animator.SetBool("Point", false);

        switch (Hand)//GripBall or HoldBook
        {
            case SixenseHands.RIGHT: if (m_controller.GetButton(SixenseButtons.TWO))
                {
                    m_animator.SetBool("Idle", false);
                    m_animator.SetBool("HoldBook", false);
                    m_animator.SetBool("GripBall", true);
                }
                else if (m_controller.GetButton(SixenseButtons.THREE))
                {
                    m_animator.SetBool("Idle", false);
                    m_animator.SetBool("GripBall", false);
                    m_animator.SetBool("HoldBook", true);
                }
                break;
            default:
                if (m_controller.GetButton(SixenseButtons.ONE) )
                {
                    m_animator.SetBool("Idle", false);
                    m_animator.SetBool("HoldBook", false);
                    m_animator.SetBool("GripBall", true);
                }
                else if (m_controller.GetButton(SixenseButtons.FOUR) )
                {
                    m_animator.SetBool("Idle", false);
                    m_animator.SetBool("GripBall", false);
                    m_animator.SetBool("HoldBook", true);
                }
                break;
        }
        m_animator.SetBool("Idle", true);
    }

    private void SetAnimation(string state)
    {
        switch (state)
        {
            //case "Idle": m_animator.SetBool("Idle", true);
        }
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
    
    private bool        isHoldingObject = false;
    private GameObject  closestObject = null;
    private Rigidbody   closestRigidbody = null;
    //private Grappling   closestGrappling = null;
    // Script attached to grabbed object with grappling data on that object

    private Vector3 handVector, handPrevious;

    protected override void UpdateObject(SixenseInput.Controller controller)
    {
        if (controller.Enabled)
        {
            //UpdateAnimationInput(controller); //Animation update
            UpdateHandAnimation();
            //Action update move to fixedUpdate by Dandy
        }

        base.UpdateObject(controller);
    }

    protected void UpdateActionInput(SixenseInput.Controller controller)
    {
        if (isHoldingObject)
        {
            FindVelocity();

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
    protected void FindVelocity()
    {
        if (Time.fixedDeltaTime != 0)//Time.deltaTime
        {
            handVector = (transform.position - handPrevious) / Time.fixedDeltaTime;//Time.deltaTime
            handPrevious = transform.position;
        }

        //handVelocity = Vector3.Magnitude(handVector);
    }

    //Throw the held object once player lets go based on hand velocity 
    protected void Throw()
    {
        if (closestRigidbody)
        {
            //Vector3 dir = (closestObject.transform.position - transform.position).normalized;
            closestRigidbody.AddForce(/*dir * handVelocity*/ handVector * throwForce);
        }
    }

}//class

