using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool isSecondBase = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        enemyScript = GetComponent<EnemyScript>();
        rangedEnemy = GetComponent<RangedEnemy>();
        maxHealth = enemyScript.health;
        previousHealth = enemyScript.health;
        isHalfLife = false;
        rangedEnemy.isWeaponActive = false;
    }

    private void Update()
    {
        if (enemyScript != null) 
        {
            
            if (enemyScript.GetCurrentHealth < maxHealth / 2 && !isHalfLife) 
            {
                Debug.Log("Bajo la vida a la mitad");
                isHalfLife=true;
                ActiveBoss();
                for (int i = 0; i < enemys.Count; i++)
                {
                    enemys[i].SetActive(true);
                }
            }

            float currentHealth = enemyScript.GetCurrentHealth;
            if (currentHealth < previousHealth)
            {
                ActiveBoss();
            }
            previousHealth = currentHealth;

            if (isHalfLife) 
            {
                Debug.Log("isHalfLife");
                bool allInactive = true;

                foreach (GameObject go in enemys)
                {
                    if (go.activeSelf)
                    {
                        allInactive = false;
                        break;
                    }
                }

                if (allInactive||enemyScript.GetCurrentHealth <=0) 
                {
                    doors.SetActive(false);
                }
                
            }
        }
    }


    void ActiveBoss() 
    {
        
        int random = Random.Range(0, transforms.Count);
        transform.position = transforms[random].position;
    }
}
