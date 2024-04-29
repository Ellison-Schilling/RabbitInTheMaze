using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public AudioSource collect_noise;
    private bool keyFound = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = 50f;
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
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
