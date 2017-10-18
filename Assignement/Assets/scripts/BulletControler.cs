using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour {

    Rigidbody2D body;
    public Vector2 velocity;
    public string tag_attack;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        transform.up = velocity;
        body.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == tag_attack)
        {
            coll.gameObject.GetComponent<Unit>().Hp -= 1;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
	}
}
