using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Key instance;
    public int y = 0;
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //Colision work
        boxCollider.OverlapCollider(filter, hits);
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
        if (coll.name == "Player")
            y = 1;
            //Destroy(gameObject);
            //gameObject.SendMessage("ChangeGetKey", 1);
        return;
    }
}
