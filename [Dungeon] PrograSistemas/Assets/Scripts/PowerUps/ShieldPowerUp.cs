using System;
using UnityEngine;

namespace PowerUps
{ 
    public class ShieldPowerUp : MonoBehaviour
    {
        [SerializeField] private int damageResist = 5;
        private int currentResistance;

        private void Start()
        {
            currentResistance = damageResist;
        }

        public void TakeDamage(int dmg)
        {
            currentResistance--;
            
            if (currentResistance <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                currentResistance = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
