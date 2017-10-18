using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public int HpMax;
    public int Hp;
    public GameObject bullet;
    public int bullet_speed;
    protected int attack_speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	virtual protected void Update () {
        if (Hp <= 0)
        {
            Death();
        }
    }

    virtual protected void Death()
    {
        Destroy(this.gameObject);
    }
}
