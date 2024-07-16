using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpData : ScriptableObject
{
    [SerializeField] private string powerUpName;
    [SerializeField] private Sprite powerUpIcon;

    public string GetPowerUpName => powerUpName;
    public Sprite GetPowerUpIcon => powerUpIcon;
    
    public abstract void ApplyEffect(PlayerPowerUps player);
}
