using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptionMenuController : MonoBehaviour
{
    public Slider masterSlider;
    public Slider bgSlider;
    public Slider seSlider;

    // Start is called before the first frame update
    public AudioMixer audioMixer;
    private void Awake() {
        SetVolumn("MasterVolumn",masterSlider);
        SetVolumn("BGVolumn",bgSlider);
        SetVolumn("SEVolumn",seSlider);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetVolumn(string soundName, Slider slider){
        float volumn;
        if(audioMixer.GetFloat(soundName,out volumn)){
            slider.value = volumn;
        }
    }
    public void ChangeMasterVolumn(float volumn)
    {
        audioMixer.SetFloat("MasterVolumn", volumn);
    }

    public void ChangeBGVolumn(float volumn)
    {
        audioMixer.SetFloat("BGVolumn", volumn);
    }

    public void ChangeSEVolumn(float volumn)
    {
        audioMixer.SetFloat("SEVolumn", volumn);
    }
}
