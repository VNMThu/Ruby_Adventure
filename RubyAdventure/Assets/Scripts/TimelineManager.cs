using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private float timeToSkipTo;
    // private bool _hasSkip;
    public void SkipCutScene()
    {
        playableDirector.time = timeToSkipTo;
        // _hasSkip = true;
    }

    // public void EndCutScene()
    // {
    //     _hasSkip = true;
    // }
    //
    // private void OnEnable()
    // {
    //     StartCoroutine(C_StartCheckTouch()) ;
    // }
    //
    // private IEnumerator C_StartCheckTouch()
    // {
    //     while (!_hasSkip)
    //     {
    //         if (Input.touchCount > 0)
    //         {
    //             SkipCutScene();
    //         }
    //         yield return null;
    //     }
    // }
    
    

}
