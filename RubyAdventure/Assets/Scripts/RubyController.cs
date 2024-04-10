using UnityEngine;
using TMPro;

public class RubyController : MonoBehaviour
{
    
    //Use this for money now
    [SerializeField]
    private int numberOfClog;
    public int NumberOfClog => numberOfClog;

    //Health
    public int maxHealth = 5;

    public int Health { get; private set; }

    //Move speed

    public float speed = 10.0f;

    //Time invincible after talking damage
    public float timeInvincible = 0.5f;
    private bool _isInvincible;
    private float _invincibleTimer;

    private Rigidbody2D _rigidBody;


    private Animator _animator;
    private Vector2 _lookDirection = new(1, 0);

    public GameObject projectilePrefab;

    public ParticleSystem getHitEffect;
    public ParticleSystem pickHealthEffect;
    [SerializeField]private ParticleSystem deathEffect;
    private AudioSource _audioSource;
    public AudioClip getHitClip;
    public AudioClip throwClip;
    [SerializeField]
    private TMP_Text bulletTxt;

    private RubyControl _rubyControl;

    public event System.Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {

        UIHealthBar.instance.MaxSize();

        if (TitleController.instance.btnChoice == "Load")
        {
            LoadGame loadGame = FindObjectOfType<LoadGame>();
            SaveData rubyData = loadGame.Load();
            if (rubyData != null)
            {
                transform.position = new Vector2(rubyData._currentPositionX, rubyData._currentPositionY);
                numberOfClog = rubyData._bulletAmount;
                Health = rubyData._health;
                UIHealthBar.instance.SetValue(Health / (float)maxHealth);
            }

        }
        else
        {
            numberOfClog = 0;
            Health = maxHealth;
        }
        bulletTxt.text = numberOfClog.ToString();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _rubyControl = new RubyControl();

    }
    private void OnEnable()
    {
        _rubyControl.Enable();
    }

    private void OnDisable()
    {
        _rubyControl.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Health <= 0)
        {
            PlayerDeath();
        }

        Vector2 move = _rubyControl.Player.Move.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        if (_isInvincible)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0)
            {
                _isInvincible = false;
            }
        }
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Launch"))
        {
            if (_rubyControl.Player.Fire.triggered)
            {
                if (numberOfClog > 0)
                {
                    Launch();
                }
            }
        }
        if (_rubyControl.Player.Talk.triggered)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rigidBody.position + Vector2.up * 0.2f, _lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Launch"))
        {
            Vector2 position = _rigidBody.position;
            position.x += speed * _rubyControl.Player.Move.ReadValue<Vector2>().x * Time.deltaTime;
            position.y += speed * _rubyControl.Player.Move.ReadValue<Vector2>().y * Time.deltaTime;
            _rigidBody.MovePosition(position);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            _animator.SetTrigger("Hit");

            if (_isInvincible)
            {
                return;
            }
            _isInvincible = true;
            _invincibleTimer = timeInvincible;
            Instantiate(getHitEffect, _rigidBody.position + Vector2.up * 0.5f, Quaternion.identity);
            PlayAudio(getHitClip);
        }
        else if (amount > 0)
        {
            Instantiate(pickHealthEffect, _rigidBody.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        Health = Mathf.Clamp(Health + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(Health / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, _rigidBody.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        _animator.SetTrigger("Launch");
        PlayAudio(throwClip);
        ChangeBullet(-1);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void ChangeBullet(int value)
    {
        if (value > 0)
        {
            Instantiate(pickHealthEffect, _rigidBody.position + Vector2.up * 0.5f, Quaternion.identity);

        }
        numberOfClog += value;
        bulletTxt.text = numberOfClog.ToString();
    }

    private void PlayerDeath()
    {
        Instantiate(deathEffect, _rigidBody.position + Vector2.up * 0.5f, Quaternion.identity);
        gameObject.SetActive(false);
        OnPlayerDeath?.Invoke();
    }
}
