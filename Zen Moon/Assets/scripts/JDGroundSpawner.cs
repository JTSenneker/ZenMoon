using UnityEngine;
using System.Collections;

public class JDGroundSpawner : MonoBehaviour
{
    public int mapWidth = 10;
    public int mapHeight = 10;

    public float tileXScale;
    public float tileYScale;

    public GameObject[,] groundArray;

    public GameObject ground;
    public GameObject tilledGround;



	// Use this for initialization
	void Start () {
        groundArray = new GameObject[mapHeight,mapWidth];
        tileXScale = 1;
        tileYScale = 1;
        
        InstantiateTerrain();
	}
	
	// Update is called once per frame
	void Update () {
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
