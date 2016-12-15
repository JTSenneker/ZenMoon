using UnityEngine;
using UnityEngine.UI;
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
    /// The selling screen
    /// </summary>
    public GameObject merchantScreen;
    /// <summary>
    /// A reference to the seeds object
    /// </summary>
    public GameObject seeds;
    /// <summary>
    /// A reference to the fence object
    /// </summary>
    public GameObject fence;
    /// <summary>
    /// A reference sprite of the sun
    /// </summary>
    public Sprite sun;
    /// <summary>
    /// A reference sprite of the moon
    /// </summary>
    public Sprite moon;
    /// <summary>
    /// A reference of day panel
    /// </summary>
    public Image dayPanel;
    /// <summary>
    /// The starting money of the player
    /// </summary>
    public int startingMoney = 500;
    /// <summary>
    /// A reference of the current merchant
    /// </summary>
    GameObject merchantRef;

    /// <summary>
    /// A reference to the grid spawner
    /// </summary>
    JDGroundSpawner gridCon;
    /// <summary>
    /// A reference to the player controller
    /// </summary>
    PlayerController playerCon;
    /// <summary>
    /// The seeds planted in the game
    /// </summary>
    ArrayList plantedSeeds = new ArrayList();
    /// <summary>
    /// The placed fences in the game
    /// </summary>
    ArrayList placedFences = new ArrayList();
    /// <summary>
    /// The day count
    /// </summary>
    int dCount = 0;
    /// <summary>
    /// The night count
    /// </summary>
    int nCount = 0;

    /// <summary>
    /// Sets the grid controller and the player controller and sets objects for the save mechanic
    /// </summary>
    void Start()
    {
        gridCon = grid.GetComponent<JDGroundSpawner>();
        playerCon = player.GetComponent<PlayerController>();
        JDStaticVariables.moneyTotal = startingMoney;

        SaveLoadController.setPlayer(player);
        SaveLoadController.setInvCon(playerCon.invCon);
        SaveLoadController.setGrid(gridCon);
        SaveLoadController.setGameControl(this);

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
            if (JDMouseTargeting.GetMouseTarget() != null && JDMouseTargeting.GetMouseTarget().tag == merchant.tag)
            {
                playerCon.inMenu = true;
                Time.timeScale = 0;
                merchantRef.GetComponent<Merchant>().openSell(merchantScreen);
            }
            else if (JDMouseTargeting.GetMouseTarget() != null && JDMouseTargeting.GetMouseTarget().GetComponent<JDGroundClass>().occupiedWith != null)
            {
                Pick();
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

        if (GetComponent<DayNightController>().isDay && dCount != JDStaticVariables.dayCount)
        {
            transform.GetComponentInParent<CameraController>().switchDay();
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
            playerCon.isNight = false;
            print(dayPanel.transform.GetChild(0));
            dayPanel.transform.GetChild(0).GetComponent<Image>().sprite = sun;
            if (dCount == 0 || merchantArrived())
            {
                Vector2 placement = new Vector2(0, gridCon.mapHeight);
                merchantRef = (GameObject)Instantiate(merchant, placement, Quaternion.identity);
            }
            dCount = JDStaticVariables.dayCount;
        }

        else if (!GetComponent<DayNightController>().isDay && nCount != JDStaticVariables.dayCount)
        {
            transform.GetComponentInParent<CameraController>().switchNight();
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z - 1);
            playerCon.isNight = true;
            CheckPlantsGrow();
            dayPanel.transform.GetChild(0).GetComponent<Image>().sprite = moon;
            SaveLoadController.setSeeds(plantedSeeds.ToArray() as GameObject[]);
            SaveLoadController.setFences(placedFences.ToArray() as GameObject[]);
            //SaveLoadController.Save();
            if (nCount == 0 || merchantArrived())
            {
                Destroy(merchantRef);
                merchantRef = null;
            }
            nCount = JDStaticVariables.dayCount;
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
    /// Allows the player to pick up an item from the grid
    /// </summary>
    void Pick()
    {
        GameObject tile = JDMouseTargeting.target;
        GameObject itemOccupying = tile.GetComponent<JDGroundClass>().occupiedWith;

        if (tile != null && itemOccupying != null)
        {
            if (itemOccupying.tag == "seeds")
            {
                //logic for dealing with picking a plant
            }
            else if (itemOccupying.tag == "crop")
            {
                Destroy(itemOccupying);
            }
            else if (itemOccupying.tag == "fence")
            {
                Destroy(itemOccupying);
            }
        }
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
            if(newItem.tag == "fence")
            {
                placedFences.Add(newItem);
            }
            tile.GetComponent<JDGroundClass>().occupiedWith = newItem;
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
                plantedSeeds.Add(newSeeds);
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
                ground.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Grow();
                if (ground.GetComponentInChildren<JDGroundClass>()._tileStatus == JDGroundClass.tiles.watered)
                {
                    ground.GetComponentInChildren<JDGroundClass>()._tileStatus = JDGroundClass.tiles.tilled;
                }
            }

        }
    }

    /// <summary>
    /// Adds seeds from save file
    /// </summary>
    /// <param name="seedInfo">The seeds being added back in</param>
    public void AddSeeds(float[,] seedInfo)
    {
        for (int i = 0; i < seedInfo.Length; i++)
        {
            Vector2 placement = new Vector2(seedInfo[0, i], seedInfo[1, i]);
            GameObject newSeeds = (GameObject)Instantiate(seeds, placement, Quaternion.identity);
            newSeeds.GetComponent<JDPlantClass>().seedType = (JDPlantClass.SeedType)seedInfo[2, i];
            //do something about the stage
            plantedSeeds.Add(newSeeds);
        }  
    }

    /// <summary>
    /// Adds fences from save file
    /// </summary>
    /// <param name="seedInfo">The fences being added back in</param>
    public void AddFences(float[,] fenceInfo)
    {
        for (int i = 0; i < fenceInfo.Length; i++)
        {
            Vector2 placement = new Vector2(fenceInfo[0, i], fenceInfo[1, i]);
            GameObject newFence = (GameObject)Instantiate(fence, placement, Quaternion.identity);
            //change fence health...
            placedFences.Add(newFence);
        }
    }

    /// <summary>
    /// Returns the item that is currently on the tile
    /// </summary>
    /// <returns>The gameobject on a tile</returns>
    public static GameObject Occupied()
    {
        GameObject tile = JDMouseTargeting.target;
        if (tile != null)
        {
            return tile.GetComponent<JDGroundClass>().occupiedWith;
        }
        return null;
    }
}

