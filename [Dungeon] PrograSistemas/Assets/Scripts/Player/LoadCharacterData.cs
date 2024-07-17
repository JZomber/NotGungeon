using System;
using System.Collections;
using System.Collections.Generic;
using UI.PowerUps;
using Unity.VisualScripting;
using UnityEngine;

public class LoadCharacterData : MonoBehaviour
{
    [SerializeField] private SelectedCharacter selectedCharacter;
    private Animator animator;
    private int maxLife;
    private PowerUpData starterPowerUp;
    
    public event Action<int> OnSetMaxLife;
    public event Action<PowerUpData> OnSetStarterPowerUp; 
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = selectedCharacter.GetAnimatorController;
        maxLife = selectedCharacter.GetMaxLife;
        starterPowerUp = selectedCharacter.GetStarterPowerUp;
        
        InitializeCharacter();
    }

    private void InitializeCharacter()
    {
        OnSetMaxLife?.Invoke(maxLife);

        if (starterPowerUp)
        {
            OnSetStarterPowerUp?.Invoke(starterPowerUp);
        }
    }
}
