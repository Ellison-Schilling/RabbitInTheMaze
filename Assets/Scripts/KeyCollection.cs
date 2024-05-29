using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public AudioSource collect_noise;
    public float rotationSpeed = 50f;
    private GameObject gate_swing_right;
    private GameObject gate_swing_left;

    static bool keyFound = false;

    // Start is called before the first frame update
    void Start()
    {
        keyFound = false;
        gate_swing_left = GameObject.FindWithTag("RightGate");
        gate_swing_right = GameObject.FindWithTag("LeftGate");
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collect_noise.Play();
            Destroy(gameObject);
            keyFound = true;
            gate_swing_left.transform.Rotate(0,-60,0);
            gate_swing_right.transform.Rotate(0,60,0);
        }
    }

    public bool isKeyFound()
    {
        return keyFound;
    }

}
