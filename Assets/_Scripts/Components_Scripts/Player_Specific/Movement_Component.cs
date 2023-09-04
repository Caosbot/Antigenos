using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Movement_Component : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float jumpSpeed = 20;
    [SerializeField] private float gravity = 10;
    public CharacterController controller;

    public Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump") && controller.isGrounded)
            {
                moveDirection.y = jumpSpeed;
                StartCoroutine(GravityIncrease());
            }
        }

        moveDirection.y -= gravity*Time.deltaTime; //Gravity
        controller.Move(moveDirection * Time.deltaTime);
    }
    private IEnumerator GravityIncrease()
    {
        yield return new WaitForSeconds(0.2f);
        gravity = 50;
        Debug.Log("GravidadeExtra");
        while (!controller.isGrounded)
        {
            yield return 0;
        }
        Debug.Log("Normal");
        gravity = 10;
    }
}
