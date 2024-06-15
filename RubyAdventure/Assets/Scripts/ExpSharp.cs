using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JSAM;
using Unity.Mathematics;
using UnityEngine;

public class ExpSharp : MonoBehaviour
{
    [SerializeField] private int expReceived;
    [SerializeField] private GameObject effectWhenHitRuby;

    private bool _isMoving;

    private void OnEnable()
    {
        _isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Constant.RubyTag) || _isMoving) return;

        //Hit ruby
        _isMoving = true;
        Vector3 dir = (transform.position - GameManager.Instance.RubyPosition).normalized;
        Debug.Log("EXP dir:" + dir);

        //Move backward
        transform.DOMove(0.5f * dir + transform.position, 0.2f).OnComplete(() =>
        {
            //Then move toward ruby
            StartCoroutine(C_MovingToRuby());
        });
    }

    private IEnumerator C_MovingToRuby()
    {
        while (true)
        {
            // Move our position a step closer to the target.
            var step = 8f * Time.deltaTime; // calculate distance to move
            
            transform.position =
                Vector3.MoveTowards(transform.position,GameManager.Instance.RubyPosition,step);

            if (Vector3.Distance(transform.position, GameManager.Instance.RubyPosition) < 0.001f)
            {
                Debug.Log("EXP hit ruby");
                ObjectsPoolManager.SpawnObject(effectWhenHitRuby, GameManager.Instance.Ruby.transform);
                EventDispatcher.Instance.PostEvent(EventID.OnExpReceive, expReceived);
                AudioManager.PlaySound(AudioLibrarySounds.CollectExp);
                ObjectsPoolManager.ReturnObjectToPool(gameObject);
                //Tick event exp receive
                break;
            }

            yield return null;
        }
    }
}