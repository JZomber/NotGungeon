using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

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
        
        isAlive = true;
        currentHealth = health;
        enemyShield.SetActive(true);
        spawnPoint = transform.position;
        
        enemyManager = FindObjectOfType<EnemyManager>();
        
        if (enemyManager != null)
        {
            enemyManager.OnMageCalled += HandlerGetNewTarget;
            //Debug.Log($"{gameObject.name} SE HA SUBSCRITO AL EVENTO OnMageCalled");
        }

        if (capsuleCollider2D == null)
        {
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            capsuleCollider2D.enabled = false;
        }
    }

    private void HandlerGetNewTarget(GameObject target)
    {
        //Debug.Log($"MOVIENDO HACIA {target}");

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

    private void UpdateColliders() //Controla cuando el mago/necro es vulnerable o no
    {
        if (isReviving && isAlive)
        {
            enemyShield.SetActive(false);
            capsuleCollider2D.enabled = true;
        }
        else
        {
            enemyShield.SetActive(true);
            capsuleCollider2D.enabled = false;
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
            
            OnMageKilled?.Invoke(gameObject);
            enemyManager.OnMageCalled -= HandlerGetNewTarget;
        }
    }

    private void OnEnable()
    {
        EnemySetup();
    }

    private void OnDisable()
    {
        enemyManager.OnMageCalled -= HandlerGetNewTarget;
        nextTarget.Clear();
    }
}
