using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Seed
{

    public static bool[,] GenerateRoomLayout(int width, int height)
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

        return roomLayout;

    }
}
