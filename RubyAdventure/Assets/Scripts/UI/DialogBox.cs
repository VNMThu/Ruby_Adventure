using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogBox: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private float wordBySecond;
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup.alpha = 0f;
    }

    public float ShowDialog(string textNeedToShow)
    {
        canvasGroup.alpha = 1f;
        StartCoroutine(C_ShowCharacterByCharacter(textNeedToShow));
        
        //Return number of seconds need to show full dialog
        return 1 / wordBySecond * textNeedToShow.Length;
    }

    private IEnumerator C_ShowCharacterByCharacter(string textNeedToShow)
    {
        dialogText.text = "";
        foreach (var character in textNeedToShow)
        {
            dialogText.text += character;
            yield return new WaitForSeconds(1 / wordBySecond) ;
        }
    }

    public void HideDialog()
    {
        canvasGroup.alpha = 0f;
    }
    
}
