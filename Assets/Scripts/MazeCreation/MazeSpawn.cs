using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class coord
{
    public int x;
    public int y;

    public coord( int x, int y)
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
    private int RoomsSpawned = 1;

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
    coord[] CoordLookupTable = { new coord(0, 1),
                                 new coord(1, 0),
                                 new coord(0, -1),
                                 new coord(-1, 0)
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
    int GetCompass(byte room)
    {
        int compass = room & 0b11110000;
        compass = compass >> 4;
        return compass;
    }

    // Given a room's identifying byte isolates the value associated with room type.
    int GetRoomType(byte room)
    {
        int roomType = room & 0b00001111;
        return roomType;
    }

    byte MakeRoom(int dir, int type)
    {
        int result = 0b00000000;
        result = (result & (dir << 4));
        result = result & type;
        return (byte)result;
    }

    bool RoomExistsAtCoordinates(byte[][] sim, coord location)
    {
        if (sim[location.x][location.y] != 0)
        {
            return true;
        }
        return false;
    }

    bool CorridorHitsWall(byte[][] sim, coord location, int dir)
    {
        coord neighbor = location.add(CoordLookupTable[dir]);
        if (!RoomExistsAtCoordinates(sim, neighbor))
        {
            return false;
        }
        int opposite = (dir + 2) % 4;
        int roomtype = GetRoomType(sim[neighbor.x][neighbor.y]);
        for (int i = 0; i < 4; i++)
        {
            if (RelativeEntranceCoords[roomtype, i] == opposite)
            {
                return false;
            }
        }
        return true;
    }

    bool RoomPlacementIsValid(byte[][] sim, coord location, byte room)
    {
        int dir;
        int rotation = GetCompass(room);
        int RoomType = GetRoomType(room);
        for (int i = 0; i < 4; i++)
        {
            dir = RelativeEntranceCoords[RoomType, i];
            if (dir != 4)
            {
                dir = CoordRotation(dir, rotation);
                if (CorridorHitsWall(sim, location, dir))
                {
                    return false;
                }
            }
        }    
        return true;
    }

    List<byte> GetPossibleRooms(byte[][] sim, coord location)
    {
        List<byte> result = new List<byte>();
        for (int RoomType = 1; RoomType < 6; RoomType++)
        {
            for (int rotation = 0; rotation < 4; rotation++)
            {
                byte room = MakeRoom(rotation, RoomType);
                if (RoomPlacementIsValid(sim, location, room))
                {
                    result.Add(room);
                }
            }
        }
        return result;
    }

    // Updates the open corridor count when new rooms are created.
    // Given the coordinates of the newly created room.
    void CountOpenCorridors(byte[][] sim, coord location)
    {
        byte room = sim[location.x][location.y];
        int RoomType = (int) GetRoomType(room);
        int[] RelativeExits = new int[4];
        int[] RoomExits = new int[4];
        for (int i  = 0; i < 4; i++)
        {
            RoomExits[i] = RelativeEntranceCoords[RoomType, i];
            RelativeExits[i] = CoordRotation((int) GetCompass(room), RoomExits[i]);
        }
        for (int i = 0; i < 4; i++)
        {
            if ((RelativeExits[i] <= 3) && (RelativeExits[i] >= 0))
            {
                coord NewCoord = location.add(CoordLookupTable[RelativeExits[i]]);
                if (!RoomExistsAtCoordinates(sim, NewCoord))
                {
                    NumberOfOpenCorridors++;
                }
            }
        }
    }

    void TrimPossibleRooms(List<byte> PossibleRooms)
    {
        List<byte> NewPossiblities = new List<byte>();
        foreach (byte i in PossibleRooms) 
        {
            if (EntranceNumberSheet[GetRoomType(i)] - 1 <= (NumberOfRooms - RoomsSpawned))
            {
                NewPossiblities.Add(i);
            }
        }
        PossibleRooms = NewPossiblities;
    }

    byte PickARoom(List<byte> PossibleRooms)
    {
        int HighestIndex = PossibleRooms.Count;
        int Pick = UnityEngine.Random.Range(0, HighestIndex);
        return PossibleRooms[Pick];
    }

    void SpawnRoomAt(byte[][] sim, coord location)
    {
        List<byte> Possibilities = GetPossibleRooms(sim, location);
        TrimPossibleRooms(Possibilities);
        byte choice = PickARoom(Possibilities);
        sim[location.x][location.y] = choice;
        int RoomType = GetRoomType(choice);
        for (int i = 0; i < 4; i++) 
        {
            int dir = RelativeEntranceCoords[RoomType, i];
            if (dir < 4) 
            {
                coord offset = CoordLookupTable[CoordRotation(dir, GetCompass(choice))];
                if (!RoomExistsAtCoordinates(sim, offset.add(location)))
                {
                    SpawnRoomAt(sim, offset.add(location));
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
        coord center = new coord((SimSize / 2), (SimSize / 2));
        sim[center.x][center.y] = 0b00000001;
        SpawnRoomAt(sim, center.add(CoordLookupTable[0]));
    }

}
