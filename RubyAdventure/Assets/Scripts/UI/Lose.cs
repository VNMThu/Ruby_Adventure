using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : UIPanel
{
   private Action<object> _onRubyDeathRef;
   private void OnEnable()
   {
      //Open when level up
      _onRubyDeathRef = _ => OnOpen();
      EventDispatcher.Instance.RegisterListener(EventID.OnRubyDeath,_onRubyDeathRef);
   }

   public override void OnOpen()
   {
      //Stop the game
      Time.timeScale = 0f; 
      base.OnOpen();
   }

   public override void OnClose()
   {
      Time.timeScale = 1f;
      base.OnClose();
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
