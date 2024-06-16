using System;

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
      if (PlayerPrefHelper.GetCurrentCoin()<Constant.CoinNeedToRevive)
      {
         base.OnOpen(isFromGameplay);
      }
   }


   public void OnHomeClick()
   {
         GameManager.Instance.ReturnToHome();
   }

   public void OnRestartClick()
   {
      GameManager.Instance.RestartGameplay();
   }
}
