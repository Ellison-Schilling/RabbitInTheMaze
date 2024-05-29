using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private int keys;
    private int carrots;
    private int collectables2;
    private int collectables3;
    [SerializeField] private TextMeshProUGUI CarrotCountDisplay;
    [SerializeField] private TextMeshProUGUI Col2CountDisplay;
    [SerializeField] private TextMeshProUGUI Col3CountDisplay;
    void Start()
    {
        keys = 0;
        carrots = 0;
        collectables2 = 0;
        collectables3 = 0;
    }

    public void AddItem(string type)
    {
        switch (type)
        {
            case "carrot":
                carrots++;
                break;
            case "2...":
                collectables2++;
                break;
            case "3...":
                collectables3++;
                break;
            case "key":
                keys++;
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
                if(carrots > 0)
                {
                    carrots--;
                    CarrotEffect();
                }
                break;
            case "2...":
                if(collectables2 > 0)
                {
                    collectables2--;
                    OtherEffect2();
                }
                break;
            case "3...":
                if(collectables3 > 0)
                {
                    collectables3--;
                    OtherEffect3();
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
                return carrots;
            case "2...":
                return collectables2;
            case "3...":
                return collectables3;
            case "key":
                return keys;
            default:
                Debug.Log("What are you looking for?");
                return 0;
        }
    }
    void CarrotEffect()
    {
        return;
    }

    void OtherEffect2()
    {
        return;
    }

    void OtherEffect3()
    {
        return;
    }
}
