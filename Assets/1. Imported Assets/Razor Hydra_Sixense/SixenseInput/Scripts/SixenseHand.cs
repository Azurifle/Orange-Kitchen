
//
// Copyright (C) 2013 Sixense Entertainment Inc.
// All Rights Reserved
//

using UnityEngine;

public class SixenseHand : SixenseObjectController
{
	//public SixenseHands Hand;//m_hand
    public SixenseInput.Controller m_controller = null;

	Animator 	m_animator;
	float 		m_fLastTriggerVal;
	//Vector3		m_initialPosition;
	//Quaternion 	m_initialRotation;
    
    protected override void Start() 
	{
		// get the Animator
		m_animator = gameObject.GetComponent<Animator>();
		m_initialRotation = transform.localRotation;
		m_initialPosition = transform.localPosition;
        
        grabPoint = transform.GetChild(0);
        /*currentPosition = GameObject.Find("RightHandCollider").transform.position;
        currentRotation = GameObject.Find("RightHandCollider").transform.rotation;*/
    }

    protected override void Update()//"override" was added by Dandy
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

    // Updates the animated object from controller input.
    protected void UpdateHandAnimation()
	{
        // three fingers
        float fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, m_controller.Trigger, 0.1f);
        m_fLastTriggerVal = fTriggerVal;

        // Point
        if ( (!m_controller.GetButton(SixenseButtons.BUMPER) && fTriggerVal > 0.01f) 
            || Hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.ONE) : m_controller.GetButton(SixenseButtons.TWO) )
			m_animator.SetBool( "Point", true );
		else
			m_animator.SetBool( "Point", false );
		
		// Grip Ball
		if ( Hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.FOUR) : m_controller.GetButton(SixenseButtons.THREE)  )
			m_animator.SetBool( "GripBall", true );
		else
			m_animator.SetBool( "GripBall", false );
				
		// Hold Book
		if ( Hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.THREE) : m_controller.GetButton(SixenseButtons.FOUR) )
			m_animator.SetBool( "HoldBook", true );
		else
			m_animator.SetBool( "HoldBook", false );

        // Fist
        if (m_controller.GetButton(SixenseButtons.BUMPER) && fTriggerVal > 0.01f )
			m_animator.SetBool( "Fist", true );
		else
			m_animator.SetBool( "Fist", false );
		
		m_animator.SetFloat("FistAmount", fTriggerVal);
		
		// Idle
		if ( m_animator.GetBool("Fist") == false &&  
			 m_animator.GetBool("HoldBook") == false && 
			 m_animator.GetBool("GripBall") == false && 
			 m_animator.GetBool("Point") == false )
		{
			m_animator.SetBool("Idle", true);
		}
		else
		{
			m_animator.SetBool("Idle", false);
		}
	}

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

    public float minGrabDistance = 0.002f, throwForce = 3.0f;
    // Grabbable object must be within this distance from hand colliders to be picked up 
    // Force multiplyer for throwing objects
    public Transform itemsParent;

    private bool isHoldingObject = false;
    private Transform grabPoint;
    private GameObject closestObject = null;
    private Rigidbody closestRigidbody = null;
    private Grappling closestGrappling = null;
    // Script attached to grabbed object with grappling data on that object 
    //private float handVelocity = 0.0f;
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
                closestObject.transform.SetParent(itemsParent);
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
                , nearest = Vector3.Distance(grabbables[0].transform.position, grabPoint.position);
            if (nearest < minGrabDistance)
                closestObject = grabbables[0];

            for (int i = 1; i < grabbables.Length; ++i)
            {
                dist = Vector3.Distance(grabbables[i].transform.position, grabPoint.position);
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

            closestObject.transform.position = grabPoint.position;
            closestObject.transform.rotation = grabPoint.rotation;
            closestObject.transform.SetParent(grabPoint);

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

