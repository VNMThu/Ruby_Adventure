using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RubyController : MonoBehaviour
{
    [SerializeField]
    private int numberOfClog;
    public int NumberOfClog
    {
        get { return numberOfClog; }
        set { numberOfClog = value; }
    }
    public int maxHealth = 5;
    public float speed = 10.0f;
    int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public float timeInvincible = 0.5f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigid_body;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    public ParticleSystem getHitEffect;
    public ParticleSystem pickHealthEffect;
    public ParticleSystem DeathEffect;
    private AudioSource audioSource;
    public AudioClip getHitClip;
    public AudioClip throwClip;
    [SerializeField]
    private TMP_Text bulletTxt;

    private RubyControl rubyControl;

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
                _health = rubyData._health;
                UIHealthBar.instance.SetValue(_health / (float)maxHealth);
            }

        }
        else
        {
            numberOfClog = 0;
            _health = maxHealth;
        }
        bulletTxt.text = numberOfClog.ToString();
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        rubyControl = new RubyControl();

    }
    private void OnEnable()
    {
        rubyControl.Enable();
    }

    private void OnDisable()
    {
        rubyControl.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_health <= 0)
        {
            PlayerDeath();
        }

        Vector2 move = rubyControl.Player.Move.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Launch"))
        {
            if (rubyControl.Player.Fire.triggered)
            {
                if (numberOfClog > 0)
                {
                    Launch();
                }
            }
        }
        if (rubyControl.Player.Talk.triggered)
        {
            RaycastHit2D hit = Physics2D.Raycast(rigid_body.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
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
    void FixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Launch"))
        {
            Vector2 position = rigid_body.position;
            position.x += speed * rubyControl.Player.Move.ReadValue<Vector2>().x * Time.deltaTime;
            position.y += speed * rubyControl.Player.Move.ReadValue<Vector2>().y * Time.deltaTime;
            rigid_body.MovePosition(position);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");

            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Instantiate(getHitEffect, rigid_body.position + Vector2.up * 0.5f, Quaternion.identity);
            PlayAudio(getHitClip);
        }
        else if (amount > 0)
        {
            Instantiate(pickHealthEffect, rigid_body.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        _health = Mathf.Clamp(_health + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(_health / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigid_body.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        PlayAudio(throwClip);
        ChangeBullet(-1);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void ChangeBullet(int value)
    {
        if (value > 0)
        {
            Instantiate(pickHealthEffect, rigid_body.position + Vector2.up * 0.5f, Quaternion.identity);

        }
        numberOfClog += value;
        bulletTxt.text = numberOfClog.ToString();
    }

    private void PlayerDeath()
    {
        Instantiate(DeathEffect, rigid_body.position + Vector2.up * 0.5f, Quaternion.identity);
        gameObject.SetActive(false);
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }
}
