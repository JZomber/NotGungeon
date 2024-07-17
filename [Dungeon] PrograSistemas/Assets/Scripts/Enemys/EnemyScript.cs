using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] private float health;
    private float currentHealth;
    [SerializeField] private float speed;
    [SerializeField] private bool isAlive = true;
    private float damageCooldownTime = 0.1f;
    private bool canTakeDamage = true;
    [SerializeField] private bool isRangedEnemy;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    public float GetHealth => health;
    public float GetCurrentHealth => currentHealth;

    [Header("Player")]
    [SerializeField] private GameObject player;
    private float distance;

    private RangedEnemy rangedEnemy;
    private EnemyManager enemyManager;

    public event Action<GameObject> OnEnemyKilled;
    public event Action OnEnemyRevived;
    [SerializeField] private bool isFaceRight;


    private void Start()
    {
        EnemySetup();
        if (player)
        {
            if (player.transform.position.x > transform.position.x && isFaceRight)
            {
                Flip();
            }

            if (player.transform.position.x < transform.position.x && !isFaceRight)
            {
                Flip();
            }
        }
    }

    private void Update()
    {
        if (player)
        {
            // Chase player
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 20)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    private void Flip()
    {
        // Flip rotation on Y
        
        if (transform.rotation.y >= 180)
        {
            isFaceRight = false;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);
        }
        else 
        {
            isFaceRight = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 180f, transform.rotation.eulerAngles.z);
        }
    }

    private void EnemySetup()
    {
        isAlive = true;
        currentHealth = health;
        
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>();
            enemyManager.OnEnemyDespawn += HandlerEnemyDespawn;
        }
        else
        {
            enemyManager.OnEnemyDespawn += HandlerEnemyDespawn;
        }
        
        if (capsuleCollider2D == null)
        {
            capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        }
        capsuleCollider2D.enabled = false;
        capsuleCollider2D.isTrigger = false;

        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetBool("isAlive", isAlive);
        }
        else
        {
            animator.SetBool("isAlive", isAlive);
        }

        if (isRangedEnemy)
        {
            if (rangedEnemy == null)
            {
                rangedEnemy = gameObject.GetComponent<RangedEnemy>();
            }

            rangedEnemy.isWeaponActive = true;
            StartCoroutine(rangedEnemy.UpdateWeaponStatus(0f));
            StartCoroutine(RangedReset(1f));
        }
    }

    public void EnemyDamage(float damage)
    {
        if (isAlive && canTakeDamage)
        {
            currentHealth -= damage;
            canTakeDamage = false;
            StartCoroutine(ResetDamageCooldown());

            if (currentHealth <= 0)
            {
                isAlive = false;
                capsuleCollider2D.enabled = false;
                animator.SetBool("isAlive", isAlive);
                animator.SetTrigger("isDead");

                OnEnemyKilled?.Invoke(this.gameObject);

                if (isRangedEnemy && gameObject.activeInHierarchy)
                {
                    rangedEnemy.canShoot = false;
                    rangedEnemy.isWeaponActive = false;
                    StartCoroutine(rangedEnemy.UpdateWeaponStatus(0f));
                }
            }
            else 
            {
                animator.SetTrigger("Damaged");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            isAlive = false;
            capsuleCollider2D.enabled = false;
            animator.SetBool("isAlive", isAlive);
            animator.SetTrigger("isDead");

            OnEnemyKilled?.Invoke(gameObject);

            if (isRangedEnemy)
            {
                rangedEnemy.canShoot = false;
                rangedEnemy.isWeaponActive = false;
                StartCoroutine(rangedEnemy.UpdateWeaponStatus(0f));
            }
        }
    }

    public void EnemyRevive()
    {
        if (!isAlive)
        {
            isAlive = true;
            currentHealth = health;
            animator.SetTrigger("isRevived");
            animator.SetBool("isAlive", isAlive);

            if (isRangedEnemy)
            {
                rangedEnemy.isWeaponActive = true;
                StartCoroutine(rangedEnemy.UpdateWeaponStatus(1f));
                StartCoroutine(RangedReset(1.5f));
            }
            OnEnemyRevived?.Invoke();
        }
    }

    private void HandlerEnemyDespawn()
    {
        animator.SetTrigger("despawn");
        enemyManager.OnEnemyDespawn -= HandlerEnemyDespawn;
    }

    private IEnumerator ResetDamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldownTime);
        canTakeDamage = true;
    }

    private IEnumerator RangedReset(float delay)
    {
        yield return new WaitForSeconds(delay);

        capsuleCollider2D.enabled = true;
        rangedEnemy.canShoot = true;
    }

    private void OnEnable()
    {
        EnemySetup();
    }
}
