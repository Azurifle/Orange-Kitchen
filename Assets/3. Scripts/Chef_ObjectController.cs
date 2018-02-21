using UnityEngine;

public class Chef_ObjectController : MonoBehaviour {

	public SixenseHands			Hand;
	public Vector3				Sensitivity = new Vector3(0.004f, 0.004f, 0.004f);//0.002f changed by Dandy

    protected bool				m_enabled = false;
	protected Quaternion		m_initialRotation;
	protected Vector3			m_initialPosition;
	protected Vector3			m_baseControllerPosition;
    
    protected virtual void Start()
    {
        m_initialRotation = transform.localRotation;
        m_initialPosition = transform.localPosition;
    }
    
    protected virtual void Update ()//"protected virtual" added by Dandy
    {
		if ( Hand == SixenseHands.UNKNOWN )
			return;
		
		SixenseInput.Controller controller = SixenseInput.GetController( Hand );
		if ( controller != null && controller.Enabled )  
			UpdateObject(controller);
	}
		
	void OnGUI()
	{
		if ( !m_enabled )
		{
			GUI.Box( new Rect( Screen.width / 2 - 100, Screen.height - 40, 200, 30 ),  "Press Start To Move/Rotate" );
		}
	}
		
	protected virtual void UpdateObject(  SixenseInput.Controller controller )
	{
		if ( controller.GetButtonDown( SixenseButtons.START ) )
		{
			// enable position and orientation control
			m_enabled = !m_enabled;
			
			// delta controller position is relative to this point
			m_baseControllerPosition = new Vector3( controller.Position.x * Sensitivity.x,
													controller.Position.y * Sensitivity.y,
													controller.Position.z * Sensitivity.z );
			
			// this is the new start position
			m_initialPosition = transform.localPosition;
        }
		
		if ( m_enabled)
        {
			UpdatePosition( controller );
			UpdateRotation( controller );
		}
	}
	
	protected void UpdatePosition( SixenseInput.Controller controller )
	{
		Vector3 controllerPosition = new Vector3( controller.Position.x * Sensitivity.x,
												  controller.Position.y * Sensitivity.y,
												  controller.Position.z * Sensitivity.z );
		
		// distance controller has moved since enabling positional control
		Vector3 vDeltaControllerPos = controllerPosition - m_baseControllerPosition;
		
		// update the localposition of the object
		transform.localPosition = m_initialPosition + vDeltaControllerPos;
	}
	
	protected void UpdateRotation( SixenseInput.Controller controller )
	{
		transform.localRotation = controller.Rotation * m_initialRotation;
	}

    /*Dandy add __________________________________________*/

    public Transform handModel;
    
    private const string grabbableTag = "Grabbable", handTag = "Hand";

    //if realHand isColliding make handModel out of its child
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(grabbableTag) || collider.gameObject.CompareTag(handTag))
            return;
        
        handModel.SetParent(null);
    }

    //if realHand !isColliding make handModel snap to realHand and become child
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(grabbableTag) || collider.gameObject.CompareTag(handTag))
            return;
        
        handModel.SetParent(transform);
        handModel.localPosition = new Vector3(0, 0.02f, -0.1f);
        handModel.localRotation = Quaternion.identity;
    }
    
}//class