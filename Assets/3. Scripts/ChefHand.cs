using UnityEngine;

public class ChefHand : HandController
{
    public Animator                 m_animator;

	private float m_fLastTriggerVal;
    private enum HandAnimation { Idle, Fist, Point, GripBall, HoldBook };
    private SixenseInput.Controller controller;

    /* Razer Hydra Tutorial____________________________________
    |
    | https://www.slideshare.net/cjros/basic-vr-development-tutorial-integrating-oculus-rift-and-razer-hydra
    |__________________________________________________________*/

    public bool IsGrabbing()
    {
        if(controller == null)
            return false;
        return controller.GetButton(SixenseButtons.TRIGGER);
    }

    protected override void UpdateObject(SixenseInput.Controller controller)
    {
        if (controller.Enabled)//m_animator != null
            UpdateHandAnimation();//UpdateAnimationInput(controller);

        base.UpdateObject(controller);//press start & move hand
    }

    protected void Update()//override SixenseObjectController Update
    {
        controller = SixenseInput.GetController(Hand);
        if (controller != null && controller.Enabled)
            UpdateObject(controller);
    }

    private void UpdateHandAnimation()
	{
        if (controller.GetButton(SixenseButtons.TRIGGER) )// Fist or Point
        {
            if (controller.GetButton(SixenseButtons.BUMPER) )
            {
                SetAnimation(HandAnimation.Fist);

                float fTriggerVal = Mathf.Lerp(m_fLastTriggerVal, controller.Trigger, 0.1f);
                m_animator.SetFloat("FistAmount", fTriggerVal);
                m_fLastTriggerVal = fTriggerVal;

                return;
            }// Fist

            SetAnimation(HandAnimation.Point);
            return;
        }

        switch (Hand)//GripBall or HoldBook or Idle
        {
            case SixenseHands.RIGHT: if (controller.GetButton(SixenseButtons.TWO))
                    SetAnimation(HandAnimation.GripBall);
                else if (controller.GetButton(SixenseButtons.THREE))
                    SetAnimation(HandAnimation.HoldBook);
                else
                    SetAnimation(HandAnimation.Idle);
                break;
            default: if (controller.GetButton(SixenseButtons.ONE) )
                    SetAnimation(HandAnimation.GripBall);
                else if (controller.GetButton(SixenseButtons.FOUR) )
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