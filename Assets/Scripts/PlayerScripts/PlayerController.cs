using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for SceneManager
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject Inventory;
    private bool alive;
    private CharacterController characterController;
    private PlayerInventory inventory;
    private Animator animator;
    private Transform moveTransform;
    private Vector3 toMouse;
    private Vector3 move;
    private float desiredAngle;
    private int moving;
    [SerializeField] public float maxSpeed;
    [SerializeField] private float sprintBoost;
    [SerializeField] private float gravity;
    [SerializeField] private AudioSource walkSteps;
    [SerializeField] private AudioSource sprintSteps;
    [SerializeField] private Image staminaBar;
    [SerializeField] public float stamina, maxStamina;
    [SerializeField] private float sprintCost, chargeRate;
    private Coroutine recharge;

    void Start()
    {
        alive = true;
        characterController = GetComponent<CharacterController>();
        moveTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("Is_Dead", false);
        inventory = Inventory.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (alive)
        {
            toMouse = GetMouseVector();
            desiredAngle = AngleFromVector(toMouse);
            toMouse.z = toMouse.y;
            toMouse.y = 0f;
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
                    move = toMouse * maxSpeed * sprintBoost; 
                    stamina -= sprintCost * Time.deltaTime;
                    if (stamina <= 0) stamina = 0;
                    staminaBar.fillAmount = stamina / maxStamina;
                    if (recharge != null) StopCoroutine(recharge);
                    recharge = StartCoroutine(RechargeStamina());
                }
                else
                {
                    sprintSteps.Stop();
                    if (!walkSteps.isPlaying)
                    {
                        walkSteps.Play();
                    }
                    move = toMouse * maxSpeed;
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
            
            if (Input.GetKeyDown("a"))
                inventory.UseItem("carrot");
            if (Input.GetKeyDown("s"))
                inventory.UseItem("goldenCarrot");
            if (Input.GetKeyDown("d"))
                inventory.UseItem("cabbage");
        }
        else{
            animator.SetBool("Is_Dead", true);
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

    private void OnAnimatorMove()
    {
        moveTransform.eulerAngles = new Vector3(0f, desiredAngle, 0f);
    }
    private Vector3 GetMouseVector()
    {
        Vector3 result;
        Vector3 centerScreen = new Vector3(0f, 0f, 0f);
        centerScreen.Set((Screen.width / 2), (Screen.height / 2), 0);
        result = Input.mousePosition - centerScreen;
        result.Normalize();
        return result;
    }

    private float AngleFromVector(Vector3 vec)
    {
        float result;
        result = (float)Math.Acos(vec.y);
        result = (180f / (float)Math.PI) * result;
        if (vec.x < 0)
        {
            result = result * -1;
        }
        return result;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    public void SetMaxSpeed(float newMaxSpeed)
    {
        maxSpeed = newMaxSpeed;
    }
    public float GetSprintCost()
    {
        return sprintCost;
    }

    public void Dies()
    {
        alive = false;
    }
    public void SetSprintCost(float newSprintCost)
    {
        sprintCost = newSprintCost;
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
