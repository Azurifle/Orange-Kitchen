using UnityEngine;

public class ChefHand : HandController
{
    public Animator                 m_animator;

	private float m_fLastTriggerVal;
    private enum HandAnimation { Idle, Fist, Point, GripBall, HoldBook };
    private SixenseInput.Controller _controller;

    /* Razer Hydra Tutorial____________________________________
    |
    | https://www.slideshare.net/cjros/basic-vr-development-tutorial-integrating-oculus-rift-and-razer-hydra
    |__________________________________________________________*/

    public bool IsPointing()
    {
        if(_controller == null)
            return false;
        return _controller.GetButton(SixenseButtons.TRIGGER);
    }

    public float GetJoyStickX()
    {
        if (_controller == null)
            return 0f;
        return _controller.JoystickX;
    }
    public float GetJoyStickY()
    {
        if (_controller == null)
            return 0f;
        return _controller.JoystickY;
    }

    protected void Update()//override SixenseObjectController Update
    {
        _controller = SixenseInput.GetController(Hand);
        if (_controller != null && _controller.Enabled)
            UpdateHandAnimation();
    }

    private void FixedUpdate()
    {
        if (_controller != null)
            UpdateObject(_controller);
    }

    private void UpdateHandAnimation()
	{
        if (_controller.GetButton(SixenseButtons.TRIGGER) )// Fist or Point
        {
            if (_controller.GetButton(SixenseButtons.BUMPER) )
            {
                SetAnimation(HandAnimation.Fist);

                float fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, _controller.Trigger, 0.1f);
                m_animator.SetFloat("FistAmount", fTriggerVal);
                m_fLastTriggerVal = fTriggerVal;

                return;
            }// Fist

            SetAnimation(HandAnimation.Point);
            return;
        }

        switch (Hand)//GripBall or HoldBook or Idle
        {
            case SixenseHands.RIGHT: if (_controller.GetButton(SixenseButtons.TWO))
                    SetAnimation(HandAnimation.GripBall);
                else if (_controller.GetButton(SixenseButtons.THREE))
                    SetAnimation(HandAnimation.HoldBook);
                else
                    SetAnimation(HandAnimation.Idle);
                break;
            default: if (_controller.GetButton(SixenseButtons.ONE) )
                    SetAnimation(HandAnimation.GripBall);
                else if (_controller.GetButton(SixenseButtons.FOUR) )
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
}//class