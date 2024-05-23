using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
public class RangedWeapon : Weapon
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer muzzleFlash;

    [FormerlySerializedAs("rubyProjectilePrefab")] [FormerlySerializedAs("projectilePrefab")] [Header("projectile")] [SerializeField]
    protected RubyGunBullet rubyGunBulletPrefab;

    [SerializeField] protected Transform spawnProjectilePoint;


    private const string AnimationCondition = "IsShooting";
    private readonly int _shoot = Animator.StringToHash(AnimationCondition);
    private readonly int _isStanding = Animator.StringToHash("IsStanding");
    private SpriteRenderer _spriteRenderer;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    protected void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //Default stand
        animator.SetBool(_isStanding, true);


        //Turn off muzzle flash
        muzzleFlash.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        //Start animation - Over Call
        if (_isAttacking)
        {
            Debug.Log("Call Stop Attack");
            StopCoroutine(_attackCoroutine);
        }

        _isAttacking = true;
        _attackCoroutine = StartCoroutine(C_AttackCoroutine());
    }

    private IEnumerator C_AttackCoroutine()
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(1 / fireRate);
            animator.SetTrigger(_shoot);
        }
    }

    //This is call inside animation clip
    public virtual void FireProjectile()
    {
        //Create projectile
        RubyGunBullet rubyGunBullet = ObjectsPoolManager.SpawnObject(rubyGunBulletPrefab.gameObject,
            spawnProjectilePoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<RubyGunBullet>();

        //Launch it
        rubyGunBullet.Launch(transform.right, 40, damagePerAttack, forcePushBack);
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