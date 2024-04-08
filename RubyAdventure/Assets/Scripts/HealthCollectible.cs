using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();
        if(controller != null && controller.Health < controller.maxHealth){
            controller.ChangeHealth(1);
            controller.PlayAudio(collectedClip);
            gameObject.SetActive(false);
        }
    }
}
