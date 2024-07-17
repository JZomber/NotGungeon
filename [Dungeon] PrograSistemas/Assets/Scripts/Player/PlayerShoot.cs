using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootingOrig;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject weapon;
    private WeaponScript weaponScript;
    private StopTime stopTime;
    public bool canShoot = true;
    

    private void Start()
    {
        weaponScript = weapon.GetComponent<WeaponScript>();
        stopTime = GetComponent<StopTime>();

        if (stopTime == null) 
        {
            return;
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canShoot)
        {
            if (weaponScript != null)
            {
                weaponScript.Shoot(shootingOrig);
            }
        }
        else 
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && canShoot)
            {
                if (weaponScript != null)
                {
                    weaponScript.Shoot(shootingOrig);
                }
            }
        }
        
        if (!canShoot)
        {
            weapon.GameObject().SetActive(false);
        }
    }
}
