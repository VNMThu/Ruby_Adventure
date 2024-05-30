using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSoldier : Enemy
{
  private readonly int _isAttacking = Animator.StringToHash("IsAttacking");
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private ParticleSystem appearEffect;
    [SerializeField] private float attackRate;
    [SerializeField] private float bulletSpeed;
    private readonly int _isDeath = Animator.StringToHash("IsDeath");
    private readonly int _isRunning = Animator.StringToHash("IsRunning");
    [SerializeField] private RangeSoldierBullet bulletPrefab;
    private bool _faceLeft = true;
    private Coroutine _attackCoroutine;
    private float attackCountDown;
    protected override void OnEnable()
    {
        base.OnEnable();
        _faceLeft = true;
        animator.SetBool(_isRunning, false);
        ObjectsPoolManager.SpawnObject(appearEffect.gameObject, transform.position, Quaternion.identity,
            ObjectsPoolManager.PoolType.ParticleSystem);
        StartMoving();
        attackCountDown = 1/attackRate;
    }

    protected override void StartMoving()
    {
        StartCoroutine(MoveToRuby());
    }

    private void Update()
    {
        //Return if death
        if (!IsAlive) return;
        FlipToFaceRuby();

        Vector3 rubyPosition = GameManager.Instance.RubyPosition;
        if (Vector2.Distance(transform.position, rubyPosition) <= attackRange && !IsAttacking)
        {
            StartAttack();
        }
        else if (Vector2.Distance(transform.position, rubyPosition) > attackRange && IsAttacking)
        {
            StopAttack();
        }
    }

    public void FireBullet()
    {
        //Create projectile
        RangeSoldierBullet bullet = ObjectsPoolManager.SpawnObject(bulletPrefab.gameObject,
            attackPoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<RangeSoldierBullet>();

        //Find direction to ruby
        Vector2 direction = GameManager.Instance.RubyPosition - transform.position;
        direction = direction.normalized;
        
        //Launch it
        bullet.Launch(direction, bulletSpeed, damage,0);
    }

    private IEnumerator MoveToRuby()
    {
        while (IsAlive)
        {
            if (!IsAttacking)
            {
                animator.SetBool(_isRunning, true);

                Vector3 rubyPosition =GameManager.Instance.RubyPosition;
                //speed
                float step = speed * Time.deltaTime;

                // move towards the ruby location
                transform.position = Vector2.MoveTowards(transform.position, rubyPosition, step);
            }
            yield return null;
        }
    }

    private void FlipToFaceRuby()
    {
        //Rotate depend on ruby position
        if ( GameManager.Instance.RubyPosition.x >= transform.position.x && _faceLeft)
        {
                transform.RotateAround(transform.position, transform.up, 180f);
                _faceLeft = false;
        }
        else if(GameManager.Instance.RubyPosition.x < transform.position.x && !_faceLeft)
        {
                transform.RotateAround(transform.position, transform.up, -180f);
                _faceLeft = true;
        }
    }


    protected override void StartAttack()
    {
        //Stop moving
        animator.SetBool(_isRunning, false);
        
        //Start coroutine attack

        if (IsAttacking)
        {
            return;
        }
        
        IsAttacking = true;
        _attackCoroutine = StartCoroutine(C_Attack());
    }

    private IEnumerator C_Attack()
    {
        while (IsAttacking)
        {
            attackCountDown -= Time.deltaTime;
            yield return null;
            
            //Not yet
            if (!(attackCountDown <= 0)) continue;
            
            animator.SetTrigger(_isAttacking);
            
            attackCountDown = 1 / attackRate;
        }
    }
    

    protected override void StopAttack()
    {
        //Stop attack
        IsAttacking = false;
        StopCoroutine(_attackCoroutine);
        
        //Start moving again
        animator.SetBool(_isRunning, true);
        
        
    }

    protected override void Death()
    {
        base.Death();
        //Start death animation
        animator.SetTrigger(_isDeath);
    }

    //Get call in animation
    public void ReturnToPool()
    {
        ObjectsPoolManager.ReturnObjectToPool(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
