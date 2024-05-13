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

    // Used to keep track of number of corridors that do not lead into another room
    // starts at one as the starting room should only have one exit.
    private int NumberOfOpenCorridors = 1;

    // Used to find the number of entrances into a room based on room style number.
    int[] EntranceNumberSheet = { 0, 1, 2, 2, 3, 4 };
    /* Gives a list of compass directions where the default room orientation has
       an entrance.
       0 - North
       1 - East
       2 - South
       3 - West
       4 - No exit
    */
    int[,] RelativeEntranceCoords = { {4, 4, 4, 4},
                                      {0, 4, 4, 4},
                                      {0, 2, 4, 4},
                                      {0, 1, 4, 4},
                                      {0, 1, 3, 4},
                                      {0, 1, 2, 3},
                                    };
    // Converts comapss direction numbers into relative coordinates. (x, y) aka (col, row)
    int[,] CoordLookupTable = { { 0, 1 },
                                { 1, 0 },
                                { 0, -1 },
                                { -1, 0 }
                              };
    const int RoomSize = 100; // The unit size of a room. 100 means 100 x 100 units.
    const int SimSize = 100; // The size of the spawning space for rooms. 100
                             //  means a space of 100 x 100 rooms.
    
    // Rotates a given compass direction number based on the rooms compass direction number rotation
    int CoordRotation(int RoomDirection, int CorridorDirection)
    {
        if (CorridorDirection == 4) return 4;
        int result = (RoomDirection + CorridorDirection) % 4;
        return result;
    }

    // Initializes the matrix used to generate the map.
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

    // Given a room's identifying byte isolates the value associated with compass direction.
    uint GetCompass(byte room)
    {
        uint compass = (uint) (room & 0b11110000);
        compass = compass >> 4;
        return compass;
    }

    // Given a room's identifying byte isolates the value associated with room type.
    uint GetRoomType(byte room)
    {
        uint roomType = (uint) (room & 0b00001111);
        return roomType;
    }



    void SpawnRoomAt(byte[][] sim, int[] coord)
    {
        switch (GetRoomType(sim[coord[0]][coord[1]])) 
        {
            case 0:
                break;
        }

    }

    bool RoomExistsAtCoordinates(byte[][] sim, int[] coord)
    {
        if (sim[coord[0]][coord[1]] != 0)
        {
            return true;
        }
        return false;
    }

    // Updates the open corridor count when new rooms are created.
    // Given the coordinates of the newly created room.
    void CountOpenCorridors(byte[][] sim, int[] coord)
    {
        byte room = sim[coord[0]][coord[1]];
        int RoomType = (int) GetRoomType(room);
        int[] RelativeExits = new int[4];
        int[] RoomExits = new int[4];
        int x;
        int y;
        for (int i  = 0; i < 4; i++)
        {
            RoomExits[i] = RelativeEntranceCoords[RoomType, i];
            RelativeExits[i] = CoordRotation((int) GetCompass(room), RoomExits[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            if ((RelativeExits[i] <= 3) && (RelativeExits[i] >= 0))
            {
                x = CoordLookupTable[RelativeExits[i], 0];
                y = CoordLookupTable[RelativeExits[i], 1];
                x = x + coord[0];
                y = y + coord[1];
                int[] NewCoord = {x, y};
                if (!RoomExistsAtCoordinates(sim, NewCoord))
                {
                    NumberOfOpenCorridors++;
                }
            }
        }
    }

    // Start is called before the first frame update
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
         * 0 - EmptyRoom
         * 1 - OneRoom
         * 2 - TwoRoomStraight
         * 3 - TwoRoomBent
         * 4 - ThreeRoom
         * 5 - FourRoom
         */
        byte[][] sim = MakeSimMatrix();
        int[] center = { SimSize / 2, SimSize / 2 };
        sim[center[0]][center[1]] = 0b00000001;
        SpawnRoomAt(sim, center);
    }

}
