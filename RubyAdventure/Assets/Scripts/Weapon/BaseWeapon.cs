using System;
using System.Collections;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    [SerializeField] protected GameObject bullet;
    protected SpriteRenderer _spriteRenderer;
    protected Transform currentTarget;
    protected bool _isSpriteFlipLeft;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual IEnumerator C_FireBullet()
    {
        //Fire off this bullet - Passing in target here
        
        // Instantiate(bullet);
        yield return new WaitForSeconds(1 / fireRate);
    }
    
    

    protected virtual IEnumerator C_RotateWeapon()
    {
        //Rotate to target
        while (!CheckHitEnemy())
        {
            float angle = Mathf.Atan2(currentTarget.position.y - transform.position.y,
                currentTarget.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            var rotation = transform.rotation;
            
            rotation = Quaternion.RotateTowards(rotation, targetRotation,
                360f);
            
            transform.rotation = rotation;
            
            float currentAngleOfGun = rotation.eulerAngles.z;
            
            //Flip sprite correct to rotation
            
            switch (currentAngleOfGun)
            {
                case > 90 and < 270 when !_isSpriteFlipLeft:
                    _spriteRenderer.flipY = !_spriteRenderer.flipY;
                    _isSpriteFlipLeft = true;
                    break;
                case < 90 or > 270 when _isSpriteFlipLeft:
                    _spriteRenderer.flipY = !_spriteRenderer.flipY;
                    _isSpriteFlipLeft = false;
                    break;
            }

            yield return null;

        }
        
        //If rotate hit enemy -> Fire bullet
        StartCoroutine(C_FireBullet()) ;
        
    }

    protected virtual bool CheckHitEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, LayerMask.GetMask("Character"));

        //Check if hit anything at all
        if (hit.collider == null) return false; 
        
        //Check if hit enemy
        if(hit.collider.CompareTag("Enemy")) return true;

        //Hit something else
        return false;
    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        //Find the enemy that closer
        if (currentTarget != null)
        {
            if (Vector3.Distance(other.transform.position, transform.position) <
                Vector3.Distance(currentTarget.position, transform.position))
            {
                currentTarget = other.transform;
            }
        }
        else
        {
            currentTarget = other.transform;
        }


        //Start rotate toward that enemy
        StartCoroutine(C_RotateWeapon());
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        //Fire
        StopCoroutine(C_RotateWeapon());
    }
}
