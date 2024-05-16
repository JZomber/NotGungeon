using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform shootingOrig; //Origen de las balas
    public GameObject bulletPrefab; //Prefab de las balas (player)
    public GameObject weapon;

    public bool isPowerActive; //Poder de disparo
    private float timePowerUp = 6f; //Duración del power up

    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (isPowerActive) //Si el poder está activo
        {
            var dt = Time.deltaTime;
            timePowerUp -= dt;
            
            if (timePowerUp <= 0f)
            {
                isPowerActive = false;
                timePowerUp = 6f;
                //Debug.LogError("Power Shoot desactivado");
            }
        }
        
        if (Mouse.current.leftButton.wasPressedThisFrame && canShoot) //Cada vez que se presione el mouse
        {
            StartCoroutine(PlayerShooting(bulletPrefab, shootingOrig, 0.15f)); //Prefab, Origen, Delay
        }
        
        if (!canShoot)
        {
         weapon.GameObject().SetActive(false);
        }
    }

    private IEnumerator PlayerShooting(GameObject prefab, Transform orig, float delay)
    {
        var rotation = orig.rotation;
        rotation *=  Quaternion.Euler(0, 0, -90);
        Instantiate(prefab, orig.position, rotation);
            
        if (isPowerActive) //Si el poder está activo, instancia otra bala
        {
            yield return new WaitForSeconds(delay);
            Instantiate(prefab, orig.position, rotation);
        }
    }
}
