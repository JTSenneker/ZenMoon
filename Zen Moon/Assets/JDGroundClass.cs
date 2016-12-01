using UnityEngine;
using System.Collections;

public class JDGroundClass : MonoBehaviour {

    //the current status of this tile, tilled, seeded, etc
   public JDStaticVariables.tiles _tileStatus;
    //what this tile has placed/planted ontop of it
    public GameObject occupiedWith;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//this is where we update the sprite according to it's status
    //unless we use the animator for that
	}
}
