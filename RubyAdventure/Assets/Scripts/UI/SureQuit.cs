using System.Collections;
using System.Collections.Generic;
using JSAM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SureQuit : UIPanel
{
    public void OnYesClick()
    {
        GameManager.Instance.ReturnToHome();
    }

    public void OnNoClick()
    {
        OnClose();
    }
}
