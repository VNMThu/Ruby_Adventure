using JSAM;
using UnityEngine;

public class BulletCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            controller.ChangeBullet(5);
            AudioManager.PlaySound(AudioLibrarySounds.CoinCollect);
            gameObject.SetActive(false);
        }
    }
}