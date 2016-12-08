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
            //int[,] ground = grid.map;
            //for (int i = 0; i < ground.Length; i++)
            //{
            //    //for (int j = 0; j < ground.GetLength(i); j++)
            //    //{
            //    //    //For this to work at all I need either a multidimensional array using
            //    //    //a gameobject array or I need a way to get the tile from the JDGroundSpawner class!!

            //    //    //get the type of each tile
            //    //    //make a switch statement to switch each type to a string
            //    //}
            //}
        }

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
    //public string[,] groundType;
}
