using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int NumberOfRooms;
    public float RoomScale;
    public GameObject FourRoom;
    public GameObject ThreeRoom;
    public GameObject TwoRoomBent;
    public GameObject TwoRoomStraight;
    public GameObject OneRoom;

    const int RoomSize = 100;
    const int SimSize = 100;

    // Start is called before the first frame update
    byte[][] MakeSimMatrix()
    {
        byte[][] sim = new byte[SimSize][];
        for (int i = 0; i < SimSize; i++)
        {
            sim[i] = new byte[SimSize];
            for (int j = 0; j < SimSize; j++)
            {
                sim[i][j] = 0;
            }
        }
        return sim;
    }

    uint GetCompass(byte room)
    {
        uint compass = (uint) (room & 0b11110000);
        compass = compass >> 4;
        return compass;
    }

    uint GetRoomType(byte room)
    {
        uint roomType = (uint) (room & 0b00001111);
        return roomType;
    }

    void SpawnRoomAt(byte[][] sim, Tuple<int, int> coord)
    {
        switch (GetRoomType(sim[coord.Item1][coord.Item2])) 
        {
            case 0:
                break;
        }

    }

    void Start()
    {
        /* First four bits are Direction
         * Second four are room type
         * 
         * 0 - North
         * 1 - East
         * 2 - South
         * 3 - West
         * 
         * 1 - OneRoom
         * 2 - TwoRoomStraight
         * 3 - TwoRoomBent
         * 4 - ThreeRoom
         * 5 - FourRoom
         */
        byte[][] sim = MakeSimMatrix();
        Tuple<int, int> center = new Tuple<int, int>(SimSize/2, SimSize/2);
        sim[center.Item1][center.Item2] = 0b00000001;
        SpawnRoomAt(sim, center);
    }

}
