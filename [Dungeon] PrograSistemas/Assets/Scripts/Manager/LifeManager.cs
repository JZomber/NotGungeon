using System;
using System.Collections;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int maxLife; // Cantidad máxima de vida
    public int GetMaxLife => maxLife;
    
    private int currentLife; // Vida actual
    [SerializeField] private GameObject player; // Referencia al jugador
    private LoadCharacterData loadCharacterData;

    private LevelManager levelManager;

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

    private void Awake()
    {
        loadCharacterData = FindObjectOfType<LoadCharacterData>();
        if (loadCharacterData != null)
        {
            loadCharacterData.OnSetMaxLife += SetMaxLife;
        }

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnLevelFinished += HandlerUnsubscribeEvents;
        }
    }

    private void SetMaxLife(int amount)
    {
        maxLife = amount;
        currentLife = maxLife;
    }

    public bool CheckMaxHealth()
    {
        return currentLife == maxLife;
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
            player.GetComponent<PlayerMov>().SetPlayerIsDead();
            player.GetComponent<Animator>().SetTrigger("isDead");
            player.GetComponent<PlayerShoot>().canShoot = false;

            OnPlayerDeath?.Invoke();
        }
    }

    public void HealPlayer(int healAmount, GameObject obj)
    {
        if (currentLife == maxLife)
        {
            return;
        }
        
        for (int i = 0; i < healAmount; i++)
        {
            if (currentLife < maxLife)
            {
                OnHeartGained?.Invoke();
                currentLife++;

                if (obj)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    
    private void HandlerUnsubscribeEvents()
    {
        loadCharacterData = FindObjectOfType<LoadCharacterData>();
        if (loadCharacterData != null)
        {
            loadCharacterData.OnSetMaxLife -= SetMaxLife;
        }

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnLevelFinished -= HandlerUnsubscribeEvents;
        }
    }
}

