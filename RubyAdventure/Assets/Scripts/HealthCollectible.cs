using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private RubyController _controller;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("Player")) return;

        if (_controller == null)
        {
            _controller = GameManager.Instance.Ruby;
        }

        if (_controller.Health >= _controller.MaxHealth) return;
        _controller.ChangeHealth(1);
        _controller.PlayAudio(collectedClip);
        gameObject.SetActive(false);
    }
}
