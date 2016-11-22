using UnityEngine;
using System.Collections;

public class JDGroundSpawner : MonoBehaviour {




    public int mapWidth = 10;
    public int mapHeight = 10;

    public float tileXScale;
    public float tileYScale;


    int[,] map;

    public GameObject ground;
    public GameObject tilledGround;



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
    void InstantiateTerrain()
    {
        GameObject tile;
        int r;
        for (int x = 0; x <= mapWidth - 1; x++)
        {
            for (int y = 0; y <= mapHeight - 1; y++)
            {
                r = Random.Range(1, 3);
                //print(r);
                tile = GetGroundTile(r);

                Vector3 position = new Vector3(x * tileXScale, 0, y*tileYScale);
                //tile = Instantiate(ground, position, Quaternion.identity) as GameObject;
                tile = Instantiate(tile, position, Quaternion.Euler(90,0,0)) as GameObject;
                //tile.transform.Rotate(new Vector3(1, 0, 0), 90);
            }
        }
    }
    GameObject GetGroundTile(int tile)
    {
        switch (tile)
        {
            case 1:
                return ground;
                break;
            case 2:
                return tilledGround;
                break;
        }
        return ground;
    }
}
