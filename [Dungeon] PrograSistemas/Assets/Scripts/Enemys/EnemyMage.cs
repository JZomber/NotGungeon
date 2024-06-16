using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMage : MonoBehaviour
{
    [Header("Enemy Attributes")] 
    public int health;
    private int currentHealth;
    public bool isAlive = true;
    [SerializeField] private Animator animator;
    
    private int deadEnemies;

    [SerializeField] private EnemyManager enemyManager;

    private CapsuleCollider2D capsuleCollider2D;

    private Vector2 spawnPoint;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        spawnPoint = transform.position;

        if (enemyManager != null)
        {
            enemyManager.OnMageCalled += MoveToTarget;
            Debug.Log($"{gameObject.name} SE HA SUBSCRITO AL EVENTO OnMageCalled");
        }
        else
        {
            Debug.LogError($"OBJ: {gameObject.name} | REFERENCIA {enemyManager} NO ENCONTRADA");
        }
    }

    private void MoveToTarget(GameObject target)
    {
        Debug.Log($"MOVIENDO HACIA {target}");
        
        gameObject.transform.position = target.transform.position + new Vector3(0, 1, 0);
        StartCoroutine(ReviveTarget(2, target));
    }

    private IEnumerator ReviveTarget(float delay, GameObject target)
    {
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(target.GetComponent<EnemyScript>().EnemyRevive(3f));

        StartCoroutine(Relocate(1.5f));
    }

    private void ActiveColliders() //Controla cuando el mago es vulnerable o no
    {
        if (deadEnemies > 0)
        {
            capsuleCollider2D.enabled = false;
        }
        else
        {
            capsuleCollider2D.enabled = true;
        }
    }

    private void ShieldPower()
    {
        
    }

    private IEnumerator Relocate(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.transform.position = spawnPoint;
    }
    
    public void EnemyDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //levelManager.enemyCounter++;
            capsuleCollider2D.enabled = false;
            animator.SetTrigger("isDead");
            isAlive = false;
            
            enemyManager.OnMageCalled -= MoveToTarget;
        }
    }
}
