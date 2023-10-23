using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        RubyController rubyController = collider.GetComponent<RubyController>();

        if (rubyController != null)
        {
            if (rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
