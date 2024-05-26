using JSAM;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAdvance :MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public RectTransform graphic;
    public bool canPlaySoundActive = true;
    public bool canPlaySoundInActive = true;
    public bool canInnerBounce = true;
    
    private Button _button;

    private void Start()
    {
        if (graphic)
        {
        }

        _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (!_button.interactable)
        {
            if (canPlaySoundInActive)
            {
                AudioManager.PlaySound(AudioLibrarySounds.ButtonDecline);
            }

            return;
        }
        

        if (canPlaySoundActive)
        {
            AudioManager.PlaySound(AudioLibrarySounds.ButtonAccept);
        }
        

        if (canInnerBounce)
        {
            graphic.localScale = new Vector3(0.92f, 0.92f, 1f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_button.interactable)
            return;
        
        if (canInnerBounce)
        {
            graphic.localScale = Vector3.one;
        }
    }

}
