using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExpSharp : MonoBehaviour
{
    [SerializeField] private float distanceToFindRuby;
    //Find Ruby to fly to
    private void OnEnable()
    {
        if (Vector2.Distance(transform.position, GameManager.Instance.Ruby.transform.position) < distanceToFindRuby)
        {
            transform.DOMove(GameManager.Instance.Ruby.transform.position, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                ObjectsPoolManager.ReturnObjectToPool(gameObject);
            });
        }   
    }
    
    
}
