using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;


public class EnemyInfanteryControler : Unit {

    //The point to move to
    public GameObject Player;

    private Seeker seeker;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 2;
    Rigidbody2D body;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
    int time;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public void Start()
    {
        attack_speed = 0;
        time = 0;
        body = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Player != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.up);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + Vector3.Normalize(Player.transform.position - transform.position), Player.transform.position - transform.position);

            if (hit.collider.gameObject == Player)
            {
                body.velocity = Vector2.zero;
                Attack();
            }
            else if (hit2.collider.gameObject == Player)
            {
                body.velocity = Vector2.zero;
                Rotate();
            }
            else
                Path();
        }
    }

    void Rotate()
    {
        transform.up = Player.transform.position - transform.position;
    }

    void Attack()
    {
        if (attack_speed == 0)
            {
                bullet.transform.position = this.transform.position + this.transform.up * 3 / 2;
                bullet.GetComponent<BulletControler>().velocity = transform.up * bullet_speed;
            bullet.GetComponent<BulletControler>().tag_attack = "Player";
            Instantiate(bullet);
            }
        if (attack_speed == 3)
            {
                attack_speed = 0;
            }
        else
            attack_speed++;
    }

    void Path()
    {
        if (path == null)
        {
            seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            body.velocity = Vector2.zero;
            path = null;
            return;
        }

        //Direction to the next waypoint
        if (time >= 300)
        {
            seeker.StartPath(transform.position, Player.transform.position, OnPathComplete);
            time = 0;
        }
        else
            time++;
        Vector3 dir = new Vector3(path.vectorPath[currentWaypoint].x - transform.position.x, path.vectorPath[currentWaypoint].y - transform.position.y, 0).normalized;
        transform.up = new Vector2(path.vectorPath[currentWaypoint].x - transform.position.x, path.vectorPath[currentWaypoint].y - transform.position.y);
        body.velocity = new Vector2((path.vectorPath[currentWaypoint].x - transform.position.x) * speed, (path.vectorPath[currentWaypoint].y - transform.position.y) * speed);
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
