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
    InventoryController invCon;
    /// <summary>
    /// The player's animation controller
    /// </summary>
    PlayerAnimationController animCon;

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
        animCon = GetComponent<PlayerAnimationController>();
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
        switchInventory = Input.GetAxisRaw("Inventory Scroll");
        action = Input.GetAxis("Action");
        pick = Input.GetAxis("Pick");

        if (animCon.animFinished)
        {
            if (switchInventory != 0 && !invCon.isShowing)
            {
                animCon.SwitchInventory();
                invCon.MoveInventory(switchInventory);
                invCon.ShowItem();
            }
            #region needs fixing or refactoring or both
            else if (action != 0 && invCon.currItem != null)
            {
                if (invCon.currItem.tag == "tool")
                {
                    animCon.UseTool(invCon.currItem.GetComponent<Tool>().AnimVar());
                    //Change the dirt accordingly
                }
                if (invCon.currItem.tag == "seeds")
                {
                    
                    //change dirt to seeded dirt
                }
                if (invCon.currItem.tag == "diakon")
                {
                    //play animation
                    //delete crop
                }
                if (invCon.currItem.tag == "leek")
                {
                    //play animation
                    //delete crop
                }
                if (invCon.currItem.tag == "corn")
                {
                    //play animation
                    //revert to earlier stage
                }
                if (invCon.currItem.tag == "rice")
                {
                    //play animation
                    //revert to earlier stage
                }
                if (invCon.currItem.tag == "fence")
                {
                    //play animation
                    //place fence
                }
            }
            #endregion
            else if (pick != 0) //and there's something there and there's nothing in the player's hands
            {
                //Will need changing when merged and able to look at grid system
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, 1);
                if(hit.collider != null)
                {
                    animCon.PickUp();
                    invCon.AddItem(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                MovePlayer();
            }
        }

        CheckDirection();
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
    /// Hides inventory item
    /// </summary>
    public void ItemHide()
    {
        invCon.HideItem();
    }
}
