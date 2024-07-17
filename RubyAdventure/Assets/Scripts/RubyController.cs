using System;
using System.Collections;
using JSAM;
using UnityEngine;
using UnityEngine.Serialization;

public class RubyController : MonoBehaviour
{

    //Health
    [Header("Max Health")] [SerializeField]
    private int maxHealth = 5;

    public int MaxHealth => maxHealth;

    public int Health { get; private set; }

    //Move speed
    [Header("Move Speed")] [SerializeField]
    private float movingSpeed = 5.0f;

    //Dash
    [Header("Dash time and speed")] [SerializeField]
    private float dashTime = 0.5f;

    [SerializeField] private float dashSpeed = 0.5f;
    [SerializeField] private float dashCoolDown = 5f;

    //Dash
    [Header("Shockwave cooldown, range, damage")] 
    [SerializeField] private float shockwaveRange = 0.5f;
    [SerializeField] private float shockwaveCoolDown = 3f;
    [SerializeField] private int shockwaveDamage = 2;
    [SerializeField] private int shockwaveForce = 4;

    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private GameObject shockwaveEffect;
    
    private bool _isDashing;

    [Header("Weapon And Hands")] [SerializeField]
    private Weapon[] weapons;
    [SerializeField] private RubyHand[] handSlot;
    private int _numberOfActiveHands;
    [Header("Others")]
    //Time invincible after talking damage
    [SerializeField]
    private float timeInvincible = 0.5f;

    private bool _isInvincible;
    private float _invincibleTimerWhenHurt;

    private Rigidbody2D _rigidBody;


    private Animator _animator;
    private Vector2 _lookDirection = new(0, -1);


    public ParticleSystem getHitEffect;
    public ParticleSystem pickHealthEffect;
    [SerializeField] private ParticleSystem deathEffect;

    private RubyControl _rubyControl;
    private readonly int _launch = Animator.StringToHash("Launch");
    private readonly int _hit = Animator.StringToHash("Hit");
    private readonly int _lookX = Animator.StringToHash("Look X");
    private readonly int _lookY = Animator.StringToHash("Look Y");
    private readonly int _speed = Animator.StringToHash("Speed");

    private Action<object> _onWeaponUnlockPref;
    private Action<object> _onRevivePref;

    private SpriteRenderer _spriteRenderer;

    private bool _isDeath;
    
    // Start is called before the first frame update
    private void Start()
    {
        Health = maxHealth;
        _isDeath = false;
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    

    private void Awake()
    {
        _rubyControl = new RubyControl();
        _animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        _rubyControl.Enable();
        _onWeaponUnlockPref = param => OnWeaponUnlock((WeaponType)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnWeaponUnlock,_onWeaponUnlockPref);
        
        _onRevivePref = _ => OnRevive();
        EventDispatcher.Instance.RegisterListener(EventID.OnRubyRevive,_onRevivePref);
    }
    
    

    private void OnDisable()
    {
        _rubyControl.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Health <= 0 && !_isDeath)
        {
            PlayerDeath();
            _isDeath = true;
        }

        //If dashing then don't do anything else
        if (!_isDashing)
        {
            Vector2 move = _rubyControl.Player.Move.ReadValue<Vector2>();
            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                _lookDirection.Set(move.x, move.y);
                _lookDirection.Normalize();
            }

            _animator.SetFloat(_lookX, _lookDirection.x);
            _animator.SetFloat(_lookY, _lookDirection.y);
            _animator.SetFloat(_speed, move.magnitude);

            if (_isInvincible)
            {
                _invincibleTimerWhenHurt -= Time.deltaTime;
                if (_invincibleTimerWhenHurt < 0)
                {
                    _isInvincible = false;
                }
            }

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Launch"))
            {
                if (_rubyControl.Player.Dash.triggered)
                {
                    // Dash();
                    StartCoroutine(C_Dash());
                }
            }
        }

        if (_rubyControl.Player.Shockwave.triggered)
        {
            Debug.Log("DO Trigger");
            DoShockwave();
        }

        //Talk with NPC
        
        // if (!_rubyControl.Player.Talk.triggered) return;
        // RaycastHit2D hit = Physics2D.Raycast(_rigidBody.position + Vector2.up * 0.2f, _lookDirection, 1.5f,
        //     LayerMask.GetMask("NPC"));
        //
        // if (hit.collider == null) return;
        //
        // NPC character = hit.collider.GetComponent<NPC>();
        //
        // if (character != null)
        // {
        //     character.DisplayDialog();
        // }
    }

    private void FixedUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Launch")) return;
        Vector2 position = _rigidBody.position;
        position.x += movingSpeed * _rubyControl.Player.Move.ReadValue<Vector2>().x * Time.deltaTime;
        position.y += movingSpeed * _rubyControl.Player.Move.ReadValue<Vector2>().y * Time.deltaTime;
        _rigidBody.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        switch (amount)
        {
            case < 0 when _isDashing:
                return;
            case < 0:
            {
                _animator.SetTrigger(_hit);

                if (_isInvincible)
                {
                    return;
                }

                _isInvincible = true;
                _invincibleTimerWhenHurt = timeInvincible;

                ObjectsPoolManager.SpawnObject(getHitEffect.gameObject, _rigidBody.position + Vector2.up * 0.5f,
                    Quaternion.identity,
                    ObjectsPoolManager.PoolType.ParticleSystem);
                AudioManager.PlaySound(AudioLibrarySounds.RubyGetHit);

                break;
            }
            case > 0:
                ObjectsPoolManager.SpawnObject(pickHealthEffect.gameObject, _rigidBody.position + Vector2.up * 0.5f,
                    Quaternion.identity,
                    ObjectsPoolManager.PoolType.ParticleSystem);
                break;
        }

        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        EventDispatcher
            .Instance.PostEvent(EventID.OnHealthChange, Health);
    }

    private IEnumerator C_Dash()
    {
        //Animation and audio
        _animator.SetTrigger(_launch);
        AudioManager.PlaySound(AudioLibrarySounds.RubyDash);
        _isDashing = true;
        EventDispatcher.Instance.PostEvent(EventID.OnRubyDash,dashCoolDown);
        //Dash movement
        float currentDashTime = dashTime; // Reset the dash timer.
        while (currentDashTime > 0f)
        {
            currentDashTime -= Time.deltaTime; // Lower the dash timer each frame.
            _rigidBody.velocity = _lookDirection.normalized * dashSpeed; // Dash in the direction that was held down.
            // No need to multiply by Time.DeltaTime here, physics are already consistent across different FPS.
            yield return null; // Returns out of the coroutine this frame so we don't hit an infinite loop.
        }

        _isDashing = false;
        //Stop the dash
        _rigidBody.velocity = new Vector2(0f, 0f); // Stop dashing.
    }

    private void DoShockwave(bool isFromRevive = false)
    {
        //Create overlapped sphere to hit all enemy in a area
        //Detect all enemy in range
        var results = Physics2D.OverlapCircleAll(GameManager.Instance.RubyPosition, shockwaveRange, enemyLayerMask);

        //Hit nothing so return here
        if (results.Length > 0)
        {
            Debug.Log("EnemyHitByShockWave:"+results.Length);
            //Check all collider
            foreach (var enemyCollider in results)
            {
                if (enemyCollider.CompareTag(Constant.EnemyTag))
                {
                    //Hit enemy
                    Debug.Log("EnemyHitByShockWave2");
                    enemyCollider.GetComponent<Enemy>().GetHitNormal(shockwaveDamage,shockwaveForce);
                }
            }
        }
        
        //Turn on effect
        shockwaveEffect.gameObject.SetActive(true);
        
        //Play sound effect
        AudioManager.PlaySound(AudioLibrarySounds.Shockwave);
        
        //Tick event 
        if (!isFromRevive)
        {
            EventDispatcher.Instance.PostEvent(EventID.OnRubyShockWave, shockwaveCoolDown);
        }
    }

    public void ChangeCoin(int value)
    {
        if (value > 0)
        {
            ObjectsPoolManager.SpawnObject(pickHealthEffect.gameObject, _rigidBody.position + Vector2.up * 0.5f,
                Quaternion.identity,
                ObjectsPoolManager.PoolType.ParticleSystem);
        }
        
        PlayerPrefHelper.IncreaseCurrentCoin(value);
        EventDispatcher.Instance.PostEvent(EventID.OnCoinReceive);
    }

    private void PlayerDeath()
    {
        _spriteRenderer.enabled = false;
        EventDispatcher.Instance.PostEvent(EventID.OnRubyDeath);
    }

    private void OnRevive()
    {
        _spriteRenderer.enabled = true;
        //Update health
        Health = maxHealth;
        _isDeath = false;

        EventDispatcher
            .Instance.PostEvent(EventID.OnHealthChange, Health);
        
        DoShockwave(true);
    }

    private void OnWeaponUnlock(WeaponType weaponType)
    {
        //Find Weapon
        int findWeapon = -1;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].WeaponType == weaponType)
            {
                findWeapon = i;
            }
        }
        
        //Find Correct hand slot
        
        if (findWeapon != -1)
        {
          Weapon weaponCreated = Instantiate(weapons[findWeapon], handSlot[_numberOfActiveHands].transform);
          
          //Give it first Level
          WeaponData levelZeroData = new WeaponData(weaponType,
              GameManager.Instance.WeaponAttributeManagers.AttributeOfFirstLevel(weaponType));
          weaponCreated.SetAttribute(levelZeroData);

          //Activate the hand
          handSlot[_numberOfActiveHands].gameObject.SetActive(true);

          
          //Increase of active hands
          _numberOfActiveHands++;

        }
        else
        {
            Debug.LogError("No Weapon found at Ruby");
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GameManager.Instance.RubyPosition, shockwaveRange);
    }
    
}