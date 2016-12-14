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
    /// The merchant
    /// </summary>
    public GameObject merchant;
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
    /// The day count
    /// </summary>
    int dCount = 0;

    /// <summary>
    /// Sets the grid controller and the player controller and sets objects for the save mechanic
    /// </summary>
    void Start()
    {
        gridCon = grid.GetComponent<JDGroundSpawner>();
        playerCon = player.GetComponent<PlayerController>();

        SaveLoadController.setPlayer(player);
        SaveLoadController.setInvCon(playerCon.invCon);
        SaveLoadController.setGrid(gridCon);

        if (SaveLoadController.contin)
        {
            SaveLoadController.Load();
        }
    }

    /// <summary>
    /// Finds out if the player is interacting with the grid
    /// </summary>
    void Update()
    {
        if (playerCon.isInteracting)
        {
            if (JDMouseTargeting.GetMouseTarget().tag == merchant.tag)
            {
                Time.timeScale = 0;
                merchant.GetComponent<Merchant>().openSell();
            }
            else if (playerCon.invCon.currItem.tag == "tool")
            {
                SwitchTile();
            }
            else if (playerCon.invCon.currItem.tag == "seeds")
            {
                PlaceSeeds();
            }
            else if (playerCon.invCon.currItem.tag == "crop" || playerCon.invCon.currItem.tag == "fence")
            {
                PlaceItems();
            }

            playerCon.isInteracting = false;
        }

        if (GetComponent<DayNightController>().isDay && dCount != JDStaticVariables.dayCount || JDStaticVariables.dayCount == 1 && dCount != JDStaticVariables.dayCount)
        {
            if (merchantArrived())
            {
                Vector2 placement = new Vector2(0, gridCon.mapHeight);
                Instantiate(merchant, placement, Quaternion.identity);
            }
            dCount = JDStaticVariables.dayCount;
        }

        else if (!GetComponent<DayNightController>().isDay)
        {
            CheckPlantsGrow();
        }

    }

    /// <summary>
    /// Figures out if the merchant is coming today
    /// </summary>
    /// <returns>True if the merchant is coming today</returns>
    bool merchantArrived()
    {
        int r = Random.Range(1, 11);
        if (r <= 7)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Places down an item that is being thrown
    /// </summary>
    void PlaceItems()
    {
        GameObject tile = JDMouseTargeting.target;
        GameObject item = playerCon.invCon.currItem;
        if (tile != null)
        {
            GameObject newItem = (GameObject)Instantiate(item, tile.transform.position, Quaternion.identity);
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
            JDGroundClass.tiles tileType = tile.GetComponent<JDGroundClass>()._tileStatus;
            if (tileType == JDGroundClass.tiles.tilled)
            {
                GameObject newSeeds = (GameObject)Instantiate(seeds, tile.transform.position, Quaternion.identity);
                newSeeds.GetComponent<JDPlantClass>().seedType = playerCon.invCon.currItem.GetComponent<JDPlantClass>().seedType;
                newSeeds.GetComponent<JDPlantClass>().plantedTile = tile;
                tile.GetComponent<JDGroundClass>().occupiedWith = newSeeds;
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
            JDGroundClass.tiles tileType = tile.GetComponent<JDGroundClass>()._tileStatus;
            if (playerCon.invCon.currItem.GetComponent<Tool>().toolType == Tool.ToolType.hoe)
            {
                tile.GetComponent<JDGroundClass>()._tileStatus = JDGroundClass.tiles.tilled;
            }
            if (playerCon.invCon.currItem.GetComponent<Tool>().toolType == Tool.ToolType.wateringCan && tileType == JDGroundClass.tiles.tilled)
            {
                tile.GetComponent<JDGroundClass>()._tileStatus = JDGroundClass.tiles.watered;
            }
        }
    }

    /// <summary>
    /// this function checks each tile
    /// if the tiles has something planted in it
    /// it tells the plant to grow if it can
    /// then resets every watered tile to it's default status
    /// so the player must water the tile again the next day
    /// </summary>
    void CheckPlantsGrow()
    {
        GameObject[,] groundArray = grid.GetComponent<JDGroundSpawner>().groundArray;

        foreach (GameObject ground in groundArray)
        {
            if (ground.GetComponentInChildren<JDGroundClass>().occupiedWith != null)
            {
                //the plant checks if the tile has been watered, if so it will grow                
                ground.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Grow();
                if (ground.GetComponentInChildren<JDGroundClass>()._tileStatus == JDGroundClass.tiles.watered)
                {
                    ground.GetComponentInChildren<JDGroundClass>()._tileStatus = JDGroundClass.tiles.tilled;
                }
            }

        }
    }
}

