using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class RangedWeapon : Weapon
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer muzzleFlash;
    [Header("projectile")]
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Transform spawnProjectilePoint;

    
    private const string AnimationCondition = "IsShooting"; 
    private readonly int _shoot = Animator.StringToHash(AnimationCondition);
    private readonly int _isStanding = Animator.StringToHash("IsStanding");
    private SpriteRenderer _spriteRenderer;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;
    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //Default stand
        animator.SetBool(_isStanding,true);    

        
        //Turn off muzzle flash
        muzzleFlash.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        //Start animation - Over Call
        _isAttacking = true;
        _attackCoroutine = StartCoroutine(C_AttackCoroutine());
    }

    private IEnumerator C_AttackCoroutine()
    {
        while (_isAttacking)
        {
            animator.SetTrigger(_shoot);
            yield return new WaitForSeconds(1 / fireRate);
        }
    } 

    //This is call inside animation clip
    public virtual void FireProjectile()
    {
        //Create projectile
        Projectile projectile = ObjectsPoolManager.SpawnObject(projectilePrefab.gameObject, spawnProjectilePoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<Projectile>();
        
        //Launch it
        projectile.Launch(transform.right,40);
    }

    public override void StopAttack()
    {
        //Stop firing
        if (!_isAttacking) return;
        _isAttacking = false;
        StopCoroutine(_attackCoroutine);
    }

    public override void FlipSprite()
    {
        _spriteRenderer.flipY = !_spriteRenderer.flipY;
        muzzleFlash.flipY = !muzzleFlash.flipY;

    }
}
