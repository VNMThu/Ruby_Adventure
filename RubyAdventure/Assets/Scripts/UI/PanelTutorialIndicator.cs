using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelTutorialIndicator : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnTurnOn()
    {
        image.DOFade(1f, 0.1f);
    }
    
    public void OnTurnOff()
    {
        image.DOFade(0.3f, 0.1f);
    }
}
