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

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = soundOn;
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
        }
        else
        {
            AudioManager.MusicMuted = !toggle;
        }
    }
}