using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KnightEnemy;

[RequireComponent(typeof(Rigidbody2D),typeof(touchingDirection),typeof(damagable))]
public class KnightEnemy : MonoBehaviour
{
    public float walkAccileration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Animator animator;

    Rigidbody2D rb;
    touchingDirection touchingDir;
    damagable Damagable;
    public enum WalkableDirection {Right, Left};

    private WalkableDirection _walkableDirection;
    public Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection walkDirection
    {
        get
        {
            return _walkableDirection;
        }
        set
        {
            if(_walkableDirection != value)
            {
                //Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }
    public bool _hasTarget=false;
    public bool HasTarget { get
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

    public float AttackCoolDown
    {
        get
        {
         return   animator.GetFloat(animationString.attackCooldown);
        }
        private set 
        { 
        animator.SetFloat(animationString.attackCooldown,MathF.Max(value,0));
        } 
    }

    private void Awake()
    {
       rb= GetComponent<Rigidbody2D>();
        touchingDir=GetComponent<touchingDirection>();
        animator= GetComponent<Animator>();
        Damagable=GetComponent<damagable>();
    }
    private void Update()
    {
        HasTarget=attackZone.detectedColliders.Count > 0;
        if (AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
       
    }
    private void FixedUpdate()
    {
        if (touchingDir.IsGrounded && touchingDir.IsOnWall)
        {
            FlipDirection();
        }
        if(!Damagable.lockVelocity)
        {
            if (CanMove&&touchingDir.IsGrounded)
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAccileration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }
   
    private void FlipDirection()
    {
        if (walkDirection == WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }else if(walkDirection == WalkableDirection.Left)
        {
            walkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }
    public void OnHit(int damage,Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
    public void OnCliffDetected()
    {
        if(touchingDir.IsGrounded)
        {
            FlipDirection();
        }
    }
  
}
