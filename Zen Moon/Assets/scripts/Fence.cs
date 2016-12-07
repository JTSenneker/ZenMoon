using UnityEngine;
using System.Collections;

/// <summary>
/// Describes a fence
/// </summary>
public class Fence : MonoBehaviour
{ 
    /// <summary>
    /// If the crop is currently selected or not
    /// </summary>
    bool isSelected = false;

    /// <summary>
    /// If the crop is selected move with the player
    /// </summary>
    void Update()
    {
        if (isSelected)
        {
            Vector3 PlayerPos = GameObject.Find("Player").transform.position;
            transform.position = new Vector3(PlayerPos.x, PlayerPos.y + .5f, PlayerPos.z);
        }
    }

    /// <summary>
    /// Sets isSelected to true
    /// </summary>
    public void Selected()
    {
        isSelected = true;
    }

    /// <summary>
    /// Sets isSelected to false
    /// </summary>
    public void NotSelected()
    {
        isSelected = false;
    }
}
