using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldPowerUp", menuName = "PowerUps/Shield")]
public class ShieldPowerUpData : PowerUpData
{
    [SerializeField] private int damageResistance;
    private int currentResistance;
    public int GetDamageResistance => damageResistance;

    public override void ApplyEffect(PlayerPowerUps player)
    {
        player.ActivateShield(this);
    }
}
