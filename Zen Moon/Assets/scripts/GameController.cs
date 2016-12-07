using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the Game elements
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// The grid
    /// </summary>
    public GameObject grid;
    /// <summary>
    /// The player
    /// </summary>
    public GameObject player;
    /// <summary>
    /// A reference to the seeds object
    /// </summary>
    public GameObject seeds;
    /// <summary>
    /// A reference to the grid spawner
    /// </summary>
    JDGroundSpawner gridCon;
    /// <summary>
    /// A reference to the player controller
    /// </summary>
    PlayerController playerCon;

	/// <summary>
    /// Sets the grid controller and the player controller and sets objects for the save mechanic
    /// </summary>
	void Start ()
    {
        gridCon = grid.GetComponent<JDGroundSpawner>();
        playerCon = player.GetComponent<PlayerController>();

        SaveLoadController.setPlayer(player);
        SaveLoadController.setInvCon(playerCon.invCon);
        SaveLoadController.setGrid(gridCon);

        if (SaveLoadController.contin)
        {
            print("Loading");
            SaveLoadController.Load();
        }
	}
	
	/// <summary>
    /// Finds out if the player is interacting with the grid
    /// </summary>
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

    /// <summary>
    /// Places seeds on the grid
    /// </summary>
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

    /// <summary>
    /// Switches the tiles according to what tool the player is using to interact with the grid
    /// </summary>
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
