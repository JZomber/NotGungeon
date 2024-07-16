using System;
using PowerUps;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private LifeManager lifeManager;
    private bool canTakeDamage = true;

    public float damageCooldownTime = 2f; // Tiempo de espera antes de volver a tomar 

    private CapsuleCollider2D playerCollider;

    public Color defaultColor; //Color default del player
    public Color damageColor; // Color que indica cuando el player recibe da�o
    private SpriteRenderer rend; //Sprite del player

    private PlayerPowerUps playerPowerUps;

    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>(); // Encuentra el script LifeSystem
        rend = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerPowerUps = FindObjectOfType<PlayerPowerUps>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && canTakeDamage && playerCollider.enabled || 
            other.CompareTag("EnemyBullet") && canTakeDamage && playerCollider.enabled) // Si el jugador colisiona con un enemigo / bullet y puede tomar da�o
        {
            lifeManager.TakeDamage(1); // Reduce la vida del jugador en 1
            canTakeDamage = false; // Deshabilita la capacidad de tomar da�o temporalmente
            rend.color = damageColor; // Cambio el color del player
            StartCoroutine(ResetDamageCooldown()); // Espera un tiempo antes de permitir que el jugador tome da�o nuevamente
        }
    }

    private void OnTriggerStay2D(Collider2D other) // Si el jugador se queda encima de una trampa
    {
        if (other.CompareTag("Trap") && canTakeDamage && playerPowerUps.isShieldActive == false) //Si el jugador colisiona y/o se queda en la trampa, recibe da�o
        {
            lifeManager.TakeDamage(1); // Reduce la vida del jugador en 1
            canTakeDamage = false; // Deshabilita la capacidad de tomar da�o temporalmente
            rend.color = damageColor;// Cambio el color del player
            StartCoroutine(ResetDamageCooldown()); // Espera un tiempo antes de permitir que el jugador tome da�o nuevamente
        }
    }

    System.Collections.IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime); // Espera el tiempo especificado
        canTakeDamage = true; // Permite que el jugador tome da�o nuevamente
        rend.color = defaultColor; // Reinicio el color
    }
}