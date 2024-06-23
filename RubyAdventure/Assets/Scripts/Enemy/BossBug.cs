using System;
using System.Collections;
using System.Collections.Generic;
using JSAM;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BossBug : Enemy
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _isDeath = Animator.StringToHash("IsDeath");
    private readonly int _triggerAttackEnergyBall = Animator.StringToHash("AttackBalls");
    private readonly int _triggerAttackLaser = Animator.StringToHash("AttackLaser");

    
    [Header("Bug Boss SPECIFICALLY")] [SerializeField]
    private float timeBetweenAttack;
    private float _actualCountDown;

    [Header("Attack Energy balls")] 
    [SerializeField]private RangeSoldierBullet energyBall;
    [SerializeField]private Transform attackPoint;
    [SerializeField]private float ballSpeed;

   protected override void StartMoving()
   {
      StartCoroutine(MoveToRuby());
   }

   protected override void OnEnable()
   {
      base.OnEnable();
      StartAttackSequence();
   }


   private IEnumerator MoveToRuby()
   {
      while (IsAlive && !IsAttacking)
      {
            if (!animator.GetBool(_isMoving))
            {
               animator.SetBool(_isMoving,true);
            }
         
            Vector3 rubyPosition = GameManager.Instance.RubyPosition;
            //speed
            float step = speed * Time.deltaTime;

            // move towards the ruby location
            transform.position = Vector2.MoveTowards(transform.position, rubyPosition, step);

            //Flip sprite depend on ruby position
            spriteRenderer.flipX = !(rubyPosition.x < transform.position.x);
            
            yield return null;
      }
   }

   private void StartAttackSequence()
   {
      _actualCountDown = timeBetweenAttack;
      StartCoroutine(C_CountDownToAttack());
   }

   
   /// <summary>
   /// Hurt ruby when hit her
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerStay2D(Collider2D other)
   {
      switch (other.tag)
      {
         case Constant.RubyHurtBoxTag:
         {
            //Change HP
            GameManager.Instance.Ruby.ChangeHealth(-damage);
            break;
         }
      }
   }

   protected override void Death()
   {
      base.Death();
      
      //Start death animation
      animator.SetTrigger(_isDeath);
      
      //Tick win level
      EventDispatcher.Instance.PostEvent(EventID.OnWinLevel);
   }

   private IEnumerator C_CountDownToAttack()
   {
      while (_actualCountDown > 0)
      {
         _actualCountDown -= Time.deltaTime;
         yield return null;
      }

      IsAttacking = true;
      
      //Roll the dice to choose attack
      int result = Random.Range(0,10);
      
      //Choose Attack 
      TriggerAttack(result<=4);
      
      //Reset
      StartAttackSequence();
   }
   
   private void TriggerAttack(bool isAttack1)
   {
      animator.SetTrigger(isAttack1 ? _triggerAttackEnergyBall : _triggerAttackLaser);
   }

   /// <summary>
   /// This function get call to fire off balls in 8 direction
   /// </summary>
   public void FireOffBalls()
   {
      Vector2[] directions = {
         new(0, 1), new(1, 1), new(1, 0), new(1, -1), new(0, -1),
         new(-1, -1), new(-1, 0), new(-1, 1)
      };
      
      foreach (var direction in directions)
      {
         //Create projectile
         RangeSoldierBullet bullet = ObjectsPoolManager.SpawnObject(energyBall.gameObject,
            attackPoint.position, transform.rotation,
            ObjectsPoolManager.PoolType.Projectile).GetComponent<RangeSoldierBullet>();

         //Find fire all direction
         Vector2 directionNormalized = direction.normalized;
        
         //Launch it
         bullet.Launch(directionNormalized, ballSpeed, damage,0);
      }

      IsAttacking = false;
      //Sound effect
      AudioManager.PlaySound(AudioLibrarySounds.LazerGun);
   }

   public override void GetHitNormal(float damageDeal, float forcePushPower = 0)
   {
      //Ruby hit dont move boss
      base.GetHitNormal(damageDeal,0f);
   }
}
