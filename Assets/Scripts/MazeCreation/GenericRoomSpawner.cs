using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;
    public GameObject Room4;

    List<GameObject> RoomList = new List<GameObject>(); 
    void Start()
    {
        if (Room1 != null) { RoomList.Add(Room1); }
        if (Room2 != null) { RoomList.Add(Room2); }
        if (Room3 != null) { RoomList.Add(Room3); }
        if (Room4 != null) { RoomList.Add(Room4); }

        int i = Random.Range(0, RoomList.Count);
        Instantiate(RoomList[i], gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
