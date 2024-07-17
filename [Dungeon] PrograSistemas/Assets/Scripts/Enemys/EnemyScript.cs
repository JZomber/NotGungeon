using System;
using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public float health;
    private float currentHealth;
    public float speed;
    public bool isAlive = true;
    public bool isRangedEnemy;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    [Header("Player")]
    public GameObject player;
    private float distance;

    private RangedEnemy rangedEnemy;
    //private SoundManager soundManager; // Referencia al SoundManager
    private EnemyManager enemyManager;

    public event Action<GameObject> OnEnemyKilled;
    public event Action OnEnemyRevived;
    public bool isFaceRight;

    private void Start()
    {
        EnemySetup();
        //soundManager = SoundManager.Instance; // Obtener instancia del SoundManager
    }

    void Update()
    {
        if (player)
        {
            
            // Perseguir al Jugador
            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 20)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }

            if (player.transform.position.x > transform.position.x && isFaceRight)
            {
                isFaceRight = false;
                transform.rotation = Quaternion.Euler(transform.rotation.x , transform.rotation.y + 180f, transform.rotation.z);
            }
            else if (player.transform.position.x < transform.position.x && !isFaceRight) 
            {
                isFaceRight = true;
                transform.rotation = Quaternion.Euler(transform.rotation.x , transform.rotation.y + 180f, transform.rotation.z);
            } ;
            
        }

        
    }

    private bool isFaceRightPlayer() 
    {
        if (player == null) 
        {
            return false;
        }
        return player.transform.position.x < transform.position.x;
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

    // Daño del Enemigo
    public void EnemyDamage(float damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                isAlive = false;
                capsuleCollider2D.enabled = false;
                animator.SetTrigger("isDead");

                OnEnemyKilled?.Invoke(this.gameObject);

                // Reproducir el sonido de muerte del enemigo
                // if (soundManager != null)
                // {
                //     soundManager.PlayEnemySkeletonDeathSound();
                // }

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
            capsuleCollider2D.enabled = false;
            animator.SetTrigger("isDead");
            isAlive = false;

            OnEnemyKilled?.Invoke(gameObject);

            // Reproducir el sonido de muerte del enemigo
            // if (soundManager != null)
            // {
            //     soundManager.PlayEnemySkeletonDeathSound();
            // }

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
            //SoundManager.Instance.PlayEnemyReviveSound();

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
