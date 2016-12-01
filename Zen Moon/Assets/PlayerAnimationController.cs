using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour 
{
    /// <summary>
    /// The animator component on the player
    /// </summary>
    Animator anim;
    /// <summary>
    /// Whether or not the animation has ended.
    /// </summary>
    public bool animFinished = true;

    /// <summary>
    /// Sets the animator component of the player
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Triggers the animation for the switch inventory animation
    /// </summary>
    public void SwitchInventory()
    {
        anim.SetBool("facingBack", false);
        anim.SetBool("facingSide", false);
        anim.SetBool("swappingInventory", true);
        animFinished = false;
    }

    /// <summary>
    /// Triggers the animation for using a tool
    /// </summary>
    /// <param name="var">The parameter boolean for the currently selected tool</param>
    public void UseTool(string var)
    {
        anim.SetBool(var, true);
    }

    /// <summary>
    /// Triggers the animation for picking up an item
    /// </summary>
    public void PickUp()
    {
        anim.SetBool("isPicking", true);
        animFinished = false;
    }
    
    /// <summary>
    /// Triggers the walking animation 
    /// </summary>
    /// <param name="var">The direction float for the animator parameter</param>
    /// <param name="input">The input value</param>
    public void SetWalkDir(string var, float input)
    {
        anim.SetFloat(var, input);

        if (var == "verticalSpeed")
        {
            if (input > 0)
            {
                anim.SetBool("facingSide", false);
                anim.SetBool("facingBack", true);
            }
            else if (input < 0)
            {
                anim.SetBool("facingBack", false);
                anim.SetBool("facingSide", false);
            }
        }
        else if (var == "horizontalSpeed")
        {
            anim.SetBool("facingBack", false);
            anim.SetBool("facingSide", true);
        }
    }

    /// <summary>
    /// Triggers a transition when the inventory swap animation ends
    /// </summary>
    public void InventorySwapEnd()
    {
        anim.SetBool("swappingInventory", false);
        animFinished = true;
        GetComponent<PlayerController>().ItemHide();
    }

    /// <summary>
    /// Triggers a transition when the hoe animation ends
    /// </summary>
    public void HoeAnimEnd()
    {
        anim.SetBool("isHoe", false);
        animFinished = true;
    }

    /// <summary>
    /// Triggers a transition when the watering can animation ends
    /// </summary>
    public void WaterAnimEnd()
    {
        anim.SetBool("isWater", false);
        animFinished = true;
    }

    /// <summary>
    /// Triggers a transition when the picking up animation ends
    /// </summary>
    public void PickingAnimEnd()
    {
        anim.SetBool("isPicking", false);
        animFinished = true;
    }
}
