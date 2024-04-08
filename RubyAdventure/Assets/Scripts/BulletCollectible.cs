using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();
        if(controller != null){
            controller.ChangeBullet(5);
            controller.PlayAudio(collectedClip);
            gameObject.SetActive(false);
        }
    }
}
