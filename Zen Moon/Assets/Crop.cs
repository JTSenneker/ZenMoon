using UnityEngine;
using System.Collections;

public class Crop : MonoBehaviour 
{

	public enum CropType
    {
        daikon,
        leek,
        corn,
        rice
    }

    public CropType cropType;
    bool isSelected = false;

    void Update()
    {
        if(isSelected)
        {
            Vector3 PlayerPos = GameObject.Find("Player").transform.position;
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + .5f, PlayerPos.z);
        }
    }

    public void Selected()
    {
        isSelected = true;
    }

    public void NotSelected()
    {
        isSelected = false;
    }
}
