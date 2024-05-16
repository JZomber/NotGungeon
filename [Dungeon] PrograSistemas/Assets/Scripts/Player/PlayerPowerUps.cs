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
    public bool isShieldActive; //Bool si está activo el escudo
    
    private TDA_Queue tdaQueue; //Script de la lista de power ups
    private GameObject powerUp; //Objeto de la lista de power ups

    private LifeSystem lifeSystem; //Script del sistema de vidas

    private PlayerShoot playerShoot; //Script que le permite al player disparar

    private CapsuleCollider2D playerCollider; //Collider del player (Cápsula)

    // Start is called before the first frame update
    void Start()
    {
        shieldPowerUp = shieldPrefab.GetComponent<ShieldPowerUp>(); //Script del escudo (powerUp)
        playerCollider = this.GameObject().GetComponent<CapsuleCollider2D>(); //Collider del player
        tdaQueue = FindObjectOfType<TDA_Queue>(); // Busca el script TDA Queue
        lifeSystem = FindObjectOfType<LifeSystem>(); // Busca el script del TDA Pila
        playerShoot = FindObjectOfType<PlayerShoot>(); // Busca el script que le permite al player disparar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Input - SPACE
        {
            powerUp = tdaQueue.currentPowerUp; //Referencio al primer objeto de la TDA Cola

            if (powerUp.name == "Shield") // Si el objeto es el escudo
            {
                playerCollider.enabled = false;
                shieldPrefab.SetActive(true);
                isShieldActive = true;
            }

            if (powerUp.name == "HealthUp") // Si el objeto es para recuperar vida
            {
                lifeSystem.HealPlayer(2, powerUp.gameObject);
            }
            
            if (powerUp.name == "FastShoot") // Si el objeto es para mayor disparo
            {
                playerShoot.isPowerActive = true;
            }
            
            tdaQueue.RemovePowerUp(); //Quito el objeto del TDA Cola
        }

        if (shieldPowerUp.damageResist <= 0) //Si la resistencia del escudo termina
        {
            isShieldActive = false;
            playerCollider.enabled = true;
            shieldPowerUp.damageResist = 5;
            shieldPrefab.SetActive(false);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp")) // Si el jugador colisiona con un enemigo y puede tomar daño
        {
            tdaQueue.AddPowerUp(other.GameObject());
        }
    }
}
