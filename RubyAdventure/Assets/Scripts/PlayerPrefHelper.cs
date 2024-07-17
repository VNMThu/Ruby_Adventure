using UnityEngine;

public static class PlayerPrefHelper
{
    private const string CoinKey = "ruby_coin";
    private const string SoundKey = "sound_toggle";
    private const string MusicKey = "music_toggle";
    // private static int CoinAmount = 0;

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
        PlayerPrefs.SetInt(SoundKey, toggleValue ? 1 : 0);
    }
    
    public static void ToggleMusic(bool toggleValue)
    {
        PlayerPrefs.SetInt(MusicKey, toggleValue ? 1 : 0);
    }

    public static bool GetSoundStatus()
    {
        return PlayerPrefs.GetInt(SoundKey,1) == 1;
    }
    
    public static bool GetMusicStatus()
    {
        return PlayerPrefs.GetInt(MusicKey,1) == 1;
    }
    
    
}
