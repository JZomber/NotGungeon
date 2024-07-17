using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedCharacterData", menuName = "ScriptableObjects/Character/Selected Character", order = 1)]
public class SelectedCharacter : ScriptableObject
{
    [Header("Selected character data")]
    [SerializeField] private RuntimeAnimatorController animatorController;
    [SerializeField] private int maxLife;
    [SerializeField] private PowerUpData starterPowerUp;
    [SerializeField] private bool isCharacterSelected;

    public RuntimeAnimatorController GetAnimatorController => animatorController;
    public int GetMaxLife => maxLife;
    public PowerUpData GetStarterPowerUp => starterPowerUp;
    public bool GetIsCharacterSelected => isCharacterSelected;

    public void SetupCharacter(RuntimeAnimatorController p_animator, int p_maxLife, PowerUpData p_powerUpData)
    {
        animatorController = p_animator;
        maxLife = p_maxLife;
        starterPowerUp = p_powerUpData;
        isCharacterSelected = true;
    }

    public void GetLastSelectedCharacter()
    {
        animatorController = null;
        maxLife = 0;
        starterPowerUp = null;
        isCharacterSelected = false;
    }
}
