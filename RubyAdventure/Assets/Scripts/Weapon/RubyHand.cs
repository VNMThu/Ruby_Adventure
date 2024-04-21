using System;
using System.Collections;
using UnityEngine;

public class RubyHand : MonoBehaviour
{
    private Weapon _weapon;
    private Transform _currentTarget;
    private bool _isSpriteFlipLeft;
    private bool _isRotating;

    private void Attack()
    {
        _weapon.Attack();
    }

    private void Start()
    {
       SetWeapon();
    }

    private void SetWeapon()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }

    private IEnumerator C_RotateWeapon()
    {
        _isRotating = true;
        
        //Rotate to target
        while (_isRotating)
        {
            float angle = Mathf.Atan2(_currentTarget.position.y - transform.position.y,
                _currentTarget.position.x - transform.position.x) * Mathf.Rad2Deg;
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
                    _weapon.FlipSprite();
                    _isSpriteFlipLeft = true;
                    break;
                case < 90 or > 270 when _isSpriteFlipLeft:
                    _weapon.FlipSprite();
                    _isSpriteFlipLeft = false;
                    break;
            }

            //Attack weapon
            Attack();
            yield return null;

        }
        
        //If rotate hit enemy -> Fire bullet

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        //Find the enemy that closer
        
        if (_currentTarget != null)
        {
            if (Vector3.Distance(other.transform.position, transform.position) <
                Vector3.Distance(_currentTarget.position, transform.position))
            {
                _currentTarget = other.transform;
            }
        }
        else
        {
            _currentTarget = other.transform;
        }


        //Start rotate toward that enemy
        if (!_isRotating)
        {
            StartCoroutine(C_RotateWeapon());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        
        //Fire
        if (_isRotating)
        {
            StopCoroutine(C_RotateWeapon());
        }

        _weapon.StopAttack();
        _isRotating = false;
    }
}
