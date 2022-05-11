using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experiance 
    public int xpValue = 1;

    //Logic 
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameMenager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //Is the player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
            }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            if(hits[i].tag == "Fighter" && hits[i].name =="Player")
            {
                collidingWithPlayer=true;
            }
            //The array is not clened up , so we do it ourself
            hits[i] = null;
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameMenager.instance.GrandXp(xpValue);
        GameMenager.instance.ShowText("+" + xpValue + "xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        GameMenager.instance.ShowText("+" + xpValue + "xp" , 25, Color.green, transform.position, Vector3.zero, 0.5f);
    }
}
