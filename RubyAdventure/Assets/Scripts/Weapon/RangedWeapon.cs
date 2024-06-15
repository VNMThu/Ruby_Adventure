using System.Collections;
using JSAM;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class RangedWeapon : Weapon
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer muzzleFlash;

    [Header("projectile")] [SerializeField]
    protected RubyGunBullet rubyGunBulletPrefab;

    [SerializeField] protected Transform spawnProjectilePoint;


    private const string AnimationCondition = "IsShooting";
    private readonly int _shoot = Animator.StringToHash(AnimationCondition);
    private readonly int _isStanding = Animator.StringToHash("IsStanding");
    private SpriteRenderer _spriteRenderer;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;
    protected override void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //Default stand
        animator.SetBool(_isStanding, true);


        //Turn off muzzle flash
        muzzleFlash.gameObject.SetActive(false);
        
        //Reset attackCountDown
        attackCountDown = 1 / currentAttribute.FireRate;
        
        base.OnEnable();

    }

    public override void Attack()
    {
        //Start animation - Over Call
        if (_isAttacking)
        {
            return;
        }

        _isAttacking = true;
        _attackCoroutine = StartCoroutine(C_AttackCoroutine());
    }

    private IEnumerator C_AttackCoroutine()
    {
        while (_isAttacking)
        {
            attackCountDown -= Time.deltaTime;
            yield return null;
            
            //Not yet
            if (!(attackCountDown <= 0)) continue;
            
            //Reach wait Time
            
            //Shoot
            animator.SetTrigger(_shoot);
            
            //Reset count down
            attackCountDown = 1 / currentAttribute.FireRate;
        }
    }

    //This is call inside animation clip
    public virtual void FireProjectile()
    {
        //Create projectile
        RubyGunBullet rubyGunBullet = ObjectsPoolManager.SpawnObject(rubyGunBulletPrefab.gameObject,
            spawnProjectilePoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<RubyGunBullet>();

        //Sound Effect
        AudioManager.PlaySound(soundEffect);
        
        //Launch it
        rubyGunBullet.Launch(transform.right, 40, currentAttribute.DamagePerAttack, forcePushBack);
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