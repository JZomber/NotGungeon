using System;
using System.Collections;
using PowerUps;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private LifeManager lifeManager;
    private bool canTakeDamage = true;

    [SerializeField] private float damageCooldownTime = 2f;

    private CapsuleCollider2D playerCollider;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color damageColor;
    private SpriteRenderer rend; // Player's sprite

    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        rend = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && canTakeDamage && playerCollider.enabled || 
            other.CompareTag("EnemyBullet") && canTakeDamage && playerCollider.enabled)
        {
            lifeManager.TakeDamage(1);
            canTakeDamage = false;
            rend.color = damageColor;
            StartCoroutine(ResetDamageCooldown());
        }
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime);
        canTakeDamage = true;
        rend.color = defaultColor;
    }
}