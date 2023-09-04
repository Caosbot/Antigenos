using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationState_Component
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController controller;

    public void Update()
    {
        CalculateAngle();
        CalculateVelocity();
        Jump();
    }
    private void CalculateAngle()
    {
        float angle = cameraTransform.localEulerAngles.x;
        if (angle <= 360 && angle >= 250) //Up
        {
            float min = 250;
            float max = 360;

            angle = 1 - ((angle - min) / (max - min));
        }
        else //Down
        {
            float min = 0;
            float max = 100;
            angle = (angle - min) / (max - min);
            angle *= -1;
        }
        animator.SetFloat("AimHeight", angle);
    }
    private void CalculateVelocity()
    {
        Vector3 velocityLocal = characterTransform.InverseTransformDirection(controller.velocity);
        float XVelocity = velocityLocal.z;
        float ZVelocity = velocityLocal.x;
        animator.SetFloat("X_Velocity", XVelocity);
        animator.SetFloat("Z_Velocity", ZVelocity);
        if(XVelocity == 0 && ZVelocity == 0)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }
    private void Jump()
    {
        if (Input.GetButton("Jump") && controller.isGrounded)
        {
            //animator.SetBool("Jump", true);
            animator.Play("Jumping", 0);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        animator.SetBool("IsGrounded",controller.isGrounded);
    }
}
