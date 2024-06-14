using UnityEngine;

public static class PlayerPrefHelper
{
    private const string CoinKey = "ruby_coin";
    private const string levelNumber = "level_number";
    private const string soundKey = "sound_toggle";
    private const string musicKey = "music_toggle";


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

    public static void ToggleSound(bool toggleValue)
    {
        PlayerPrefs.SetInt(soundKey, toggleValue ? 1 : 0);
    }
    
    public static void ToggleMusic(bool toggleValue)
    {
        PlayerPrefs.SetInt(musicKey, toggleValue ? 1 : 0);
    }

    public static bool GetSoundStatus()
    {
        return PlayerPrefs.GetInt(soundKey,1) == 1;
    }
    
    public static bool GetMusicStatus()
    {
        return PlayerPrefs.GetInt(musicKey,1) == 1;
    }
    
    
}
