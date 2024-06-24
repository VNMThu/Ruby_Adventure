using UnityEngine;

public class BugBossLaser : MonoBehaviour
{
   [SerializeField] private ParticleSystem warning;
   [SerializeField] private ParticleSystem laser;
   [SerializeField] private ParticleSystem explosion;

   [Header("Attack Config")] [SerializeField]
   private float warningTime;
   [SerializeField] private float strikeArea;
   [SerializeField] private LayerMask playerLayerMask;

   private int _currentDamage;
   public void LaserAttack(int damage)
   {
      //Make warning 
      warning.Play();
      
      //Strike
      _currentDamage = damage;
      Invoke(nameof(LaserStrike),warningTime);
   }

   private void LaserStrike()
   {
      //Stop warning
      warning.Stop();
      
      //Call effect laser and explosion
      laser.Play();
      explosion.Play();
      
      //Check to cause damage
      Invoke(nameof(OnDoneEffect),0.1f);
   }

   private void OnDoneEffect()
   {
      //Detect if player in range
      var results = Physics2D.OverlapCircleAll(transform.position, strikeArea, playerLayerMask);
      Debug.Log("Laser connect 1");

      //Hit nothing so return here
      if (results.Length <= 0) return;
        
      Debug.Log("Laser connect 2");


      //Check all collider
      foreach (var variable in results)
      {
         Debug.Log("Laser connect 3:"+variable.tag);

         if (variable.CompareTag(Constant.RubyHurtBoxTag))
         {
            //Hit Ruby
            GameManager.Instance.Ruby.ChangeHealth(-_currentDamage);
         }
      }
      
      //Return self to bool
      Invoke(nameof(AfterDone),0.3f);
   }

   private void AfterDone()
   {
      ObjectsPoolManager.ReturnObjectToPool(gameObject);
   }
   
   
   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(transform.position, strikeArea);
   }

}
