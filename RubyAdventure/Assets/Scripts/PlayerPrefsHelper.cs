using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsHelper
{
    private const string CoinKey = "ruby_coin";
    private const string levelNumber = "level_number";

    public static int GetCurrentCoin()
    {
        return PlayerPrefs.GetInt(CoinKey, 0);
    }

    public static void SubtractCoins(int amountLost)
    {
        int newValue = GetCurrentCoin() - amountLost;
        PlayerPrefs.SetInt(CoinKey,newValue);
    }
    
    public static void IncreaseCurrentCoin(int increaseValue)
    {
        int newValue = GetCurrentCoin() + increaseValue;
        PlayerPrefs.SetInt(CoinKey,newValue);
    }
}
