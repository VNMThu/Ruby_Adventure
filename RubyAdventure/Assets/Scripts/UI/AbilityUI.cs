using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image countDownUI;
    [SerializeField] private OnScreenButton onScreenButtonUI;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private EventID eventSubToReact;
    [SerializeField] private Button normalButton;
    private float _countDownTime;
    
    // Start is called before the first frame update
    private Action<object> _onAbilityUseRef;
    private void OnEnable()
    {
        AbilityEnable();
        _onAbilityUseRef = param => OnAbilityUse((float)param);
        EventDispatcher.Instance.RegisterListener(eventSubToReact,_onAbilityUseRef);
    }

    private void AbilityEnable()
    {
        countDownUI.fillAmount = 0;
        onScreenButtonUI.enabled = true;
        normalButton.interactable = true;
        timeText.gameObject.SetActive(false);
    }
    
    private void AbilityDisable()
    {
        countDownUI.fillAmount = 1;
        onScreenButtonUI.enabled = false;
        normalButton.interactable = false;
        timeText.text = _countDownTime.ToString("F1");
        timeText.gameObject.SetActive(true);
        
    }
    
    private void OnAbilityUse(float coolDownTime)
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
