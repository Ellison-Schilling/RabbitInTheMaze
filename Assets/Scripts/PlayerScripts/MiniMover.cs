using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMover : MonoBehaviour
{
    public GameObject modelPrefab;

    GameObject model;

    Vector3 realPosition;
    Vector3 modelPosition = new Vector3();

    Vector3 offset = new Vector3(10100f, 0f, 10100f);
    float scale = .01f;


    void updatePosition()
    {
        realPosition = (gameObject.transform.position) * scale;
        modelPosition = realPosition + offset;
    }

    // Start is called before the first frame update
    void Start()
    {
        updatePosition();
        model = Instantiate(modelPrefab, modelPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
        model.transform.position = modelPosition;
    }
}
