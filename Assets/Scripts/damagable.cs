using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    public UnityEvent<int, int> healthChanged;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField]
    private int _Health = 100;

    public int Health
    {
        get { return _Health; }
        set { _Health = value;
            healthChanged?.Invoke(_Health, maxHealth);

            if (_Health <=0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _IsAlive = true;
   
    [SerializeField]
    private bool isInvincible = false;

   

    public float invincibltyTime = 0.25f;
    private float timeSinceHit = 0;

    public bool IsAlive
    {
        get
        {
            return _IsAlive;
        }
        private set
        {
            _IsAlive = value;
            animator.SetBool(animationString.isAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }

    public bool lockVelocity
    {
        get
        {
            return animator.GetBool(animationString.lockVelocity);
        }
        set
        {
            animator.SetBool(animationString.lockVelocity, value);
        }

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isInvincible)
        {
            if (timeSinceHit > invincibltyTime)
            {
                //remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit+= Time.deltaTime;
        }
    }
    public bool Hit(int damage,Vector2 knockBack)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(animationString.hitTrigger);
            lockVelocity = true;
            damagableHit?.Invoke(damage, knockBack);
            charecterEvents.charecterDamaged.Invoke(gameObject, damage);

            return true;
        }
       return false;
    }
    public bool Heal(int healthRestore)
    {
        if (IsAlive&& Health< maxHealth)
        {
            int maxHeal=Mathf.Max(maxHealth-Health, 0);
            int actualHeal=Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            charecterEvents.charecterHealed(gameObject,actualHeal);
            return true;
        }
        return false;
    }
}
