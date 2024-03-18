using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEye : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;

    Animator animator;
    Rigidbody2D rb;
    damagable Damagable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(animationString.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(animationString.canMove);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Damagable=GetComponent<damagable>();
    }
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget=biteDetectionZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (Damagable. IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            //dead flys falls to ground
            rb.gravityScale = 2f;
            rb.velocity=new Vector2(0,rb.velocity.y);      }
    }

    //fly between waypoints and loop back to start when final waypoint is reached
    private void Flight()
    {
        //fly to next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //check if we have reached the waypoint alredy
        float distance=Vector2.Distance(nextWaypoint.position,transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        //see if we need to switch waypoints
        if(distance<=waypointReachedDistance)
        {
            //switch to next waypoint
            waypointNum++;

            if(waypointNum>=waypoints.Count)
            {
                //loop back to original waypoint
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 locaScale= transform.localScale;

        if (transform.localScale.x > 0)
        {
            //facing the right
            if (rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locaScale.x, locaScale.y, locaScale.z);

            }
        }
        else
        {
            //facing left
            if (rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locaScale.x, locaScale.y, locaScale.z);

            }
        }
    }
}
