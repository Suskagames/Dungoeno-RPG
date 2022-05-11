using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 orginalSize;
    
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        orginalSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed , 0);

        //Swap Sprite direction,wether you're going pight or left

        if (moveDelta.x > 0)
        {
            transform.localScale = orginalSize;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(orginalSize.x * -1,orginalSize.y,orginalSize.z);
        }

        //Add push vectro, if any 
        moveDelta += pushDirection;

        //Reduce push force evert frame.based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecovertSpeed);


        //Make sure we can move in this direction, by casting a box there first,if the box returns null, we'ar free to move 
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make this thing move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //Make this thing move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
