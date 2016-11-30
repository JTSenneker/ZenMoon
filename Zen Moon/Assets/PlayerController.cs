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
    /// Whether or not the animation has ended.
    /// </summary>
    bool animFinished = true;
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
        float input = Input.GetAxisRaw("Inventory Scroll");
        float action = Input.GetAxis("Action");
        float pick = Input.GetAxis("Pick");

        if (animFinished)
        {
            if (input != 0)
            {
                anim.SetBool("facingBack", false);
                anim.SetBool("facingSide", false);
                anim.SetBool("swappingInventory", true);
                invCon.MoveInventory(input);
                animFinished = false;
                invCon.ShowItem();
            }
            else if (action != 0 && invCon.currItem != null)
            {
                if (invCon.currItem.tag == "hoe")
                {
                    anim.SetBool("isHoe", true);
                    //Change dirt to tilled dirt
                }
                if (invCon.currItem.tag == "wateringCan")
                {
                    anim.SetBool("isWater", true);
                    //Change dirt to watered dirt
                }
                if (invCon.currItem.tag == "daikonSeeds")
                {
                    //play animation
                    //change dirt to seeded dirt
                }
                if (invCon.currItem.tag == "leekSeeds")
                {
                    //play animation
                    //change dirt to seeded dirt
                }
                if (invCon.currItem.tag == "cornSeeds")
                {//play animation
                    //change dirt to seeded dirt

                }
                if (invCon.currItem.tag == "riceSeeds")
                {
                    //play animation
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
            else if (pick != 0) //and there's something there and there's nothing in the player's hands
            {
                Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, 1);
                if(hit.collider != null)
                {
                    anim.SetBool("isPicking", true);
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
        animFinished = true;
        invCon.HideItem();
    }

    /// <summary>
    /// Triggers a transition when the hoe animation ends
    /// </summary>
    public void HoeAnimEnd()
    {
        anim.SetBool("isHoe", false);
    }

    /// <summary>
    /// Triggers a transition when the watering can animation ends
    /// </summary>
    public void WaterAnimEnd()
    {
        anim.SetBool("isWater", false);
    }

    /// <summary>
    /// Triggers a transition when the picking up animation ends
    /// </summary>
    public void PickingAnimEnd()
    {
        anim.SetBool("isPicking", false);
    }
}
