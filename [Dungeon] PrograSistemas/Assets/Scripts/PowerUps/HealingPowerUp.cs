using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingPowerUp", menuName = "PowerUps/Healing")]
public class HealingPowerUp : PowerUpData
{
    [SerializeField] private int healingAmount;

    public override void ApplyEffect(PlayerPowerUps player)
    {
        player.Heal(healingAmount);
    }
}