using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeySpawn : MonoBehaviour
{
    public static int KeySpawned = 0;
    public float DistanceModifier = 1.0f;
    public GameObject KeyItem;

    private int BandModifier = 0;
    private float Target = 10000;

    // Start is called before the first frame update
    void Start()
    {
        Target = Target * DistanceModifier;
        float shot = UnityEngine.Random.Range(0, Target) + gameObject.transform.position.magnitude;
        if (KeySpawned > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (shot > Target)
            {
                Instantiate(KeyItem);
                KeySpawned++;
                Debug.Log("Key Spawned");
            }
            else
            {
                BandModifier++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Target = Target * DistanceModifier;
        float shot = UnityEngine.Random.Range(0, Target) + gameObject.transform.position.magnitude + BandModifier;
        if (KeySpawned > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (shot > Target)
            {
                Instantiate(KeyItem, gameObject.transform.position, gameObject.transform.rotation);
                KeySpawned++;
                Debug.Log("Key Spawned at " + gameObject.transform);
            }
            else
            {
                BandModifier++;
            }
        }
    }
}
