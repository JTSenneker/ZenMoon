using UnityEngine;
using System.Collections;

public class JDFenceClass : MonoBehaviour {

    public float health = 4;
    //the tile this plant is 'planted' on
    public GameObject plantedTile;
    // Use this for initialization
    void Start () {
        //health gets % better based on zen, double at max zen
        health *= JDStaticVariables.zenTotal * .01f + 1; 
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0) Destroy();
	}

    public void TakeDamage()
    {
        health--;
    }
    void Destroy()
    {
        plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.dirt;
        plantedTile.GetComponentInChildren<JDGroundClass>().occupiedWith = null;
        Destroy(gameObject);
    }
}
