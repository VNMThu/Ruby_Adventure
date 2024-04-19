using UnityEngine;

public class Gun : BaseWeapon
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected SpriteRenderer _muzzleFlash;
    
    
    private const string animationCondition = "IsShooting"; 
    private readonly int Shoot = Animator.StringToHash(animationCondition);
    private readonly int IsStanding = Animator.StringToHash("IsStanding");
    private SpriteRenderer _spriteRenderer;
    
    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //Set firing speed
        _animator.speed = fireRate;
        //Turn off muzzle flash
        _muzzleFlash.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        //Start animation
        _animator.SetBool(Shoot,true);
    }

    protected virtual void FireBullet()
    {
        //This is call inside animation clip
        Debug.Log("Firing Bullet");
    }

    public override void StopAttack()
    {
        //Stop firing
        if (_animator.GetBool(Shoot))
        {
            _animator.SetBool(Shoot, false);
        }
        _animator.SetTrigger(IsStanding);    
    }

    public override void FlipSprite()
    {
        _spriteRenderer.flipY = !_spriteRenderer.flipY;
        _muzzleFlash.flipY = !_muzzleFlash.flipY;

    }
}
