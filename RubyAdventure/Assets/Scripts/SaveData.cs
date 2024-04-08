using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public int _health;
    public int _bulletAmount;
    public float _currentPositionX;
    public float _currentPositionY;
    public int _robotCount;
    public bool[] robotState;
    public bool[] itemState;

    public SaveData(int heath, int bulletAmount, float currentPosX, float currentPosY, int robotCount, GameObject[] robots,GameObject[] items)
    {
        this._health = heath;
        this._bulletAmount = bulletAmount;
        this._currentPositionX = currentPosX;
        this._currentPositionY = currentPosY;
        this._robotCount = robotCount;
        robotState = new bool[robots.Length];
        for (int i = 0; i < robots.Length; i++)
        {
            EnemyController enemyController = robots[i].GetComponent<EnemyController>();
            robotState[i] = enemyController.isBroken;
        }

        itemState = new bool[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            itemState[i] = items[i].activeSelf;
        }

    }

    public void PrintOut()
    {
        Debug.Log("Health: " + _health + '\n');
        Debug.Log("Bullet: " + _bulletAmount + "\n");
        Debug.Log("X: " + _currentPositionX + "\n");
        Debug.Log("Y: " + _currentPositionY + "\n");
        Debug.Log("RobotCount: " + _robotCount + "\n");
        for (int i = 0; i < robotState.Length; i++)
        {
            Debug.Log(i + ": " + robotState[i]);
        }

        for (int i = 0; i < itemState.Length; i++)
        {
            Debug.Log(i + ": " + itemState[i]);
        }
    }

}
