using DG.Tweening;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform panel;
    
    [Header("Open and close animation")] 
    [SerializeField] private float defaultOpenScale;
    [SerializeField] private float openAnimationTime;
    [SerializeField] private float targetCloseScale;
    [SerializeField] private float closeAnimationTime;
    [Header("Panel ID")] [SerializeField] private UIPanelID id;
    public UIPanelID ID =>id;
    protected virtual void Awake()
    {
        canvas.enabled = false;
    }

    public virtual void OnOpen()
    {
        Time.timeScale = 0f;
        canvas.enabled = true;
        panel.transform.localScale = Vector3.one * defaultOpenScale;
        panel.transform.DOScale(Vector3.one, openAnimationTime).SetUpdate(true);
    }
    
    public virtual void OnClose()
    {
        panel.transform.DOScale(Vector3.one*targetCloseScale, closeAnimationTime).SetUpdate(true);
        canvas.enabled = false;
        Time.timeScale = 1f;
    }
    
    
}
