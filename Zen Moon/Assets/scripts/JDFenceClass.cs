using UnityEngine;
using System.Collections;

/// <summary>
/// this class holds all the data for a created fence object
/// </summary>
public class JDFenceClass : MonoBehaviour {

    /// <summary>
    /// the object's health
    /// </summary>
    public float health = 4;
    /// <summary>
    ///the tile this plant is 'planted' on 
    /// </summary>
    /// 
    public GameObject plantedTile;
    // Use this for initialization
    void Start () {
        //health gets % better based on zen, double at max zen
        health *= JDStaticVariables.zenTotal * .01f + 1; 
	}
	
    /// <summary>
    /// we check the fence's health, if it's low, run the destroy function
    /// </summary>
	void Update () {

        if (health <= 0) Destroy();
	}

    /// <summary>
    /// this function is called when something damages the fence
    /// decreases the fence's health
    /// </summary>
    public void TakeDamage()
    {
        health--;
    }
    /// <summary>
    /// this function is called when we need to destroy the fence
    /// sets the tile it's standing on back to dirt and tells it it's empty
    /// </summary>
    void Destroy()
    {
        plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus = JDGroundClass.tiles.dirt;
        plantedTile.GetComponentInChildren<JDGroundClass>().occupiedWith = null;
        Destroy(gameObject);
    }
}
