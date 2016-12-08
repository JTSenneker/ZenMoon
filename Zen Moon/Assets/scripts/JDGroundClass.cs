using UnityEngine;
using System.Collections;

public class JDGroundClass : MonoBehaviour {

    //the current status of this tile, tilled, seeded, etc
    public tiles _tileStatus;
    //what this tile has placed/planted ontop of it
    public GameObject occupiedWith;
    /// <summary>
    /// The sprite renderer of the tile
    /// </summary>
    SpriteRenderer spriteRend;
    /// <summary>
    /// The dirt sprite
    /// </summary>
    public Sprite dirt;
    /// <summary>
    /// The tilled dirt sprite
    /// </summary>
    public Sprite tilled;
    /// <summary>
    /// The watered dirt sprite
    /// </summary>
    public Sprite watered;

    /// <summary>
    /// Types of possible dirt tiles
    /// </summary>
    public enum tiles
    {
        dirt,
        tilled,
        watered
    }

    /// <summary>
    /// Might be easier if you put your enumerator into this class for the dirt tiles also a bit better organized.
    /// If you do this remember to change _tileStatus to just tiles rather than JDStaticVariables.tiles
    /// Also, I would suggest getting rid of seeded and rock tiles because seeds will be plased above tiles and for now rocks aren't being used
    /// !!!If you do this make sure you fix the Game Controller script as well to make a working game!!!
    /// </summary>

	// Use this for initialization
	void Start ()
    {
        spriteRend = GetComponent<SpriteRenderer>();
	}
	
	/// <summary>
    /// Switches between sprites when the player interacts with the ground
    /// </summary>
	void Update ()
    {
        switch (_tileStatus)
        {
            case tiles.dirt:
                spriteRend.sprite = dirt;
                break;
            case tiles.tilled:
                spriteRend.sprite = tilled;
                break;
            case tiles.watered:
                spriteRend.sprite = watered;
                break;
        }
    }
}
