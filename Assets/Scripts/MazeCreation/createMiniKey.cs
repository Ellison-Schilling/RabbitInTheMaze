using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createMiniKey : MonoBehaviour
{
    public GameObject prefabKey;

    GameObject MiniKey;
    Vector3 offset = new Vector3(10100f, 0f, 10100f);
    float scale = .01f;

    // Start is called before the first frame update
    void Start()
    {
        MiniKey = Instantiate(prefabKey, (gameObject.transform.position * scale) + offset,Quaternion.identity);
    }

    private void OnDestroy()
    {
        Destroy(MiniKey);
    }
}
