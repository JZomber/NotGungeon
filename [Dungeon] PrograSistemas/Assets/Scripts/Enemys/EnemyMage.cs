using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public int health;
    private int currentHealth;
    public bool isAlive;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enemyShield;
    [SerializeField] private EnemyManager enemyManager;
    private CapsuleCollider2D capsuleCollider2D;
    private Vector2 spawnPoint;
    private GameObject currentTarget;
    private bool isReviving;
    private List<GameObject> nextTarget = new List<GameObject>();

    public event Action<GameObject> OnMageKilled;

    // Start is called before the first frame update
    void Start()
    {
        EnemySetup();
    }

    private void EnemySetup()
    {
        currentTarget = null;
        isReviving = false;
        isAlive = true;
        currentHealth = health;
        spawnPoint = transform.position;
        
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
            enemyManager.OnMageCalled += HandlerGetNewTarget;
            enemyManager.OnEnemyDespawn += HandlerEnemyDespawn;
        }
        else
        {
            enemyManager.OnMageCalled += HandlerGetNewTarget;
            enemyManager.OnEnemyDespawn += HandlerEnemyDespawn;
        }

        if (capsuleCollider2D == null)
        {
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            capsuleCollider2D.enabled = false;
        }
        
        UpdateColliders();
    }

    private void HandlerGetNewTarget(GameObject target)
    {
        if (currentTarget == null)
        {
            currentTarget = target;
            StartCoroutine(MoveToTarget(target, 1f));
        }
        else
        {
            nextTarget.Add(target);
        }
    }

    private IEnumerator MoveToTarget(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isAlive)
        {
            isReviving = true;
            UpdateColliders();

            gameObject.transform.position = target.transform.position + new Vector3(0, 1, 0);
            StartCoroutine(ReviveTarget(1f, target));
        }
    }

    private IEnumerator ReviveTarget(float delay, GameObject target)
    {
        yield return new WaitForSeconds(delay);

        if (isAlive)
        {
            target.GetComponent<EnemyScript>().EnemyRevive();
            currentTarget = null;
        }

        if (nextTarget.Count > 0 && !currentTarget)
        {
            GetNextTarget();
        }
        else
        {
            StartCoroutine(Relocate(1.5f));
        }
    }

    private void GetNextTarget()
    {
        if (nextTarget.Count > 0)
        {
            currentTarget = nextTarget[0];
            nextTarget.RemoveAt(0);
            StartCoroutine(MoveToTarget(currentTarget, 1f));
        }
    }

    private void UpdateColliders() // Controla cuando el mago/necro es vulnerable o no
    {
        if (!isReviving && isAlive)
        {
            enemyShield.SetActive(true);
            capsuleCollider2D.enabled = false;
        }
        else
        {
            enemyShield.SetActive(false);
            capsuleCollider2D.enabled = true;
        }
    }

    private IEnumerator Relocate(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isAlive)
        {
            isReviving = false;
            UpdateColliders();

            gameObject.transform.position = spawnPoint;
        }
    }

    public void EnemyDamage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;
        }

        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            capsuleCollider2D.enabled = isAlive;
            enemyShield.SetActive(isAlive);
            animator.SetTrigger("isDead");

            // Reproduce el sonido de muerte del enemigo esqueleto
            //SoundManager.Instance.PlayEnemySkeletonDeathSound();

            OnMageKilled?.Invoke(gameObject);
            enemyManager.OnMageCalled -= HandlerGetNewTarget;
        }
    }
    
    private void HandlerEnemyDespawn()
    {
        animator.SetTrigger("despawn");
        enemyManager.OnEnemyDespawn -= HandlerEnemyDespawn;
    }

    private void OnEnable()
    {
        EnemySetup();
    }

    private void OnDisable()
    {
        if (enemyManager != null)
        {
            enemyManager.OnMageCalled -= HandlerGetNewTarget;
        }
        nextTarget.Clear();
    }
}
