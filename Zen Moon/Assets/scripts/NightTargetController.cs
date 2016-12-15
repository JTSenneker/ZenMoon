using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the night target
/// </summary>
public class NightTargetController : MonoBehaviour
{
    /// <summary>
    /// The collision collider
    /// </summary>
    CollisionController colCon;
    /// <summary>
    /// The vertical direction of the target
    /// </summary>
    float walkV = 0;
    /// <summary>
    /// The horizontal direction of the target
    /// </summary>
    float walkH = 0;
    /// <summary>
    /// The velocity of the target
    /// </summary>
    public float velocity = 5;
    /// <summary>
    /// How fast the target goes per frame
    /// </summary>
    Vector3 speed = new Vector3(0, 0, 0);
    /// <summary>
    /// The direction the target is facing
    /// </summary>
    direction movement = direction.forward;

    /// <summary>
    /// The directions that the target can face
    /// </summary>
    enum direction
    {
        forward,
        backward,
        left,
        right
    }

    /// <summary>
    /// sets the collision collider for the target
    /// </summary>
    void Start()
    {
        colCon = GetComponent<CollisionController>();
    }

    /// <summary>
    /// Figures out the input of the user and applies it to the target, and changes the animation conditions depending on the input.
    /// </summary>
    void Update () 
    {
            walkV = Input.GetAxisRaw("Vertical");
            walkH = Input.GetAxisRaw("Horizontal");
            MovePlayer();
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
                MoveVertical();
                break;
            case direction.backward:
                MoveVertical();
                break;
            case direction.left:
                MoveHorizontal();
                break;
            case direction.right:
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
}
