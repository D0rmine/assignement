using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergieBarControler : MonoBehaviour {

    Image fillImg;
    float timeAmt = 10;
    public PlayerControler player;

    // Use this for initialization
    void Start()
    {
        fillImg = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float energie = player.energie;
        float energieMax = player.enrgieMax;
        fillImg.fillAmount = energie / energieMax;
    }
}
