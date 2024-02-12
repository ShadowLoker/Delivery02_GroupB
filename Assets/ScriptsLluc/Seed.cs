using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Seed
{
    public static int width = 14;
    public static int height = 15;

    public static bool[,] GenerateRoomLayout()
    {

        bool[,] roomLayout = new bool[width, height];

        // Set all cells to true initially (walkable areas)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                roomLayout[x, y] = true;
            }
        }

        // Set the outer edges of the room to false (walls)
        for (int x = 0; x < width; x++)
        {
            roomLayout[x, 0] = false;
            roomLayout[x, height - 1] = false;
        }
        for (int y = 0; y < height; y++)
        {
            roomLayout[0, y] = false;
            roomLayout[width - 1, y] = false;
        }

        // Set some inner walls
        for (int x = 2; x < width - 2; x++)
        {
            roomLayout[x, 2] = false;
            roomLayout[x, height - 3] = false;
        }

        return layout1;

    }

    private static bool[,] layout1 =
    {
        {false,false,false,false,false,false,false,false,false,false,false,false,false,false },
        {false,false,false,false,false,false,false,false,true,true,true,true,true,false },
        {false,true,true,true,true,false,true,true,true,true,true,true,true,false },
        {false,true,true,true,true,true,true,false,true,true,true,true,true,false },
        {false,true,true,true,true,false,true,false,true,true,true,true,true,false },
        {false,true,false,false,false,false,true,false,false,false,true,false,false,false },
        {false,true,true,false,false,true,true,true,false,false,true,false,false,false },
        {false,true,true,true,true,true,true,true,true,true,true,true,false,false },
        {false,true,true,false,false,true,true,true,false,false,false,true,false,false },
        {false,true,true,false,false,false,true,false,false,false,true,true,true,false },
        {false,false,true,false,false,false,true,true,true,true,true,true,true,false },
        {false,false,true,false,false,false,true,false,false,false,true,true,true,false },
        {false,true,true,true,true,true,true,true,true,false,false,true,false,false },
        {false,true,true,true,true,true,true,true,true,true,true,true,false,false },
        {false,false,false,false,false,false,false,false,false,false,false,false,false,false }
    };

}
