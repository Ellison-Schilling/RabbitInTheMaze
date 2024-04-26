using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    Transform m_Transform;
    Vector3 center_screen;
    Vector3 to_mouse;
    Vector3 move;
    float desired_Angle;
    int moving;

    public float max_speed;
    public float gravity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_Transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

    void Update()
    {
        center_screen.Set((Screen.width / 2), (Screen.height / 2), 0);
        to_mouse = Input.mousePosition - center_screen;
        to_mouse.Normalize();
        desired_Angle = (float)Math.Acos(to_mouse.y);
        desired_Angle = (180f / (float)Math.PI) * desired_Angle;
        if (to_mouse.x < 0)
        {
            desired_Angle = desired_Angle * -1;
        }
        animator.SetFloat("Speed", moving);
        if (Input.GetAxis("Go") == 1)
        {
            if (Input.GetAxis("Sprint") == 1)
            {
                move = new Vector3(to_mouse.x, 0f, to_mouse.y) * max_speed;
            }
            else
            {
                move = new Vector3(to_mouse.x, 0f, to_mouse.y) * max_speed * 0.5f;
            }
            moving = 0;
        }
        else
        {
            move = Vector3.zero;
            moving = 1;
        }
        if (!characterController.isGrounded)
        {
            move.y += (move.y - 1) * gravity;
        }
    }

    void FixedUpdate()
    {
        characterController.Move(move);
    }
    void OnAnimatorMove () {
        m_Transform.eulerAngles = new Vector3(0f, desired_Angle, 0f);
    }
}
