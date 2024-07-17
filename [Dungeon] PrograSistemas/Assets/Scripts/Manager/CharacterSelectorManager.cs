using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacterData playerCharacterData;
    [SerializeField] private SelectedCharacter selectedCharacter;

    public void SelectRogue()
    {
        selectedCharacter.SetupCharacter(playerCharacterData.GetRogueAnimator,
            playerCharacterData.GetRogueMaxLife, playerCharacterData.GetRogueStarterPowerUp);
    }

    public void SelectKnight()
    {
        selectedCharacter.SetupCharacter(playerCharacterData.GetKnightAnimator,
            playerCharacterData.GetKnightMaxLife, playerCharacterData.GetKnightStarterPowerUp);
    }

    public void SelectMage()
    {
        selectedCharacter.SetupCharacter(playerCharacterData.GetMageAnimator,
            playerCharacterData.GetMageMaxLife, playerCharacterData.GetMageStarterPowerUp);
    }

    public void CheckIsCharacterSelected()
    {
        if (!selectedCharacter.GetIsCharacterSelected)
        {
            SelectRogue();
        }
    }
}
