using System;
using System.Collections;
using System.Collections.Generic;
using PowerUps;
using UI.PowerUps;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public GameObject shieldPrefab; //Prefab del escudo
    private ShieldPowerUp shieldPowerUp; //Script del powerUp de escudo
    public bool isShieldActive; //Bool si est� activo el escudo
    
    private PowerUpsStack tdaStack; //Script de la lista de power ups
    private GameObject powerUp; //Objeto de la lista de power ups

    private LifeManager lifeManager; //Script del sistema de vidas

    private PlayerShoot playerShoot; //Script que le permite al player disparar

    private CapsuleCollider2D playerCollider; //Collider del player (C�psula)

    // Start is called before the first frame update
    void Start()
    {
        shieldPowerUp = shieldPrefab.GetComponent<ShieldPowerUp>(); //Script del escudo (powerUp)
        playerCollider = this.GameObject().GetComponent<CapsuleCollider2D>(); //Collider del player
        tdaStack = FindObjectOfType<PowerUpsStack>(); // Busca el script TDA Queue
        lifeManager = FindObjectOfType<LifeManager>(); // Busca el script del TDA Pila
        playerShoot = FindObjectOfType<PlayerShoot>(); // Busca el script que le permite al player disparar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Input - SPACE
        {
            powerUp = tdaStack.CheckCurrentPowerUp(); //Referencio al primer objeto de la TDA Cola

            if (powerUp) 
            {
                if (powerUp.name == "Shield") // Si el objeto es el escudo
                {
                    playerCollider.enabled = false;
                    shieldPrefab.SetActive(true);
                    isShieldActive = true;
                }

                if (powerUp.name == "HealthUp") // Si el objeto es para recuperar vida
                {
                    lifeManager.HealPlayer(2, powerUp.gameObject);
                }

                if (powerUp.name == "FastShoot") // Si el objeto es para mayor disparo
                {
                    playerShoot.isPowerActive = true;
                }
                tdaStack.RemovePowerUp(); //Quito el objeto del TDA Cola
            }
        }

        if (!shieldPrefab.activeInHierarchy) //Si la resistencia del escudo termina
        {
            isShieldActive = false;
            playerCollider.enabled = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            tdaStack.AddPowerUp(other.GameObject());
        }
    }
}
