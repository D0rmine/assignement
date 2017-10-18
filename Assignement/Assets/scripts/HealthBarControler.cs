using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControler : MonoBehaviour {

    Image fillImg;
    float timeAmt = 10;
    public PlayerControler player;

	// Use this for initialization
	void Start () {
        fillImg = this.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        float hp = player.Hp;
        float hpMax = player.HpMax;
        fillImg.fillAmount = hp / hpMax;
    }
}

