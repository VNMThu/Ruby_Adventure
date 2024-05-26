using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<UIPanel> panelList;

    public void OpenPanel(UIPanelID panelID)
    {
        UIPanel targetPanel = FindPanelWithID(panelID);
        if (targetPanel == null)
        {
            Debug.LogError("Panel is not in list");
            return;
        }
        targetPanel.OnOpen();
    }
    
    public void OnClose(UIPanelID panelID)
    {
        UIPanel targetPanel = FindPanelWithID(panelID);
        if (targetPanel == null)
        {
            Debug.LogError("Panel is not in list");
            return;
        }
        targetPanel.OnClose();
    }

    private UIPanel FindPanelWithID(UIPanelID panelID)
    {
        return panelList.FirstOrDefault(panel => panel.ID == panelID);
    }

}
