using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSliderUI;
    private Action<object> _onHealthChange;
    private int _maxHealth;

    private void OnEnable()
    {
        //Register event
        _onHealthChange = param => OnHealthChange((int)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnHealthChange, _onHealthChange);

        //
        _maxHealth = GameManager.Instance.Ruby.MaxHealth;
    }

    // private void OnDisable()
    // {
    //     EventDispatcher.Instance.RemoveListener(EventID.OnHealthChange,_onHealthChange);
    // }

    private void OnHealthChange(int currentHealth)
    {
        float percent = currentHealth / (float)_maxHealth;
        healthSliderUI.value = percent;
    }
}