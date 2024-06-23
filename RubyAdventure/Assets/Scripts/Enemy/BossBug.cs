using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossBug : Enemy
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _isDeath = Animator.StringToHash("IsDeath");

   protected override void StartMoving()
   {
      StartCoroutine(MoveToRuby());
   }



   private IEnumerator MoveToRuby()
   {
      while (IsAlive)
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

   
   /// <summary>
   /// Hurt ruby when hit her
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerStay2D(Collider2D other)
   {
      Debug.Log("Boss hit:"+other.tag);
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
}
