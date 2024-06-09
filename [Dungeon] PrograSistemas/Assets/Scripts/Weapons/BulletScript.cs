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
            Invoke("DisableItself", 3f);
            //Destroy(gameObject, 3);
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

                if (enemy != null)
                {
                    enemy.EnemyDamage(20);
                    gameObject.SetActive(false);
                }
            }

            if (!targetEnemy && collision.CompareTag("Shield")) //Si la bala colisiona con un escudo y Enemy no es objetivo
            {
                var shield = collision.gameObject.GetComponent<ShieldPowerUp>();
                shield.damageResist -= 1;
                gameObject.SetActive(false);
            }

            if (!targetEnemy && collision.CompareTag("Player")) //Colisión con una pared o player
            {
                gameObject.SetActive(false);
            }

            if (collision.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
        }

        void DisableItself() 
        {
            gameObject.SetActive(false);
        }
    }
}
