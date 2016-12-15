using UnityEngine;
using System.Collections;

/// <summary>
/// this class holds all the information for the ground tiles
/// </summary>
public class JDGroundClass : MonoBehaviour {
    /// <summary>
    ///the current status of this tile, tilled, seeded, etc 
    /// </summary>
    public tiles _tileStatus;
    /// <summary>
    ///what this tile has placed/planted ontop of it 
    /// </summary>
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
    /// set the sprite renderer
    /// </summary>
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
    /// <summary>
    /// this function returns a string based on the tile passed in
    /// for the purpose of save/load
    /// </summary>
    /// <param name="type"> the 'tiles' type we want converted to string</param>
    /// <returns>returns a string corressponding to the tilestatus requested</returns>
    string GetGroundTile(tiles type) {
        if (type == tiles.dirt) return "dirt";
        if (type == tiles.tilled) return "tilled";
        if (type == tiles.watered) return "watered";

        return null;
    }

    /// <summary>
    /// Loads the status of the ground when the game is being continued
    /// </summary>
    /// <param name="type"></param>
    public void LoadStatus(string type)
    {
        switch(type)
        {
            case "dirt":
                _tileStatus = tiles.dirt;
                break;
            case "tilled":
                _tileStatus = tiles.tilled;
                break;
            case "watered":
                _tileStatus = tiles.watered;
                break;
        }
    }
}
