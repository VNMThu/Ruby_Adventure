using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float attackRange;    
    [SerializeField] private GameObject sparkHit;

    private bool _isAttacking;
    private Coroutine _attackCoroutine;
    private bool _isCausingDamage;
    private RubyHand _hand;
    private void Start()
    {
        _hand = transform.GetComponentInParent<RubyHand>();
    }

    public override void Attack()
    {
        //Start animation - Over Call
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
        }

        _isAttacking = true;
        _isCausingDamage = false;
        _attackCoroutine = StartCoroutine(C_AttackCoroutine());
    }
    
    private IEnumerator C_AttackCoroutine()
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(1f/fireRate);
            _isCausingDamage = true;
            
            // stop rotation
            _hand.ForceStopRotating();
            
            // This is the animation for attacking
            DOTween.Sequence().Append(transform.DOLocalMoveX(transform.localPosition.x + attackRange, 0.3f).OnComplete(
                        () =>
                        {
                            //After done thrust -> No more causing damage
                            _isCausingDamage = false;
                        })
                    .SetEase(Ease.OutCubic))
                .Append(transform.DOLocalMoveX(0f, 0.1f))
                .OnComplete(() =>
                {
                    //Rotate again
                    _hand.StopForceStopRotating();
                });

        }
    }

    public override void StopAttack()
    {
        if (!_isAttacking) return;
        _isAttacking = false;
        StopCoroutine(_attackCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isCausingDamage) return;
        switch (other.tag)
        {
            case "Enemy":
            {
                Enemy enemyController = other.GetComponent<Enemy>();

                //Spawn Particle
                ObjectsPoolManager.SpawnObject(sparkHit.gameObject, transform.position, transform.rotation,
                    ObjectsPoolManager.PoolType.ParticleSystem);

                //Play audio
                // enemyController.PlayAudio(hitClip);

                //Change HP
                enemyController.GetHitNormal(damagePerAttack, forcePushBack);
                
                break;
            }
        }
    }
}
