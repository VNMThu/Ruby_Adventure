using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : UIPanel
{
   private Action<object> _onRubyDeathRef;
   private void OnEnable()
   {
      //Open when level up
      _onRubyDeathRef = _ => OnOpen();
      EventDispatcher.Instance.RegisterListener(EventID.OnRubyDeath,_onRubyDeathRef);
   }

   public override void OnOpen(bool isFromGameplay = true)
   {
      if (PlayerPrefsHelper.GetCurrentCoin()<Constant.CoinNeedToRevive)
      {
         base.OnOpen(isFromGameplay);
      }
   }


   public void OnHomeClick()
   {
         Time.timeScale = 1f;
         SceneManager.LoadScene("Title");
   }

   public void OnRestartClick()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("Gameplay");
   }
}
