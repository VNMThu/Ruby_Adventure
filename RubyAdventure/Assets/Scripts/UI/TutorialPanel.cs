using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : UIPanel
{
    [Header("Tutorial Panel")]
    [SerializeField] private RectTransform panelHolder;
    [SerializeField] private Button forwardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private float panelMoveDistance;
    [Header("Progress")] [SerializeField] private Transform indicatorHolder;
    [SerializeField] private GameObject indicatorPrefab;
    
    private int _numberOfTutorialPanel;
    private int _currentTutorialIndex;
    private bool _isMoving;
    private readonly List<PanelTutorialIndicator> indicatorList = new();
    
    private void Start()
    {
        _numberOfTutorialPanel = panelHolder.transform.childCount;
        InitProgressIndicator();
        backButton.gameObject.SetActive(false);
    }

    private void InitProgressIndicator()
    {
        for (int i = 0; i < _numberOfTutorialPanel; i++)
        {
            indicatorList.Add(Instantiate(indicatorPrefab, indicatorHolder).GetComponent<PanelTutorialIndicator>());
            if (i == 0)
            {
                indicatorList[0].OnTurnOn();
            }
            else
            {
                indicatorList[i].OnTurnOff();
            }
        }
        
        
    }
    
    public override void OnOpen(bool isStopTime = true)
    {
        base.OnOpen(false);
    }
    
    public override void OnClose(bool isFromGameplay = true)
    {
        base.OnClose(false);
    }

    public void OnForwardButtonClick()
    {
        if (_isMoving) { return;}
        _currentTutorialIndex++;
        backButton.gameObject.SetActive(true);
        
        if (_currentTutorialIndex >= _numberOfTutorialPanel - 1)
        {
            forwardButton.gameObject.SetActive(false);
        }
        MovePanel(true);
    }
    
    
    public void OnBackButtonClick()
    {
        if (_isMoving) { return;}
        _currentTutorialIndex--;
        forwardButton.gameObject.SetActive(true);
        if (_currentTutorialIndex <=0)
        {
            backButton.gameObject.SetActive(false);
        }
        MovePanel(false);

    }

    private void MovePanel(bool isForward)
    {
        _isMoving = true;
        
        
        //Find target
        float moveDistance = panelMoveDistance;
        if (isForward)
        {
            moveDistance *= -1;
        }

        float targetX = panelHolder.localPosition.x + moveDistance;
        
        //Moving
        panelHolder.DOLocalMoveX(targetX, 0.2f).SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                _isMoving = false;
                MoveIndicator();
            });
    }

    private void MoveIndicator()
    {
        for (int i = 0; i < indicatorList.Count; i++)
        {
            if (i == _currentTutorialIndex)
            {
                indicatorList[i].OnTurnOn();
            }
            else
            {
                indicatorList[i].OnTurnOff();
            }
        }
    }
    
    
}
