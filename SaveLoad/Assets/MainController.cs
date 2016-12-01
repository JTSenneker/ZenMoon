using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class MainController : MonoBehaviour
{
    public Slider slider;

	// Use this for initialization
	void Start ()
    {
        LoadVolume();
        Save();
	}
	
	// Update is called once per frame
	void LoadVolume ()
    {
	    if (PlayerPrefs.HasKey("Volume"))
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
	}
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
        PlayerPrefs.Save();
    }

    public void Save()
    {
        SaveGameData data = new SaveGameData();
        data.health = 42;
        data.boss1Beat = true;
        data.boss2Beat = false;
        data.position = new Vector2(100, 200);

        FileStream fs = null;

        try
        {
            fs = new FileStream("save1.dagd", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data); 
        }
        catch (Exception e)
        {

        }
        finally
        {
            if(fs != null) fs.Close();
        }
        
    }

    public void Load()
    {
        FileStream fs = null;
        BinaryFormatter bf = new BinaryFormatter();

        try
        {
            fs = new FileStream("save1.dagd", FileMode.Open);
            
            SaveGameData data = (SaveGameData)bf.Deserialize(fs);
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (fs != null) fs.Close();
        }
    }
}

[Serializable]
class SaveGameData: ISerializable
{
    public int health;
    public bool boss1Beat;
    public bool boss2Beat;
    public Vector2 position = new Vector2;

    public SaveGameData()
    {

    }

    public SaveGameData(ISerializationInfo info, StreamingContext ctxt)
    {
        health = info.GetInt32("HP");
        position.x = info.GetSingle("posx");
        position.y = info.Getsingle("posy");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext stxt)
    {

    }
}
