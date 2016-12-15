using UnityEngine;
using System.Collections;

/// <summary>
/// this class is responsible for creating the tiles 
/// for the ground, based on an array
/// </summary>
public class JDGroundSpawner : MonoBehaviour
{/// <summary>
/// how many tiles wide is the grid
/// </summary>
    public int mapWidth = 10;
/// <summary>
/// how many tiles high is the grid
/// </summary>
    public int mapHeight = 10;

    /// <summary>
    /// how wide each tile is
    /// </summary>
    public float tileXScale = 1;
    /// <summary>
    /// how tall each tile is
    /// </summary>
    public float tileYScale = 1;

/// <summary>
/// the array in which we hold the ground tiles
/// </summary>
    public GameObject[,] groundArray;
    /// <summary>
    /// the ground tile we are spawning
    /// </summary>
    public GameObject ground;


    /// <summary>
    /// instantiate the array
    /// and call the function to create the terrain
    /// </summary>
	void Start () {
        groundArray = new GameObject[mapHeight,mapWidth];

        InstantiateTerrain();
	}


    /// <summary>
    /// create the terrain grid, assign random ground types
    /// </summary>
    void InstantiateTerrain()
    {
        GameObject tile;
        //int r;
        for (int x = 0; x <= mapWidth - 1; x++)
        {
            for (int y = 0; y <= mapHeight - 1; y++)
            {
                tile = ground;

                Vector3 position = new Vector3(x * tileXScale,  y*tileYScale,0);
                tile = Instantiate(tile, position, Quaternion.Euler(90,0,0)) as GameObject;
                tile.GetComponentInChildren<JDGroundClass>()._tileStatus = JDGroundClass.tiles.dirt;
                tile.transform.Rotate(new Vector3(1, 0, 0), 90);
                groundArray[x, y] = tile;
            }
        }
    }

    /// <summary>
    /// Sets the terrain to the saved terrain
    /// </summary>
    /// <param name="types">The types of the tiles in strings</param>
    public void ChangeTerrain(string[,] types)
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                groundArray[y, x].GetComponent<JDGroundClass>().LoadStatus(types[y, x]);
            }
        }
        
    }

    ////TODO: Needs to return a string for save and load
    /*
    GameObject GetGroundTile(int tile)
    {
        switch (tile)
        {
            case (int)JDStaticVariables.tiles.dirt:
                return ground;
            case (int)JDStaticVariables.tiles.tilledDirt:
                return tilledGround;
        }
        return ground;
    }*/
}
