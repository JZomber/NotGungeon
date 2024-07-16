using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int maxLife; // Cantidad máxima de vida
    public int GetMaxLife => maxLife;
    
    private int currentLife; // Vida actual
    [SerializeField] private GameObject player; // Referencia al jugador

    public event Action OnHeartLost;
    public event Action OnHeartGained;
    public event Action OnPlayerDeath;
    
    private static LifeManager instance;
    public static LifeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LifeManager>();
                if (instance == null)
                {
                    Debug.LogError("No se encontró el componente LifeSystem en la escena.");
                }
            }
            return instance;
        }
    }

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        
        if (currentLife < maxLife)
        {
            OnHeartLost?.Invoke();
        }

        if (currentLife <= 0)
        {
            player.GetComponent<PlayerMov>().isDead = true;
            player.GetComponent<Animator>().SetTrigger("isDead");
            player.GetComponent<PlayerShoot>().canShoot = false;

            OnPlayerDeath?.Invoke();
        }
    }

    public void HealPlayer(int healAmount)
    {
        for (int i = 0; i < healAmount; i++)
        {
            if (currentLife < maxLife)
            {
                OnHeartGained?.Invoke();
                currentLife++;
            }
        }
    }
}

