using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameMenager.instance.pesos += pesosAmount;
            //Debug.Log("Grand" + pesosAmount + "pesos");
            GameMenager.instance.ShowText("+" + pesosAmount + " pesos!", 20, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }
    }
}

