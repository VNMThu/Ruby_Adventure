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
    
    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //Set firing speed
        animator.speed = fireRate;
        //Turn off muzzle flash
        muzzleFlash.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        //Start animation
        animator.SetBool(_shoot,true);
    }

    //This is call inside animation clip
    public virtual void FireProjectile()
    {
        //Create projectile
        Projectile projectile = ObjectsPoolManager.SpawnObject(projectilePrefab.gameObject, spawnProjectilePoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<Projectile>();
        
        //Launch it

        Vector2 lookDirection = (transform.rotation.eulerAngles.z * Vector2.one).normalized;
        projectile.Launch(transform.right,40);
    }

    public override void StopAttack()
    {
        //Stop firing
        if (animator.GetBool(_shoot))
        {
            animator.SetBool(_shoot, false);
        }
        animator.SetTrigger(_isStanding);    
    }

    public override void FlipSprite()
    {
        _spriteRenderer.flipY = !_spriteRenderer.flipY;
        muzzleFlash.flipY = !muzzleFlash.flipY;

    }
}
