using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(touchingDirection),typeof(damagable))]
public class playerControler : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;

    Vector2 moveInput;
    touchingDirection touchingDir;
    damagable Damagable;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDir.IsOnWall)
                {
                    if (touchingDir.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //movement locked
                return 0;
            }
       
        }
    }
    
    [SerializeField]
    private bool _isMoving=false;
    public bool IsMoving {get
        {
            return _isMoving;
        }
        set
        {
           _isMoving = value;
            animator.SetBool(animationString.isMoving, _isMoving);
        }
    }
    [SerializeField]
    private bool _isRunning=false;

    public bool IsRunning { get
        {
            return _isRunning;
        }
        set 
        {
            _isRunning = value;
            animator.SetBool(animationString.isRunning, _isRunning);
        }
    }
    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; }private set {
            //flip only if value is new
            if (_isFacingRight != value) 
            {
                //flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        } 
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(animationString.canMove);
        }

    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(animationString.isAlive);
        }
    }

  

    Rigidbody2D rb;

    Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDir=GetComponent<touchingDirection>();
       Damagable=GetComponent<damagable>();
    }
  
    private void FixedUpdate()
    {
        if(!Damagable.lockVelocity)
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(animationString.yVelocity, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput=context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
        }
        else
        {
            IsMoving=false;
        }
       
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //face the right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0&& IsFacingRight)
        {
            //face the left 
            IsFacingRight=false;

        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started&&touchingDir.IsGrounded&&CanMove)
        {
            animator.SetTrigger(animationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(animationString.attackTrigger);
        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(animationString.rangedAttackTrigger);
        }
    }
    public void OnHit(int damage,Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }
}
