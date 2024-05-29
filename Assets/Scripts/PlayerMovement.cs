using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for SceneManager
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    private bool alive;
    private CharacterController characterController;
    private Animator animator;
    private Transform m_Transform;
    private Vector3 to_mouse;
    private Vector3 move;
    [SerializeField]private float desired_Angle;
    private int moving;
    [SerializeField] private float max_speed;
    [SerializeField] private float sprint_boost;
    [SerializeField] private float gravity;
    [SerializeField] private AudioSource walkSteps;
    [SerializeField] private AudioSource sprintSteps;
    [SerializeField] private Image staminaBar;
    [SerializeField] private float stamina, maxStamina, sprintCost, ChargeRate;
    private Coroutine recharge;

    void Start()
    {
        alive = true;
        characterController = GetComponent<CharacterController>();
        m_Transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("Is_Dead", false);
    }

    void Update()
    {
        if (alive)
        {
            to_mouse = getMouseVector();
            desired_Angle = AngleFromVector(to_mouse);
            to_mouse.z = to_mouse.y;
            to_mouse.y = 0f;
            animator.SetFloat("Speed", moving);
            if (Input.GetAxis("Go") == 1)
            {
                if (Input.GetAxis("Sprint") == 1 && stamina > 0)
                {
                    walkSteps.Stop();
                    if (!sprintSteps.isPlaying)
                    {
                        sprintSteps.Play();
                    }
                    move = to_mouse * max_speed * sprint_boost; 
                    stamina -= sprintCost * Time.deltaTime;
                    if (stamina <= 0) stamina = 0;
                    staminaBar.fillAmount = stamina / maxStamina;
                    if (recharge != null) StopCoroutine(recharge);
                    recharge = StartCoroutine(rechargeStamina());
                }
                else
                {
                    sprintSteps.Stop();
                    if (!walkSteps.isPlaying)
                    {
                        walkSteps.Play();
                    }
                    move = to_mouse * max_speed;
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
            
            if (Input.GetKeyDown("a")){
                //use carrot
            }
            if (Input.GetKeyDown("s")){
                //use col2
            }
            if (Input.GetKeyDown("d")){
                //use col3
            }

        }
    }

    void FixedUpdate()
    {
        if (alive)
        {
            if (!characterController.isGrounded)
            {
                move.y += (move.y - 1) * gravity;
            }
            characterController.Move(move);
        }
    }

    void OnAnimatorMove()
    {
        m_Transform.eulerAngles = new Vector3(0f, desired_Angle, 0f);
    }
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

    public float giveMaxSpeed()
    {
        return max_speed;
    }

    private IEnumerator rechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (stamina < maxStamina)
        {
            stamina += ChargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
