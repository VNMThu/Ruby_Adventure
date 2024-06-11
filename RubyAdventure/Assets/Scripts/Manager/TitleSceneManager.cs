using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : GenericSingleton<TitleSceneManager>
{
    
    public UIController UIController => uiController;
    [SerializeField] private UIController uiController;    
}
