using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private Slider expSliderUI;
    private Action<object> _onExpReceiveRef;
    private int _currentLevel;
    private int _currentExp;

    private void OnEnable()
    {
        //Reset value
        expSliderUI.value = 0;
        _currentLevel = 0;
        _currentExp = 0;

        //Handle when ruby receive Exp
        _onExpReceiveRef = (param) => OnExpReceive((int)param);
        EventDispatcher.Instance.RegisterListener(EventID.OnExpReceive, _onExpReceiveRef);
    }

    // private void OnDisable()
    // {
    //     EventDispatcher.Instance.RemoveListener(EventID.OnExpReceive,_onExpReceiveRef);
    // }

    private void OnExpReceive(int expReceive)
    {
        Debug.Log("Receive:" + expReceive + " exp");
        //Find out the current Max
        int maxValueOfGauge = GetXpNeedBaseOnLevel(_currentLevel + 1);

        //Current exp
        _currentExp += expReceive;

        //Find Percent and Update UI
        float percent = (float)_currentExp / maxValueOfGauge;
        expSliderUI.value = percent;

        //If not enough to level up then return
        if (_currentExp < maxValueOfGauge) return;

        //Level up here
        _currentLevel++;
        _currentExp = 0;
        expSliderUI.value = 0;
    }

    private int GetXpNeedBaseOnLevel(int level)
    {
        //Ruby start at level 1
        // Level   XP      Difference
        // 0       0       -
        // 1       4       4
        // 2       9       5
        // 3       15      6
        // 4       22      7

        int xpNeed = 0;
        for (int i = 0; i < level; i++)
        {
            xpNeed += Constant.XpConstant + i;
        }

        return xpNeed;
    }
}