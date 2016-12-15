using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/// <summary>
/// Save and Loads the game data
/// </summary>
public static class SaveLoadController 
{
    /// <summary>
    /// If the save 1 position has been selected
    /// </summary>
    public static bool save1 = false;
    /// <summary>
    /// If the save 2 position has been selected
    /// </summary>
    public static bool save2 = false;
    /// <summary>
    /// If this is loading a previous save 
    /// </summary>
    public static bool contin = false;
    /// <summary>
    /// The player 
    /// </summary>
    static GameObject player;
    /// <summary>
    /// The inventory of the player
    /// </summary>
    static InventoryController invCon;
    /// <summary>
    /// The grid controller
    /// </summary>
    static JDGroundSpawner grid;
    /// <summary>
    /// The placed seeds in the game
    /// </summary>
    static GameObject[] seeds;
    /// <summary>
    /// The placed fences in the game
    /// </summary>
    static GameObject[] fences;
    /// <summary>
    /// The game controller
    /// </summary>
    static GameController gameCon;

    /// <summary>
    /// Sets the player
    /// </summary>
    /// <param name="obj">The player gameobject</param>
    public static void setPlayer(GameObject obj)
    {
        player = obj;
    }

    /// <summary>
    /// Sets the player inventory controller
    /// </summary>
    /// <param name="inv">The inventory controller</param>
    public static void setInvCon(InventoryController inv)
    {
        invCon = inv;
    }

    /// <summary>
    /// Sets the grid
    /// </summary>
    /// <param name="grd">The grid</param>
    public static void setGrid(JDGroundSpawner grd)
    {
        grid = grd;
    }

    /// <summary>
    /// Sets all the seeds
    /// </summary>
    /// <param name="sds"></param>
    public static void setSeeds(GameObject[] sds)
    {
        seeds = sds;
    }

    /// <summary>
    /// Sets all the fences
    /// </summary>
    /// <param name="fncs">The fences being set</param>
    public static void setFences(GameObject[] fncs)
    {
        fences = fncs;
    }

    /// <summary>
    /// Sets the game controller
    /// </summary>
    /// <param name="controller">The game controller</param>
    public static void setGameControl(GameController controller)
    {
        gameCon = controller;
    }

    /// <summary>
    /// Saves the current game
    /// </summary>
    public static void Save()
    {
        SaveGameData data = new SaveGameData();
        string path = null;

        if (player != null)
        {
            data.playerPosX = player.transform.position.x;
            data.playerPosY = player.transform.position.y;
            data.playerPosZ = player.transform.position.z;
        }
        if (invCon != null)
        {
            data.playerInventory = invCon.GetInventory();
            data.playerInventoryCount = invCon.GetInventoryCount();
        }
        if (grid != null)
        {
            GameObject[,] ground = grid.groundArray;
            for (int y = 0; y < grid.mapHeight; y++)
            {
                for (int x = 0; x < grid.mapWidth; x++)
                {
                    //data.groundType[y, x] = ground[y, x].GetComponent<JDGroundClass>().GetType();
                }
            }
        }
        if (seeds.Length != 0)
        {
            for (int i = 0; i < seeds.Length; i++)
            {
                data.seedsInfo[0, i] = seeds[i].transform.position.x;
                data.seedsInfo[1, i] = seeds[i].transform.position.y;
                data.seedsInfo[2, i] = (int)seeds[i].GetComponent<JDPlantClass>().seedType;
                //also save the stage somehow...
            }
        }
        if (fences.Length != 0)
        {
            for (int i = 0; i < fences.Length; i++)
            {
                data.fencesInfo[0, i] = fences[i].transform.position.x;
                data.fencesInfo[1, i] = fences[i].transform.position.y;
                //save health...
            }
        }
        data.dayCount = JDStaticVariables.dayCount;
        data.money = JDStaticVariables.moneyTotal;
        data.zen = JDStaticVariables.zenTotal;

        if(save1)
        {
            path = "save1.zenMoon";
        }
        else if (save2)
        {
            path = "save2.zenMoon";
        }

        FileStream fs = null;
        try
        {
            fs = new FileStream(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            if (fs != null) fs.Close();
        }
    }

    /// <summary>
    /// Loads the selected game
    /// </summary>
    public static void Load()
    {
        FileStream fs = null;
        BinaryFormatter bf = new BinaryFormatter();
        string path = null;

        if (save1)
        {
            path = "save1.zenMoon";
        }
        else if (save2)
        {
            path = "save2.zenMoon";
        }

        try
        {
            fs = new FileStream(path, FileMode.Open);

            SaveGameData data = (SaveGameData)bf.Deserialize(fs);
            player.transform.position = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
            invCon.SetInventory(data.playerInventory);
            invCon.SetInventoryCount(data.playerInventoryCount);
            grid.ChangeTerrain(data.groundType);
            gameCon.AddSeeds(data.seedsInfo);
            gameCon.AddFences(data.fencesInfo);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        finally
        {
            if (fs != null) fs.Close();
        }
    }
}

/// <summary>
/// The game data being saved
/// </summary>
[Serializable]
class SaveGameData
{
    /// <summary>
    /// The player x position
    /// </summary>
    public float playerPosX;
    /// <summary>
    /// The player y position
    /// </summary>
    public float playerPosY;
    /// <summary>
    /// The player z position
    /// </summary>
    public float playerPosZ;
    /// <summary>
    /// The player inventory in strings
    /// </summary>
    public ArrayList playerInventory;
    /// <summary>
    /// The player inventory count
    /// </summary>
    public ArrayList playerInventoryCount;
    /// <summary>
    /// The ground type of each tile
    /// </summary>
    public string[,] groundType;
    /// <summary>
    /// The information of all the seeds planted
    /// </summary>
    public float[,] seedsInfo;
    /// <summary>
    /// The placement of all of the fences that were placed
    /// </summary>
    public float[,] fencesInfo;
    /// <summary>
    /// The day count
    /// </summary>
    public int dayCount;
    /// <summary>
    /// The money
    /// </summary>
    public int money;
    /// <summary>
    /// The zen
    /// </summary>
    public int zen;
}
