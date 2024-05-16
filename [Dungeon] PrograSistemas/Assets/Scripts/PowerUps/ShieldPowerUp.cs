using System;
using UnityEngine;

namespace PowerUps
{ 
    public class ShieldPowerUp : MonoBehaviour
    {
        public int damageResist = 5;
        
        private void Update()
        {
            if (damageResist <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                damageResist = 0;
                this.gameObject.SetActive(false);
            }
        }
    }
}
