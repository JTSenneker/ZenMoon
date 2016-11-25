using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the player's movements and animation
/// </summary>
public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// The animator component on the player
    /// </summary>
    Animator anim;
    /// <summary>
    /// The sprite renderer component on the player
    /// </summary>
    SpriteRenderer sr;
    /// <summary>
    /// The player's Inventory Controller
    /// </summary>
    InventoryController invCon;

    /// <summary>
    /// The vertical direction of the player
    /// </summary>
    float walkV = 0;
    /// <summary>
    /// The horizontal direction of the player
    /// </summary>
    float walkH = 0;
    /// <summary>
    /// The velocity of the player
    /// </summary>
    public float velocity = 1;
    /// <summary>
    /// How fast the player goes per frame
    /// </summary>
    float speed = 0;
    /// <summary>
    /// The direction the player is facing
    /// </summary>
    direction movement = direction.forward;

    /// <summary>
    /// The directions that the player can face
    /// </summary>
    enum direction
    {
        forward,
        backward,
        left,
        right
    }

	/// <summary>
	/// Initializes variables
	/// </summary>
	void Start () 
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        invCon = GetComponent<InventoryController>();
	}
	
	/// <summary>
	/// Figures out the input of the user and applies it to the player, and changes the animation conditions depending on the input.
	/// </summary>
	void Update () 
    {
        walkV = Input.GetAxisRaw("Vertical");
        walkH = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Inventory Scroll") != 0)
        {
            anim.SetBool("facingBack", false);
            anim.SetBool("facingSide", false);
            anim.SetBool("swappingInventory", true);
            invCon.animFinished = false;
            invCon.ShowItem();
        }
        else
        {
            MovePlayer();
        }

        CheckDirection();
	}

    /// <summary>
    /// Moves the player according to input and triggers animation
    /// </summary>
    private void MovePlayer()
    {
        anim.SetFloat("verticalSpeed", walkV);
        anim.SetFloat("horizontalSpeed", Mathf.Abs(walkH));

        switch (movement)
        {
            case direction.forward:
                anim.SetBool("facingBack", false);
                anim.SetBool("facingSide", false);
                MoveVertical();
                break;
            case direction.backward:
                anim.SetBool("facingSide", false);
                anim.SetBool("facingBack", true);
                MoveVertical();
                break;
            case direction.left:
                anim.SetBool("facingBack", false);
                anim.SetBool("facingSide", true);
                sr.flipX = true;
                MoveHorizontal();
                break;
            case direction.right:
                anim.SetBool("facingBack", false);
                anim.SetBool("facingSide", true);
                sr.flipX = false;
                MoveHorizontal();
                break;
        }
    }

    /// <summary>
    /// Moves the player horizontally
    /// </summary>
    private void MoveHorizontal()
    {
        speed = walkH * velocity * Time.deltaTime;
        transform.position += new Vector3(speed, 0, 0);
    }

    /// <summary>
    /// Moves the player vertically
    /// </summary>
    private void MoveVertical()
    {
        speed = walkV * velocity * Time.deltaTime;
        transform.position += new Vector3(0, speed, 0);
    }

    /// <summary>
    /// Changes the direction of the player based on the input
    /// </summary>
    private void CheckDirection()
    {
        if (walkV < 0)
        {
            movement = direction.forward;
        }
        else if (walkV > 0)
        {
            movement = direction.backward;
        }
        else if (walkH < 0)
        {
            movement = direction.left;
        }
        else if (walkH > 0)
        {
            movement = direction.right;
        }
    }

    /// <summary>
    /// Triggers a transition when the inventory swap animation ends
    /// </summary>
    public void InventorySwapEnd()
    {
        anim.SetBool("swappingInventory", false);
        invCon.animFinished = true;
        invCon.HideItem();
    }
}
