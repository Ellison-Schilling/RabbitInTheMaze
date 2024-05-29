using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] Transform startPoint;

    void Start()
    {
        GameObject[] allRooms = GameObject.FindGameObjectsWithTag("Room");
        Transform spawnRoom = startPoint;
        float newDistance, maxDistance = 0;

        for (int i = 0; i < allRooms.Length; i++)
        {
            newDistance = Vector3.Distance(startPoint.position, allRooms[i].transform.position);
            if (newDistance > maxDistance)
            {
                maxDistance = newDistance;
                spawnRoom = allRooms[i].transform;
            }
        }

        Vector3 spawnPosition = spawnRoom.position + Vector3.up * 0.5f;
        Instantiate(key, spawnPosition, Quaternion.identity);
    }
}

