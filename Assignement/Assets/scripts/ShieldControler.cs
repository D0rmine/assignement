using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControler : MonoBehaviour {

    public int lifeTime;
    private int TimeAlive;

	// Use this for initialization
	void Start () {
        TimeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (TimeAlive > lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            TimeAlive++;
        }
	}
}
