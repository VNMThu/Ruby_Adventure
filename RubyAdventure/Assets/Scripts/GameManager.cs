using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //Actions
    public Action OnGameOver;
    public Action OnGameWin;

    //Robot amount
    [SerializeField] private GameObject enemiesParent;
    private int _robotAmount;

    private void Start()
    {
        for (int i = 0; i < enemiesParent.transform.childCount; i++)
        {
            if (enemiesParent.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                _robotAmount++;
            }
        }
    }

    public void FixSingleRobot()
    {
        _robotAmount--;
        if (_robotAmount == 0)
        {
            OnGameWin.Invoke();
        }
    }
}
