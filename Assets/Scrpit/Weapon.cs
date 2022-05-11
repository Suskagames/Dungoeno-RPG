using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Dameg struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f ,2.2f, 2.5f, 3f , 3.2f, 3.6f ,4f};

    //Upgrade
    public int wepaonLevel = 0;
    public SpriteRenderer spriteRendered;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRendered = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRendered = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time -lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }    
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;
            //Create a new damego object, then we will send it to the fightr we have hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[wepaonLevel],
                origin = transform.position,
                pushForce = pushForce[wepaonLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }    

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        wepaonLevel++;
        spriteRendered.sprite = GameMenager.instance.weponSprites[wepaonLevel];

        //Change stats %%
    }
    public void SerWeaponLevel(int level)
    {
        wepaonLevel = level;
        spriteRendered.sprite = GameMenager.instance.weponSprites[wepaonLevel];
    }
}
