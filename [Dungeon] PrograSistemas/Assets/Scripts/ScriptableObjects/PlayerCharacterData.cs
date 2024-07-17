using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterData", menuName = "ScriptableObjects/Character/Player Character Data", order = 1)]
public class PlayerCharacterData : ScriptableObject
{
    [Header("Rogue data")]
    [SerializeField] private RuntimeAnimatorController rogueAnimator;
    [SerializeField] private int rogueMaxLife;
    [SerializeField] private PowerUpData rogueStarterPowerUp;

    public RuntimeAnimatorController GetRogueAnimator => rogueAnimator;
    public int GetRogueMaxLife => rogueMaxLife;
    public PowerUpData GetRogueStarterPowerUp => rogueStarterPowerUp;
    
    [Header("Knight data")]
    [SerializeField] private RuntimeAnimatorController knightAnimator;
    [SerializeField] private int knightMaxLife;
    [SerializeField] private PowerUpData knightStarterPowerUp;

    public RuntimeAnimatorController GetKnightAnimator => knightAnimator;
    public int GetKnightMaxLife => knightMaxLife;
    public PowerUpData GetKnightStarterPowerUp => knightStarterPowerUp;
    
    [Header("Mage data")]
    [SerializeField] private RuntimeAnimatorController mageAnimator;
    [SerializeField] private int mageMaxLife;
    [SerializeField] private PowerUpData mageStarterPowerUp;

    public RuntimeAnimatorController GetMageAnimator => mageAnimator;
    public int GetMageMaxLife => mageMaxLife;
    public PowerUpData GetMageStarterPowerUp => mageStarterPowerUp;
}