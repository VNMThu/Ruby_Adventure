using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float attackRange;
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    public override void Attack()
    {
        
        //Start animation - Over Call
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
        }

        _isAttacking = true;
        _attackCoroutine = StartCoroutine(C_AttackCoroutine());
    }
    
    private IEnumerator C_AttackCoroutine()
    {

        while (_isAttacking)
        {
            yield return new WaitForSeconds(1f);
            DOTween.Sequence().Append(transform.DOLocalMoveX(transform.localPosition.x + attackRange, 0.3f)
                    .SetEase(Ease.OutCubic))
                .Append(transform.DOLocalMoveX(0f, 0.1f));

        }
    }

    public override void StopAttack()
    {
        if (!_isAttacking) return;
        _isAttacking = false;
        StopCoroutine(_attackCoroutine);
    }
}
