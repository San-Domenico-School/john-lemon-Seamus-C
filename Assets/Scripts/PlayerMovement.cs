using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;


/*  This class accepts user input to create player movement and align it with
 *  the player animation.
 *  
 *  This script is a component of the Player
 *
 *  Seamus
 *  5/14/24
 */


public class PlayerMovement : MonoBehaviour
{
    float turnSpeed;
    Animator animator;
    Rigidbody rb;
    Quaternion rotation;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 20f;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        movement = Vector3.zero;
        rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Setmovement();
        SetIsWalking();
        Setrotation();

    }

    //Sets the value of movement based on user input
    private void Setmovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertial = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertial);
    }

    //Sets the value of the IsWalking parameter in the Animator based on the value of the movement
    private void SetIsWalking()
    {
        if (Mathf.Approximately(movement.magnitude, 0f))
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    //Sets the value of rotation based on the value of the movement
    private void Setrotation()
    {
        //do you need to * by deltaTime if using fixed update
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

    }

    //Moves and rotates the player based on an event from the Animator
    private void OnAnimatorMove()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }


}

