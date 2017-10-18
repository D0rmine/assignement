using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : Unit {

    private int time;
    public Camera cam;
    public float speed;
    public GameObject shield;
    public int enrgieMax;
    public Image DeathScreen;
    public int energie;
    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        DeathScreen.fillAmount = 0;
        attack_speed = 0;
        Hp = HpMax;
        energie = enrgieMax;
        body = GetComponent<Rigidbody2D>();
        time = 0;
    }

    // Update is called once per frame
    protected override void Death()
    {
        DeathScreen.fillAmount = 1;
        base.Death();
    }

    protected override void Update () {
        base.Update();
        body.velocity = Vector3.zero;
        Move();
        if (time >= 30)
            time = 0;
        else
            time++;
        lookAt();
        Shield();
        Shoot();
        EnergyRecovery();
    }

    void EnergyRecovery()
    {
        if (energie < enrgieMax && time == 0)
            energie++;
    }

    void lookAt()
    {
        Vector3 mousse2DPosition = Input.mousePosition;

        RaycastHit hit;

        Vector3 mouseClickDirection = cam.GetComponent<Camera>().ScreenToWorldPoint(mousse2DPosition);
        Ray ray = cam.ScreenPointToRay(mousse2DPosition);
        if (Input.mousePresent && Physics.Raycast(ray, out hit))
        {
            transform.up = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
        }
    }

    void Shield()
    {
        if (energie >= 50)
        {
            Vector3 mousse2DPosition = Input.mousePosition;
            RaycastHit hit;

            Vector3 mouseClickDirection = cam.ScreenToWorldPoint(mousse2DPosition);
            Ray ray = cam.ScreenPointToRay(mousse2DPosition);
                if (Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out hit))
            {
                shield.transform.position = new Vector3(hit.point.x, hit.point.y, -1);
                shield.transform.up = new Vector2(transform.position.x - shield.transform.position.x, transform.position.y - shield.transform.position.y);
                Instantiate(shield);
                energie -= 50;
            }
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (attack_speed == 0)
            {
                bullet.transform.position = this.transform.position + this.transform.up * 3/2;
                bullet.GetComponent<BulletControler>().velocity = transform.up * bullet_speed;
                bullet.GetComponent<BulletControler>().tag_attack = "Enemy";
                Instantiate(bullet);
            }
            if (attack_speed == 3)
            {
                attack_speed = 0;
            }
            else
                attack_speed++;
        }
        else
        {
            attack_speed = 0;
        }
    }

    public void Move()
    {
        double move = speed * 1;
        if (Input.GetKey(KeyCode.Z))
        {
            body.velocity = new Vector2(body.velocity.x + 0, body.velocity.y + (float)move);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = new Vector2(body.velocity.x + 0, body.velocity.y + -(float)move);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(body.velocity.x + (float)move, body.velocity.y + 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            body.velocity = new Vector2(body.velocity.x + -(float)move, body.velocity.y + 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && energie >= 20)
        {
            energie -= 20;
            transform.position = new Vector3(transform.position.x + body.velocity.x / 6, transform.position.y + body.velocity.y / 6, transform.position.z);
        }
    }
}
