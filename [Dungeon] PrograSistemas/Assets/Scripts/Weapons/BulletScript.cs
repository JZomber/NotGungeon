using System;
using PowerUps;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Weapons
{
    public class BulletScript : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D rb;

        public bool targetEnemy = true;
    
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * speed;
            //Invoke("Deactive", 3f);
        }

        private void Update()
        {
            rb.velocity = transform.up * speed;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (targetEnemy && collision.CompareTag("Enemy")) //Si la bala colisiona Enemy y es un objetivo del mismo
            {
                EnemyScript enemy = collision.GetComponent<EnemyScript>();

                EnemyMage enemyMage = collision.GetComponent<EnemyMage>();

                if (enemy != null)
                {
                    enemy.EnemyDamage(20);
                    Deactive();
                }
                else if (enemyMage != null)
                {
                    enemyMage.EnemyDamage(20);
                    Deactive();
                }
            }

            if (targetEnemy && collision.CompareTag("EnemyShield"))
            {
                Deactive();
            }

            if (!targetEnemy && collision.CompareTag("Shield")) //Si la bala colisiona con un escudo y Enemy no es objetivo
            {
                var shield = collision.gameObject.GetComponent<ShieldPowerUp>();
                shield.TakeDamage(1);
                Deactive();
            }

            if (collision.CompareTag("Wall") || !targetEnemy && collision.CompareTag("Player")) //Colisión con una pared o player
            {
                Deactive();
            }
        }
        void Deactive()
        {
            gameObject.SetActive(false);
        }
    }

    
}
