using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Fighter
{
    private float colldown = 6.0f;
    private float lastShout;
    protected Vector3 moveDelta;
    public float speed = 6.5f;
    public float height = 0.04f;
    public List<string> deathText = new List<string>();
    public int x;
    Vector3 pos;

    protected void Start()
    {
        pos = transform.position;
        //lastShout = -colldown;
    }

    private void FixedUpdate()
    {
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    protected override void Death()
    {
        GameMenager.instance.SendMessage("AngryGhost", 1);
        Destroy(gameObject);
        GameMenager.instance.ShowText(deathText[Random.Range(0,9)] ,20 , Color.red, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, 2.0f);
    }

}
