using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossBug : Enemy
{
    [SerializeField] private SpriteRenderer spriteRenderer;

   protected override void StartMoving()
   {
      StartCoroutine(MoveToRuby());
   }



   private IEnumerator MoveToRuby()
   {
      while (IsAlive)
      {
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
   private void OnTriggerEnter2D(Collider2D other)
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
      
      //Tick win level
      EventDispatcher.Instance.PostEvent(EventID.OnWinLevel);
   }
}
