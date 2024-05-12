using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public AudioSource collect_noise;
    public float rotationSpeed = 50f;
    public GameObject gate_swing_right;
    public GameObject gate_swing_left;

    static bool keyFound = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
