using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public AudioSource collect_noise;
    public float rotationSpeed = 50f;

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
        }
    }

    public bool isKeyFound()
    {
        return keyFound;
    }

}
