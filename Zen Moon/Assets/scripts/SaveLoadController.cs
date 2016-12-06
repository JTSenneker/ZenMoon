using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveLoadController 
{
    public static bool save1 = false;
    public static bool save2 = false;
    public static bool contin = false;
    static GameObject player;
    static InventoryController invCon;

    public static void setPlayer(GameObject obj)
    {
        player = obj;
    }

    public static void setInvCon(InventoryController inv)
    {
        invCon = inv;
    }

    public static void Save()
    {
        SaveGameData data = new SaveGameData();
        if (player != null)
        {
            data.playerPosX = player.transform.position.x;
            data.playerPosY = player.transform.position.y;
            data.playerPosZ = player.transform.position.z;
        }
        if (invCon != null)
        {
            data.playerInventory = invCon.GetInventory();
        }
        
        FileStream fs = null;
        try
        {
            fs = new FileStream("save1.zenMoon", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (fs != null) fs.Close();
        }
    }

    public static void Load()
    {
        FileStream fs = null;
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            fs = new FileStream("save1.zenMoon", FileMode.Open);

            SaveGameData data = (SaveGameData)bf.Deserialize(fs);
            player.transform.position = new Vector3(data.playerPosX, data.playerPosY, data.playerPosZ);
            invCon.SetInventory(data.playerInventory);
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

[Serializable]
class SaveGameData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public ArrayList playerInventory;
}
