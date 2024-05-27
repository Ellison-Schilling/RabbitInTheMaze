using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
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
    public GameObject StartRoom;

    // Used to keep track of number of corridors that do not lead into another room
    // starts at one as the starting room should only have one exit.
    private int RoomsSpawned = 1;

    // Converts comapss direction numbers into relative coordinates. (x, y) aka (col, row)
    static coord[] CoordLookupTable = { new coord(0, 1),
                                 new coord(1, 0),
                                 new coord(0, -1),
                                 new coord(-1, 0)
                               };

    public room[] RoomLookupTable =
    {
        null, // 0
        new room(1, 3), // 1
        new room(1, 2), // 2
        new room(3, 2), // 3
        new room(1, 1), // 4
        new room(2, 1), // 5
        new room(3, 1), // 6
        new room(4, 2), // 7
        new room(1, 0), // 8
        new room(3, 3), // 9
        new room(2, 0), // 10
        new room(4, 3), // 11
        new room(3, 0), // 12
        new room(4, 0), // 13
        new room(4, 1), // 14
        new room(5, 0), // 15
    };

    const int RoomSize = 100; // The unit size of a room. 100 means 100 x 100 units.
    const int SimSize = 100; // The size of the spawning space for rooms. 100
                             //  means a space of 100 x 100 rooms.
    public class coord
    {
        public int x;
        public int y;

        public coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public coord add(coord other)
        {
            coord result = new coord(this.x + other.x, this.y + other.y);
            return result;
        }

        public coord sub(coord other)
        {
            coord result = new coord(this.x - other.x, this.y - other.y);
            return result;
        }

        public bool IsEqual(coord other)
        {
            return ((this.x == other.x) && (this.y == other.y));
        }
    }

    public class room
    {
        public int RoomType;
        public int Rotation;

        public room(int roomType, int rotation)
        {
            this.RoomType = roomType;
            this.Rotation = rotation;
        }
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

    void PrintSim(byte[][] sim)
    {
        string PrintOut = "";
        for (int i = 0; i < SimSize; i++)
        {
            PrintOut += i + ": ";
            for (int j = 0; j < SimSize; j++)
            {
                PrintOut += "[" + sim[j][i] + "]";
            }
            PrintOut += '\n';
        }
        Debug.Log(PrintOut);
    }

    int ChooseRandomly()
    {
        /* Bit of a hack, when converting from float to int if the float is below a
        * whole number the int it converts to gets rounded down (e.g. .99999 -> 0 or 1.999 -> 1).
        * As Rooms spawned apporaches Number of rooms the ratio approaches 0.
        * random.value returns a float between 0.0 and 1.0 so the more rooms spawned
        * the lower the chance random.value + ratio is above 1.0.
        */
        float ratio = (float)RoomsSpawned / (float)NumberOfRooms;
        ratio = 0.999f - ratio;
        int result = (int)(UnityEngine.Random.value + ratio);
        Debug.Log("The fates have decided " + result);
        return result;
    }

    int OppositeSwizzle(byte room)
    {
        int result, holder;
        holder = room & 0b_0000_0011;
        holder = holder << 2;
        result = room >> 2;
        result = result | holder;
        return result;
    }

    void MakeRoom(byte[][] sim, coord location)
    {
        Debug.Log("Spawning room: " + RoomsSpawned + " at " + location.x + ", " + location.y);

        coord working;
        int RoomType = 0;
        working = location.add(CoordLookupTable[0]);
        RoomType = RoomType | (OppositeSwizzle(sim[working.x][working.y]) & 0b_0000_1000);
        working = location.add(CoordLookupTable[1]);
        RoomType = RoomType | (OppositeSwizzle(sim[working.x][working.y]) & 0b_0000_0100);
        working = location.add(CoordLookupTable[2]);
        RoomType = RoomType | (OppositeSwizzle(sim[working.x][working.y]) & 0b_0000_0010);
        working = location.add(CoordLookupTable[3]);
        RoomType = RoomType | (OppositeSwizzle(sim[working.x][working.y]) & 0b_0000_0001);
        Debug.Log("Required room shape: " + RoomType);
        // Needed doors should be represented now.

        List<coord> ToSpawn = new List<coord>();
        int holder = 0;
        // Case where no room is north
        if ((RoomType & 0b_0000_1000) == 0) 
        {
            working = location.add(CoordLookupTable[0]);
            Debug.Log("Room has no north neighbor");
            holder = ChooseRandomly();
            if ((holder == 1) && (sim[working.x][working.y] == 0))
            {
                RoomType = RoomType | 0b_0000_1000;
                RoomsSpawned++;
                ToSpawn.Add(CoordLookupTable[0]);
            }
        }
        // Case where no room is East
        if ((RoomType & 0b_0000_0100) == 0)
        {
            working = location.add(CoordLookupTable[1]);
            Debug.Log("Room has no East neighbor");
            holder = ChooseRandomly();
            if ((holder == 1) && (sim[working.x][working.y] == 0))
            {
                RoomType = RoomType | 0b_0000_0100;
                RoomsSpawned++;
                ToSpawn.Add(CoordLookupTable[1]);
            }
        }
        // Case where no room is South
        if ((RoomType & 0b_0000_0010) == 0)
        {
            working = location.add(CoordLookupTable[2]);
            Debug.Log("Room has no south neighbor");
            holder = ChooseRandomly();
            if ((holder == 1) && (sim[working.x][working.y] == 0))
            {
                RoomType = RoomType | 0b_0000_0010;
                RoomsSpawned++;
                ToSpawn.Add(CoordLookupTable[2]);
            }
        }
        // Case where no room is West
        if ((RoomType & 0b_0000_0001) == 0)
        {
            working = location.add(CoordLookupTable[3]);
            Debug.Log("Room has no west neighbor");
            holder = ChooseRandomly();
            if ((holder == 1) && (sim[working.x][working.y] == 0))
            {
                RoomType = RoomType | 0b_0000_0001;
                RoomsSpawned++;
                ToSpawn.Add(CoordLookupTable[3]);
            }
        }

        sim[location.x][location.y] = (byte)RoomType;
        Debug.Log("Final room type: " + RoomType);
        foreach(coord go in ToSpawn)
        {
            MakeRoom(sim, location.add(go));
        }
    }

    public void spawn(room Room, coord location)
    {
        UnityEngine.Vector3 RoomTransform = new UnityEngine.Vector3(location.x * RoomScale, 0, location.y * RoomScale);
        UnityEngine.Quaternion RoomRot = new UnityEngine.Quaternion();
        RoomRot.eulerAngles = new UnityEngine.Vector3(0, (Room.Rotation - 1) * 90, 0);
        switch (Room.RoomType)
        {
            case 1:
                Instantiate(OneRoom, RoomTransform, RoomRot);
                break;
            case 2:
                Instantiate(TwoRoomStraight, RoomTransform, RoomRot);
                break;
            case 3:
                Instantiate(TwoRoomBent, RoomTransform, RoomRot);
                break;
            case 4:
                Instantiate(ThreeRoom, RoomTransform, RoomRot);
                break;
            case 5:
                Instantiate(FourRoom, RoomTransform, RoomRot);
                break;
        }
    }

    void InstantiateSim(byte[][] sim, coord center)
    {
        int room;
        coord location = new coord(0, 0);
        for (int ys = 0; ys < SimSize; ys++)
        {
            for (int xs = 0; xs < SimSize; xs++)
            {
                location.x = xs;
                location.y = ys;
                if (location.Equals(center) )
                {
                    Instantiate(StartRoom, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
                }
                room = sim[xs][ys];
                if (room != 0)
                {
                    spawn(RoomLookupTable[room], location.sub(center));
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
        Debug.Log("Making Sim " + SimSize + " in size with " + NumberOfRooms + " rooms.");
        byte[][] sim = MakeSimMatrix();
        coord center = new coord((SimSize / 2), (SimSize / 2));
        sim[center.x][center.y] = 0b_0000_1000;
        Debug.Log("Spawning rooms");
        RoomsSpawned++;
        MakeRoom(sim, center.add(CoordLookupTable[0]));
        Debug.Log("Finished spawing rooms");
        PrintSim(sim);
        InstantiateSim(sim, center);
    }

}