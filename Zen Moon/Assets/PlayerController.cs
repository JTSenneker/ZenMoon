using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    Animator anim;
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
	}
	
	// Update is called once per frame
	void Update () 
    {
        walkV = Input.GetAxis("Vertical");
        walkH = Input.GetAxis("Horizontal");
        anim.SetFloat("verticalSpeed", walkV);
        anim.SetFloat("horizontalSpeed", walkH);

        if (walkV < 0)
        {
            anim.SetBool("isWalkingForward", true);
        }
        if (walkV > 0)
        {
            anim.SetBool("isWalkingBackward", true);
        }
        if (walkV == 0)
        {
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackward", false);
        }
        if (walkH < 0)
        {
            anim.SetBool("isWalkingLeft", true);
        }
        if (walkH > 0)
        {
            anim.SetBool("isWalkingRight", true);
        }
        if (walkH == 0)
        {
            anim.SetBool("isWalkingLeft", false);
            anim.SetBool("isWalkingRight", false);
        }

        if (walkV != 0)
        {
            speed = walkV * velocity * Time.deltaTime;
            transform.position += new Vector3(0, speed, 0);
        }
        else if (walkH != 0)
        {
            speed = walkH * velocity * Time.deltaTime;
            transform.position += new Vector3(speed, 0, 0);
        }
        
	}
}
