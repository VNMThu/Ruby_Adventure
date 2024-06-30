using System;
using System.Collections;
using UnityEngine;

public class Jambi : MonoBehaviour
{
    [SerializeField] private GameObject talkButton;
    [SerializeField] private float distanceToActiveButton;
    private Coroutine _checkingRubyCoroutine;
    private Action<object> _onStartLevel;

    private void OnEnable()
    {
        talkButton.SetActive(false);
        _onStartLevel = _ => StopChecking();
        EventDispatcher.Instance.RegisterListener(EventID.OnStartLevel,_onStartLevel);

    }

    /// <summary>
    /// Event in timeline
    /// </summary>
    public void StartChecking()
    {
        _checkingRubyCoroutine = StartCoroutine(C_CheckingToActiveButton());
    }
    
    private IEnumerator C_CheckingToActiveButton()
    {
        while (gameObject.activeInHierarchy)
        {
            if (!talkButton.activeInHierarchy)
            {
                if (Vector2.Distance(GameManager.Instance.RubyPosition, transform.position) <= distanceToActiveButton)
                {
                    talkButton.SetActive(true);
                }
            }
            else
            {
                if (Vector2.Distance(GameManager.Instance.RubyPosition, transform.position) > distanceToActiveButton)
                {
                    talkButton.SetActive(false);
                }
            }
            yield return null;
        }
    }

    private void StopChecking()
    {
        StopCoroutine(_checkingRubyCoroutine);
    }
}
