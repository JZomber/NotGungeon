using System;
using System.Collections;
using System.Collections.Generic;
using UI.PowerUps;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] public PowerUpData powerUpData;
    private SpriteRenderer spriteRenderer;
    public event Action<PowerUpData> OnCollected;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = powerUpData.GetPowerUpIcon;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke(powerUpData);
            gameObject.SetActive(false);
        }
    }
}
