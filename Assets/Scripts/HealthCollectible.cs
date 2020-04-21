﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController controller = collision.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);

                
            }
        }
    }
}
