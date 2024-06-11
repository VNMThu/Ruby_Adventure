using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnButtonPlayClick()
    {
        TitleSceneManager.Instance.UIController.OpenPanel(UIPanelID.Loading);
    }
}
