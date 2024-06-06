using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMage : MonoBehaviour
{
    public int deadEnemies;
    [SerializeField] private GameObject target;
    private EnemyScript enemyScript;

    private CapsuleCollider2D capsuleCollider2D;
    private CircleCollider2D circleCollider2D;

    private Vector2 spawnPoint;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        // allEnemies = GameObject.FindGameObjectsWithTag("Enemy"); //Array de enemies
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;

        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (deadEnemies < 0)
        {
            deadEnemies = 0;
        }
        
        if (deadEnemies > 0 && !target)
        {
            ActiveColliders();
        }
        else if (deadEnemies > 0 && target)
        {
            MoveToTarget(target);
        }
    }

    private void MoveToTarget(GameObject target)
    {
        gameObject.transform.position = target.transform.position + new Vector3(0, 1, 0) ;
        
        StartCoroutine(ReviveTarget(2));
    }

    private IEnumerator ReviveTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        StartCoroutine(enemyScript.EnemyRevive(3f));
        
        enemyScript = null;
        target = null;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyDead") && circleCollider2D.enabled)
        {
            target = other.GameObject();
            enemyScript = target.GetComponent<EnemyScript>();
        }
    }

    private void ActiveColliders()
    {
        if (deadEnemies > 0)
        {
            gameObject.tag = "EnemyMage";
            capsuleCollider2D.enabled = false;
            circleCollider2D.enabled = true;
        }
        else
        {
            gameObject.tag = "Enemy";
            capsuleCollider2D.enabled = true;
            circleCollider2D.enabled = false;
        }
    }

    private void ShieldPower()
    {
        
    }
}
