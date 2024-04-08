using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SoundController : MonoBehaviour
{
    const float minVolumn = -80;
    float currentVolumn;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    GameObject soundButton;
    [SerializeField]
    private Sprite soundOn;
    [SerializeField]
    private Sprite soundOff;

    bool isOn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSound()
    {
        if (isOn)
        {
            audioMixer.GetFloat("MasterVolumn",out currentVolumn);
            soundButton.GetComponent<Image>().sprite = soundOff;
            isOn = false;
            audioMixer.SetFloat("MasterVolumn",minVolumn);
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
            isOn = true;
            audioMixer.SetFloat("MasterVolumn",currentVolumn);
        }
    }
}
