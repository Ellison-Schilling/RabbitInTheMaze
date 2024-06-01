using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private PlayerController player;
    [SerializeField] private float carrotSpeedBoost;
    [SerializeField] private float goldenCarrotSpeedBoost;
    [SerializeField] private float cabbageStaminaBoost;
    [SerializeField] private float carrotTimer;
    [SerializeField] private float goldenCarrotTimer;
    [SerializeField] private float cabbageTimer;
    private int keyCount;
    private int carrotCount;
    private int goldenCarrotCount;
    private int cabbageCount;
    [SerializeField] private TextMeshProUGUI CarrotCountDisplay;
    [SerializeField] private TextMeshProUGUI GoldenCarrotCountDisplay;
    [SerializeField] private TextMeshProUGUI CabbageCountDisplay;
    [SerializeField] private AudioSource general_collect_noise;
    [SerializeField] private AudioSource key_collect_noise;

    void Start()
    {
        player = Player.GetComponent<PlayerController>();
        keyCount = 0;
        carrotCount = 0;
        goldenCarrotCount = 0;
        cabbageCount = 0;
    }

    public void AddItem(string type)
    {
        switch (type)
        {
            case "carrot":
                carrotCount++;
                general_collect_noise.Play(); 
                CarrotCountDisplay.text = string.Format("{0}", carrotCount);
                break;
            case "goldenCarrot":
                goldenCarrotCount++;
                general_collect_noise.Play(); 
                GoldenCarrotCountDisplay.text = string.Format("{0}", goldenCarrotCount);
                break;
            case "cabbage":
                cabbageCount++;
                general_collect_noise.Play(); 
                CabbageCountDisplay.text = string.Format("{0}", cabbageCount);
                break;
            case "key":
                keyCount++;
                key_collect_noise.Play(); 
                break;
            default:
                Debug.Log("This doesn't seem useful to me...");
                return;
        }
    }

    public void UseItem(string type)
    {
        switch (type)
        {
            case "carrot":
                if(carrotCount > 0)
                {
                    carrotCount--;
                    CarrotCountDisplay.text = string.Format("{0}", carrotCount);
                    CarrotEffect();
                }
                break;
            case "goldenCarrot":
                if(goldenCarrotCount > 0)
                {
                    goldenCarrotCount--;
                    GoldenCarrotCountDisplay.text = string.Format("{0}", goldenCarrotCount);
                    GoldenCarrotEffect();
                }
                break;
            case "cabbage":
                if(cabbageCount > 0)
                {
                    cabbageCount--;
                    CabbageCountDisplay.text = string.Format("{0}", cabbageCount);
                    CabbageEffect();
                }
                break;
            default:
                Debug.Log("What just happened???");
                return;
        }
    }

    public int GetItemCount(string type)
    {
        switch (type)
        {
            case "carrot":
                return carrotCount;
            case "goldenCarrot":
                return goldenCarrotCount;
            case "cabbage":
                return cabbageCount;
            case "key":
                return keyCount;
            default:
                Debug.Log("What are you looking for?");
                return 0;
        }
    }
    private void CarrotEffect()
    {
        // play audio for using a carrot
        float newMaxSpeed = player.GetMaxSpeed();
        newMaxSpeed*=carrotSpeedBoost;
        player.SetMaxSpeed(newMaxSpeed);
        StartCoroutine(ReturnSpeed(carrotSpeedBoost, carrotTimer));
        return;
    }

    private void GoldenCarrotEffect()
    {
        // play audio for using a golden carrot
        float newMaxSpeed = player.GetMaxSpeed();
        newMaxSpeed*=goldenCarrotSpeedBoost;
        player.SetMaxSpeed(newMaxSpeed);
        StartCoroutine(ReturnSpeed(goldenCarrotSpeedBoost, goldenCarrotTimer));
        return;
    }

    private void CabbageEffect()
    {
        // play audio for using a cabbage
        float newSprintCost = player.GetSprintCost();
        newSprintCost/=cabbageStaminaBoost;
        player.SetSprintCost(newSprintCost);
        StartCoroutine(ReturnStamina(cabbageStaminaBoost, cabbageTimer));
        return;
    }

    IEnumerator ReturnSpeed(float delta, float timer)
    {

        yield return new WaitForSeconds(timer);
        float newMaxSpeed = player.GetMaxSpeed();
        newMaxSpeed/=delta;
        player.SetMaxSpeed(newMaxSpeed);
    }

    IEnumerator ReturnStamina(float delta, float timer)
    {
        yield return new WaitForSeconds(timer);
        float newSprintCost = player.GetSprintCost();
        newSprintCost*=delta;
        player.SetSprintCost(newSprintCost);
    }
}
