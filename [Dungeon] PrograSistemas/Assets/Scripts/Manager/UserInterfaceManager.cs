using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    private WeaponScript weaponScript;
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private Image weaponUI;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponScript = FindObjectOfType<WeaponScript>();
        if (weaponScript != null)
        {
            weaponScript.OnSpriteChanged += HandlerUpdateWeaponUI;
        }
    }

    private void HandlerUpdateWeaponUI(Sprite uiSprite)
    {
        weaponUI.sprite = uiSprite;
    }

    private void OnDisable()
    {
        if (weaponScript != null)
        {
            weaponScript.OnSpriteChanged -= HandlerUpdateWeaponUI;
        }
    }
}
