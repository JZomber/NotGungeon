using System;
using UnityEngine;

namespace PowerUps
{ 
    public class ShieldPowerUp : MonoBehaviour
    {
        private int currentResistance;

        public void SetResistance(int resistance)
        {
            currentResistance = resistance;
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
