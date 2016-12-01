using UnityEngine;
using System.Collections;

public class JDGroundSpawner : MonoBehaviour {




    public int mapWidth = 10;
    public int mapHeight = 10;

    public float tileXScale;
    public float tileYScale;


   public int[,] map;

    public GameObject ground;
    public GameObject tilledGround;

    /// <summary>
    /// The dirt tile sprite
    /// </summary>
    public Sprite dirt;
    /// <summary>
    /// The tilled tile sprite
    /// </summary>
    public Sprite tilled;
    /// <summary>
    /// The watered tile sprite
    /// </summary>
    public Sprite watered;

    /// <summary>
    /// for now I would comment out the random generation since we aren't using rocks, and branches just yet and we'll just spawn dirt
    /// </summary>


	// Use this for initialization
	void Start () {
        map = new int[mapHeight,mapWidth];
        tileXScale = 1;
        tileYScale = 1;

        SpawnBasicTerrain();
        InstantiateTerrain();
	}
	
	// Update is called once per frame
	void Update () {
	}
    void SpawnBasicTerrain()
    {
        int t = 1;
        for(int x = 0; x <= mapWidth-1; x++)
        {
            for(int y = 0; y <= mapHeight-1; y++)
            {
                map[x,y] = t;
            }
        }
    }

    /// <summary>
    /// create the terrain grid, assign random ground types
    /// </summary>
    void InstantiateTerrain()
    {
        GameObject tile;
        int r;
        for (int x = 0; x <= mapWidth - 1; x++)
        {
            for (int y = 0; y <= mapHeight - 1; y++)
            {
                //exclusive random
                r = Random.Range(1, 3);
                //print(r);
                // tile = GetGroundTile(r);
                tile = ground;

                Vector3 position = new Vector3(x * tileXScale, y*tileYScale, 0);
                tile = Instantiate(ground, position, Quaternion.identity) as GameObject;
                tile.GetComponent<JDGroundClass>().dirt = this.dirt;
                tile.GetComponent<JDGroundClass>().tilled = this.tilled;
                tile.GetComponent<JDGroundClass>().watered = this.watered;
                //tile = Instantiate(tile, position, Quaternion.Euler(90,0,0)) as GameObject;
                tile.GetComponentInChildren<JDGroundClass>()._tileStatus = (JDStaticVariables.tiles)r;
                //tile.transform.Rotate(new Vector3(1, 0, 0), 90);
            }
        }
    }
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
