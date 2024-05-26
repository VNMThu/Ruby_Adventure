using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image countDownUI;
    [SerializeField] private OnScreenButton buttonUI;
    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    private Action<object> _onRubyDashRef;
    private float _countDownTime;
    private void OnEnable()
    {
        AbilityEnable();
        _onRubyDashRef = param => OnRubyDash((float)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnRubyDash,_onRubyDashRef);
    }

    private void AbilityEnable()
    {
        countDownUI.fillAmount = 0;
        buttonUI.enabled = true;
        timeText.gameObject.SetActive(false);
    }
    
    private void AbilityDisable()
    {
        countDownUI.fillAmount = 1;
        buttonUI.enabled = false;
        timeText.text = _countDownTime.ToString("F1");
        timeText.gameObject.SetActive(true);
    }
    
    private void OnRubyDash(float coolDownTime)
    {
        _countDownTime = coolDownTime;
        
        
        AbilityDisable();

        StartCoroutine(C_CountingDown());
    }

    private IEnumerator C_CountingDown()
    {
        float currentTimeLeft = _countDownTime;
        while (currentTimeLeft > 0f)
        {
            countDownUI.fillAmount = currentTimeLeft / _countDownTime;
            currentTimeLeft -= Time.deltaTime;
            timeText.text = currentTimeLeft.ToString("F1") + "s";
            yield return null;
        }
        AbilityEnable();
    }
}
