using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DialogPanel : UIPanel
{
    [SerializeField] private DialogBox jimboDialogBox;
    [SerializeField] private DialogBox rubyDialogBox;
    
    [Header("Dialogs")]
    [SerializeField] private string[] dialogsInCutscene;
    [SerializeField] private string dialogsInGame;

    [Header("For cinematic dialog")] [SerializeField]
    private float bufferBetweenDialog;

    private bool _isShowDialogInGame;
    private bool _isShowDialogInCutScene = true;

    
    //Get call in timeline
    public void ShowCinematicDialog()
    {
        OnOpen(false);
        
        DOVirtual.DelayedCall(openAnimationTime, () =>
        {
            StartCoroutine(C_ShowDialogSequence());
        });
    }

    private IEnumerator C_ShowDialogSequence()
    {
        for (int i = 0; i < dialogsInCutscene.Length; i++)
        {
            if (!_isShowDialogInCutScene)
            {
                yield break;
            }
            
            float waitTime;
            if (i % 2 == 0)
            {
                waitTime = jimboDialogBox.ShowDialog(dialogsInCutscene[i]) + bufferBetweenDialog;
                yield return new WaitForSeconds(waitTime);
                jimboDialogBox.HideDialog();
            }
            else
            {
                waitTime = rubyDialogBox.ShowDialog(dialogsInCutscene[i]) + bufferBetweenDialog;
                yield return new WaitForSeconds(waitTime);
                rubyDialogBox.HideDialog();
            }
        }
    }


    //End timeline event
    public void StopDialog()
    {
        _isShowDialogInCutScene = false;
    }
    
    public void ShowInGameDialog()
    {
        if (_isShowDialogInGame) return;
        OnOpen(false);
        _isShowDialogInGame = true;
        StartCoroutine(C_ShowJambiDialog());
    }

    private IEnumerator C_ShowJambiDialog()
    {
        float waitTime = jimboDialogBox.ShowDialog(dialogsInGame) + bufferBetweenDialog;
        yield return new WaitForSeconds(waitTime);
        jimboDialogBox.HideDialog();
        _isShowDialogInGame = false;
    }
}
