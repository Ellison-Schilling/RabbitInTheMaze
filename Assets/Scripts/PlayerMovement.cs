using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    Transform m_Transform;
    Vector3 to_mouse;
    Vector3 move;
    float desired_Angle;
    int moving;

    public float max_speed;
    public float gravity;

    public KeyCollection key; //imports the KeyCollection script
    public bool hasKey;
    public AudioSource walkSteps;
    public AudioSource sprintSteps;
    public AudioSource gameWon;

    Vector3 getMouseVector()
    {
        Vector3 result;
        Vector3 center_screen = new Vector3(0f, 0f, 0f);
        center_screen.Set((Screen.width / 2), (Screen.height / 2), 0);
        result = Input.mousePosition - center_screen;
        result.Normalize();
        return result;
    }

    float AngleFromVector(Vector3 vec)
    {
        float result;
        result = (float)Math.Acos(to_mouse.y);
        result = (180f / (float)Math.PI) * result;
        if (to_mouse.x < 0)
        {
            result = result * -1;
        }
        return result;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_Transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

    void Update()
    {
        to_mouse = getMouseVector();
        desired_Angle = AngleFromVector(to_mouse);
        to_mouse.z = to_mouse.y;
        to_mouse.y = 0f;
        animator.SetFloat("Speed", moving);
        if (Input.GetAxis("Go") == 1)
        {
            if (Input.GetAxis("Sprint") == 1)
            {
                walkSteps.Stop();
                if (!sprintSteps.isPlaying)
                {
                    sprintSteps.Play();
                }
                move = to_mouse * max_speed;
            }
            else
            {
                sprintSteps.Stop();
                if (!walkSteps.isPlaying)
                {
                    walkSteps.Play();
                }
                move = to_mouse * max_speed * 0.5f;
            }
            moving = 0;
        }
        else
        {
            walkSteps.Stop();
            sprintSteps.Stop();
            move = Vector3.zero;
            moving = 1;
        }
        hasKey = key.isKeyFound(); //check if the player has found the key
    }

    void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            move.y += (move.y - 1) * gravity;
        }
        characterController.Move(move);
    }
    void OnAnimatorMove()
    {
        m_Transform.eulerAngles = new Vector3(0f, desired_Angle, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("exit"))
        {
            if (hasKey == true)
            {
                gameWon.Play();
                Debug.Log("You completed the maze!");
            }
        }
    }

}
