using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Animator animator;
    Quaternion m_Rotation = Quaternion.identity;
    Vector3 center_screen;
    Vector3 to_mouse;
    float desired_Angle;
    public float max_speed;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
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
        m_Rotation.eulerAngles = new Vector3(0f, desired_Angle, 0f);

        if (Input.GetAxis("Stop") == 1)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            if(Input.GetAxis("Sprint") == 1)
            {
                m_Rigidbody.velocity = transform.forward * max_speed;
            }
            else
            {
                m_Rigidbody.velocity = transform.forward * max_speed * 0.5f;
            }
            animator.SetFloat("Speed", 0);
        }
    }

    void OnAnimatorMove () {
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
