using System;

public class LosePanel : UIPanel
{
   private Action<object> _onRubyDeathRef;
   private void OnEnable()
   {
      //Open when level up
      _onRubyDeathRef = _ => OpenWithCoinCondition();
      EventDispatcher.Instance.RegisterListener(EventID.OnRubyDeath,_onRubyDeathRef);
   }

   private void OpenWithCoinCondition()
   {
      if (PlayerPrefHelper.GetCurrentCoin()<Constant.CoinNeedToRevive)
      {
         base.OnOpen();
      }   
   }


   public void OnHomeClick()
   {
      GameManager.Instance.ReturnToHome();
   }

   public void OnRestartClick()
   {
      OnClose(() =>
      {
         GameManager.Instance.RestartGameplay();
      });
   }
}
