using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public int healAmount = 1; // Cantidad de vida a curar
    AudioSource source;
    [SerializeField] AudioClip clip;

    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("PlayerObj").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            // Obtener la instancia de LifeSystem
            LifeManager lifeManager = LifeManager.Instance;

            if (lifeManager != null)
            {
                if(source != null && clip!= null) 
                {
                    source.PlayOneShot(clip);
                }
                // Curar al jugador
                lifeManager.HealPlayer(healAmount, gameObject);
            }
            else
            {
                Debug.LogError("No se encontró la instancia de LifeSystem.");
            }
        }
    }
}
