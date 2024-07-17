using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    [SerializeField] List<GameObject> enemys;
    [SerializeField] EnemyScript enemyScript;
    [SerializeField] List<Transform> transforms;
    [SerializeField] GameObject doors;
    RangedEnemy rangedEnemy;
    float maxHealth;
    float previousHealth;
    bool isHalfLife;
    //bool isSecondBase = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyScript = GetComponent<EnemyScript>();
        rangedEnemy = GetComponent<RangedEnemy>();
        
        maxHealth = enemyScript.GetHealth;
        previousHealth = enemyScript.GetHealth;
        isHalfLife = false;
        rangedEnemy.isWeaponActive = false;
    }

    private void Update()
    {
        float currentHealth = enemyScript.GetCurrentHealth;
        if (currentHealth < previousHealth)
        {
            BossTeleport();
        }
        previousHealth = currentHealth;
        
        if (enemyScript.GetCurrentHealth < maxHealth / 2 && !isHalfLife) 
        {
            Debug.Log("Bajo la vida a la mitad");
            isHalfLife=true;
            ActivateEnemies();
        }
        else if (isHalfLife)
        {
            bool allInactive = true;
            foreach (GameObject go in enemys)
            {
                if (go.activeSelf)
                {
                    allInactive = false;
                    break;
                }
            }
            
            if (allInactive || enemyScript.GetCurrentHealth <= 0) 
            {
                doors.SetActive(false);
            }
        }
    }

    private void BossTeleport() 
    {
        int random = Random.Range(0, transforms.Count);
        transform.position = transforms[random].position;
    }

    private void ActivateEnemies()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].SetActive(true);
        }
        
        
    }
}
