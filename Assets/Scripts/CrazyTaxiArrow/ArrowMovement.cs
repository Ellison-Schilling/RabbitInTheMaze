using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : ArrowManager
{
    GameObject MiniKey;
    int counter = 55;
    bool KeyAcquired = false;
    // Start is called before the first frame update

    private Vector3 GetKeyVector()
    {
        Vector3 result;
        result = gameObject.transform.position - MiniKey.transform.position;
        result.y = 0f;
        result.Normalize();
        return result;
    }

    private float AngleFromVector(Vector3 vec)
    {
        float result;
        result = (float)Math.Acos(vec.z);
        result = (180f / (float)Math.PI) * result;
        if (vec.x < 0)
        {
            result = result * -1;
        }
        return result;
    }

    void Start()
    {
        if (!ArrowOn)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MiniKey == null && !KeyAcquired)
        {
            counter++;
            if (counter > 60)
            {
                MiniKey = GameObject.FindWithTag("MiniKey");
                counter = 0;
            }
        }
        else if (MiniKey == null && KeyAcquired)
        {
            gameObject.SetActive(false);
        }    
        else
        {
            KeyAcquired = true;
            transform.rotation = Quaternion.Euler(90f, (AngleFromVector(GetKeyVector()) + 180f), 0f);
        }      
    }
}
