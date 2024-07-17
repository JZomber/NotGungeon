using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            LifeManager lifeManager = LifeManager.Instance;

            if (lifeManager != null)
            {
               lifeManager.HealPlayer(healAmount, gameObject);
            }
            else
            {
                Debug.LogError("Life system instance not found.");
            }
        }
    }
}
