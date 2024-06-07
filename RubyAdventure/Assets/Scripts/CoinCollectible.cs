using JSAM;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [SerializeField] private int coinValueWhenPickup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller == null) return;
        controller.ChangeCoin(coinValueWhenPickup);
        AudioManager.PlaySound(AudioLibrarySounds.CoinCollect);
        gameObject.SetActive(false);
    }
}