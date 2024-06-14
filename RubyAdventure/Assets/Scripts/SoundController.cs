using System;
using JSAM;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private bool isSound;
    private bool _isOn = true;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = soundOn;
    }

    private void OnEnable()
    {
        //Update status
        bool toggle = isSound ? PlayerPrefHelper.GetSoundStatus() : PlayerPrefHelper.GetMusicStatus();
        if (toggle)
        {
            _isOn = true;
            _image.sprite = soundOn;
        }
        else
        {
            _isOn = false;
            _image.sprite = soundOff;
        }
        
    }

    public void ChangeSound()
    {
        if (_isOn)
        {
            _isOn = false;
            MuteOnOff(false);
            _image.sprite = soundOff;
        }
        else
        {
            _isOn = true;
            MuteOnOff(true);
            _image.sprite = soundOn;
        }
    }

    private void MuteOnOff(bool toggle)
    {
        if (isSound)
        {
            AudioManager.SoundMuted = !toggle;
            PlayerPrefHelper.ToggleSound(toggle);
        }
        else
        {
            AudioManager.MusicMuted = !toggle;
            PlayerPrefHelper.ToggleMusic(toggle);

        }
    }
}