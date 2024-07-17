using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UI.PowerUps;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    private ShieldPowerUp shieldPowerUp;
    private ShieldPowerUpData shieldPowerUpData;
    
    private PowerUpsStack powerUpsStack;
    private GameObject powerUp;

    private LifeManager lifeManager;

    private CapsuleCollider2D playerCollider;
    
    private void Start()
    {
        shieldPowerUp = shieldPrefab.GetComponent<ShieldPowerUp>();
        playerCollider = this.GameObject().GetComponent<CapsuleCollider2D>();
        powerUpsStack = FindObjectOfType<PowerUpsStack>();
        lifeManager = FindObjectOfType<LifeManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Input - SPACE
        {
            var powerUp = powerUpsStack.CheckCurrentPowerUp();

            if (powerUp)
            {
                powerUp.ApplyEffect(this);
            }
        }

        if (!shieldPrefab.activeInHierarchy) // If Shield is destroyed
        {
            playerCollider.enabled = true;
        }
    }

    public void Heal(int healAmount)
    {
        if (!lifeManager.CheckMaxHealth())
        {
            lifeManager.HealPlayer(healAmount, null);
            powerUpsStack.RemovePowerUp();
        }
    }

    public void ActivateShield(ShieldPowerUpData shieldData)
    {
        if (!shieldPrefab.activeInHierarchy)
        {
            shieldPowerUp.SetResistance(shieldData.GetDamageResistance);
            
            shieldPrefab.SetActive(true);
            playerCollider.enabled = false;
            
            powerUpsStack.RemovePowerUp();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            var powerUpPickup = other.GetComponent<PowerUpPickup>();
            if (powerUpPickup != null)
            {
                powerUpsStack.AddPowerUp(powerUpPickup.GetPowerUpData, other.gameObject);
            }
        }
    }
}
