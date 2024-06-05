using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowCheckbox : ArrowManager
{
    void ChangeToggle()
    {
        ArrowOn = !ArrowOn;
    }
}
