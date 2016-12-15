using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the player's movements and animation
/// </summary>
public class PlayerController : MonoBehaviour 
{
    /// <summary>
    /// The sprite renderer component on the player
    /// </summary>
    SpriteRenderer sr;
    /// <summary>
    /// The player's Inventory Controller
    /// </summary>
    public InventoryController invCon;
    /// <summary>
    /// The player's animation controller
    /// </summary>
    PlayerAnimationController animCon;
    /// <summary>
    /// The player's collision controller
    /// </summary>
    CollisionController colCon;

    /// <summary>
    /// Whether or not the player is interacting with the ground
    /// </summary>
    public bool isInteracting = false;

    /// <summary>
    /// The vertical direction of the player
    /// </summary>
    float walkV = 0;
    /// <summary>
    /// The horizontal direction of the player
    /// </summary>
    float walkH = 0;
    /// <summary>
    /// The switching inventory input
    /// </summary>
    float switchInventory = 0;
    /// <summary>
    /// The action input
    /// </summary>
    float action = 0;
    /// <summary>
    /// The pick up input
    /// </summary>
    float pick = 0;
    /// <summary>
    /// The velocity of the player
    /// </summary>
    public float velocity = 1;
    /// <summary>
    /// How fast the player goes per frame
    /// </summary>
    Vector3 speed = new Vector3(0, 0, 0);
    /// <summary>
    /// The direction the player is facing
    /// </summary>
    direction movement = direction.forward;
    public bool inMenu = false;

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
        animCon = GetComponent<PlayerAnimationController>();
        sr = GetComponent<SpriteRenderer>();
        invCon = GetComponent<InventoryController>();
        colCon = GetComponent<CollisionController>();
	}
	
	/// <summary>
	/// Figures out the input of the user and applies it to the player, and changes the animation conditions depending on the input.
	/// </summary>
	void Update () 
    {
        if (inMenu)
        {
            walkV = 0;
            walkH = 0;
            switchInventory = 0;
            action = 0;
            pick = 0;
        }
        else
        {
            walkV = Input.GetAxisRaw("Vertical");
            walkH = Input.GetAxisRaw("Horizontal");
            switchInventory = Input.GetAxisRaw("Inventory Scroll");
            action = Input.GetAxis("Action");
            pick = Input.GetAxis("Pick");
        }
        
        
        if (animCon.animFinished)
        {
            if (switchInventory != 0)
            {
                walkV = 0;
                walkH = 0;
                movement = direction.forward;
                invCon.MoveInventory(switchInventory);
                animCon.SwitchInventory();
                invCon.ShowItem();
            }
            else if (action != 0 && invCon.currItem != null)
            {
                if (invCon.currItem.tag == "tool")
                {
                    animCon.UseTool(invCon.currItem.GetComponent<Tool>().AnimVar());
                    isInteracting = true;
                }
                else if (invCon.currItem.tag == "seeds")
                {
                    invCon.RemoveItem(invCon.currItem);
                    animCon.UseSeeds();
                    isInteracting = true;
                }
                else if(invCon.currItem.tag == "crop" || invCon.currItem.tag == "fence")
                {
                    animCon.Throw();
                    isInteracting = true;
                }
            }
            else if (pick != 0) //and there's something there and there's nothing in the player's hands
            {
                //Will need changing when merged and able to look at grid system
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, 1);
                if (hit.collider != null)
                {
                    animCon.PickUp();
                    invCon.AddItem(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                MovePlayer();
                CheckDirection();
            }
        }
	}

    /// <summary>
    /// Moves the player according to input and triggers animation
    /// </summary>
    private void MovePlayer()
    {
        switch (movement)
        {
            case direction.forward:
                animCon.SetWalkDir("verticalSpeed", walkV);
                MoveVertical();
                break;
            case direction.backward:
                animCon.SetWalkDir("verticalSpeed", walkV);
                MoveVertical();
                break;
            case direction.left:
                animCon.SetWalkDir("horizontalSpeed", Mathf.Abs(walkH));
                sr.flipX = true;
                MoveHorizontal();
                break;
            case direction.right:
                animCon.SetWalkDir("horizontalSpeed", Mathf.Abs(walkH));
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
        speed.x = walkH * velocity * Time.deltaTime;
        colCon.Move(ref speed);
        transform.position += new Vector3(speed.x, 0, 0);
    }

    /// <summary>
    /// Moves the player vertically
    /// </summary>
    private void MoveVertical()
    {
        speed.y = walkV * velocity * Time.deltaTime;
        colCon.Move(ref speed);
        transform.position += new Vector3(0, speed.y, 0);
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
    /// Hides inventory item
    /// </summary>
    public void ItemHide()
    {
        if (invCon.currItem.tag != "crop" && invCon.currItem.tag != "fence")
        {
            invCon.HideItem();
            animCon.withItem = false;
        }
        if (invCon.currItem.tag == "crop" || invCon.currItem.tag == "fence")
        {
            animCon.withItem = true;
        }
    }

    /// <summary>
    /// Controls throwing the crop
    /// </summary>
    public void ItemThrow()
    {
        invCon.HideItem();
        invCon.RemoveItem(invCon.currItem);
        if(invCon.currItem != null)
        {
            invCon.ShowItem();
        }
        else
        {
            animCon.ThrowAnimEnd();
        }
    }

    /// <summary>
    /// Adds a bunch of items into the inventory
    /// </summary>
    /// <param name="items">The items being added</param>
    public void AddInventoryItems(ArrayList items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            print(i);
            invCon.AddItem((GameObject)items[i]);
        }
    }
}
