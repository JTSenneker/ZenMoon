using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    Animator anim;
    SpriteRenderer sr;
    public Sprite forwardSprite;
    public Sprite backwardSprite;
    public Sprite sideSprite;

    float walkV = 0;
    float walkH = 0;
    public float velocity = 1;
    float speed = 0;
    direction movement = direction.forward;

    enum direction
    {
        forward,
        backward,
        left,
        right
    }

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        walkV = Input.GetAxisRaw("Vertical") * -1;
        walkH = Input.GetAxisRaw("Horizontal");
        print(walkH);
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
        CheckInput();
	}

    private void MoveHorizontal()
    {
        speed = walkH * velocity * Time.deltaTime;
        transform.position += new Vector3(speed, 0, 0);
    }

    private void MoveVertical()
    {
        speed = walkV * velocity * Time.deltaTime;
        transform.position += new Vector3(0, speed, 0);
    }

    private void CheckInput()
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
