using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : UIPanel
{
    public override void OnOpen(bool isFromGameplay = true)
    {
        base.OnOpen(false);
    }
    
    public override void OnClose(bool isFromGameplay = true)
    {
        base.OnClose(false);
    }
}
