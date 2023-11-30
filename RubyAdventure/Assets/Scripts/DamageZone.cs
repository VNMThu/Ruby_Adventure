using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collider)
    {
        RubyController rubyController = collider.GetComponent<RubyController>();

        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }
}
