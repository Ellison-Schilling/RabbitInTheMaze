using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCheckbox : ArrowManager
{
    public TextMeshProUGUI HardMode;

    void Start()
    {
        if (ArrowOn)
        {
            HardMode.text = "Hard Mode Off";
        }
        else
        {
            HardMode.text = "Hard Mode On";
        }
    }

    void Update()
    {
        if (ArrowOn)
        {
            HardMode.text = "Hard Mode Off";
        }
        else
        {
            HardMode.text = "Hard Mode On";
        }
    }

    public void click()
    {
        ArrowOn = !ArrowOn;
    }
}
