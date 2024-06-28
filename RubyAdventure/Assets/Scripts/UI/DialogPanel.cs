using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DialogPanel : UIPanel
{
    [SerializeField] private DialogBox jimboDialogBox;
    [SerializeField] private DialogBox rubyDialogBox;
    
    [SerializeField] private string[] dialogsInCutscene;
    [Header("For cinematic dialog")] [SerializeField]
    private float bufferBetweenDialog;
    
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

    public void ShowInGameDialog()
    {
        
    }
}
