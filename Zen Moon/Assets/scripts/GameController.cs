using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject grid;
    public GameObject player;
    public GameObject seeds;
    JDGroundSpawner gridCon;
    PlayerController playerCon;

	// Use this for initialization
	void Start ()
    {
        gridCon = grid.GetComponent<JDGroundSpawner>();
        playerCon = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if(playerCon.isInteracting)
        {
            if (playerCon.invCon.currItem.tag == "tool")
            {
                SwitchTile();
            }
            if (playerCon.invCon.currItem.tag == "seeds")
            {
                PlaceSeeds();
            }
            
            playerCon.isInteracting = false;
        }
	}

    void PlaceSeeds()
    {
        GameObject tile = JDMouseTargeting.target;

        if (tile != null)
        {
            JDStaticVariables.tiles tileType = tile.GetComponent<JDGroundClass>()._tileStatus;
            if (tileType == JDStaticVariables.tiles.tilledDirt)
            {
                GameObject newSeeds = (GameObject)Instantiate(seeds, tile.transform.position, Quaternion.identity);
                newSeeds.GetComponent<Seeds>().seedType = playerCon.invCon.currItem.GetComponent<Seeds>().seedType;
            }
        }
    }

    void SwitchTile()
    {
        GameObject tile = JDMouseTargeting.target;

        if (tile != null)
        {
            JDStaticVariables.tiles tileType = tile.GetComponent<JDGroundClass>()._tileStatus;
            if(playerCon.invCon.currItem.GetComponent<Tool>().toolType == Tool.ToolType.hoe)
            {
                tile.GetComponent<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.tilledDirt;
            }
            if (playerCon.invCon.currItem.GetComponent<Tool>().toolType == Tool.ToolType.wateringCan && tileType == JDStaticVariables.tiles.tilledDirt)
            {
                tile.GetComponent<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.watered;
            }
        }
    }
}
