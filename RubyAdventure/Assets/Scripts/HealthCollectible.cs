using JSAM;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private RubyController _controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Constant.RubyTag)) return;

        if (_controller == null)
        {
            _controller = GameManager.Instance.Ruby;
        }

        if (_controller.Health >= _controller.MaxHealth) return;
        _controller.ChangeHealth(1);
        AudioManager.PlaySound(AudioLibrarySounds.CoinCollect);
        gameObject.SetActive(false);
    }
}