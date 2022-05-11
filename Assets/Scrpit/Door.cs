using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite CloseDoor;
    public Sprite OpenDoor;
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    public static Door instance;


    protected void Start()
    {
        GetComponentInParent<SpriteRenderer>().sprite = CloseDoor;
        boxCollider = GetComponent<BoxCollider2D>();

    }

    protected virtual void Update()
    {
        //Colision work
        boxCollider.OverlapCollider(filter, hits);
        x = ;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            OnCollide(hits[i]);
            //The array is not clened up , so we do it ourself
            hits[i] = null;

        }
    }
    
    protected virtual void OnCollide(Collider2D coll)
    {
        
        if (coll.name == "Player" &&   )
        {
            GetComponentInParent<SpriteRenderer>().sprite = OpenDoor;
            Debug.Log("Open door");
            GetComponentInParent<BoxCollider2D>().enabled = false;
            boxCollider.enabled = false;
        }
        if (coll.name != "Player")
            return;
    }

    protected virtual void ChangeGetKey(int i)
    {

    }
}
