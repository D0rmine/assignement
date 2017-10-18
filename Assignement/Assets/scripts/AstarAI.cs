using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

public class AstarAI : MonoBehaviour
{
    //The point to move to
    public Transform target;

    private Seeker seeker;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 2;
    Rigidbody2D body;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            body.velocity = Vector2.zero;
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = new Vector3(path.vectorPath[currentWaypoint].x - transform.position.x, path.vectorPath[currentWaypoint].y - transform.position.y, 0).normalized;
        transform.up = new Vector2(path.vectorPath[currentWaypoint].x - transform.position.x, path.vectorPath[currentWaypoint].y - transform.position.y);
        body.velocity = new Vector2(path.vectorPath[currentWaypoint].x - transform.position.x, path.vectorPath[currentWaypoint].y - transform.position.y);
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance * 3 && path.vectorPath.Capacity > currentWaypoint + 1)
        {
            body.velocity = body.velocity + new Vector2(path.vectorPath[currentWaypoint + 1].x - transform.position.x, path.vectorPath[currentWaypoint + 1].y - transform.position.y);
            transform.up = body.velocity;
        }

        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
