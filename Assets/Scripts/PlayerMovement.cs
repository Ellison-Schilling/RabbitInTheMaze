using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    CharacterController characterController;
    Vector3 move;
    float gravity;

    public float speed = 1.0f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            gravity += ((gravity - 1) * 8 * Time.deltaTime);
            if (gravity > 20) { gravity= 20; }
        }
        else
        {
            gravity = 0.0f;
        }
        move = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed);
    }
}
