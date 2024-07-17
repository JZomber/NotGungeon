using System;
using System.Collections;
using System.Collections.Generic;
using UI.PowerUps;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [Header("Weapons Ui")]
    [SerializeField] private Image weaponUI;
    private WeaponScript weaponScript;

    [Header("Lives Ui")]
    [SerializeField] private Image heartUI;
    [SerializeField] private Transform heartParent;
    private List<Vector3> lostHeartPositions = new List<Vector3>();
    private Stack<Image> heartStack = new Stack<Image>();
    private LifeManager lifeManager;
    private int heartsMaxAmount;

    [Header("PowerUps Ui")] 
    [SerializeField] private Image powerUpUI;
    [SerializeField] private Sprite defaultPowerUpSprite;
    [SerializeField] private Transform powerUpParent;
    private PowerUpsStack powerUpsStack;

    private LevelManager levelManager;

    private void Awake()
    {
        weaponScript = FindObjectOfType<WeaponScript>();
        if (weaponScript != null)
        {
            weaponScript.OnSpriteChanged += HandlerUpdateWeaponUI;
        }
        
        lifeManager = FindObjectOfType<LifeManager>();
        if (lifeManager != null)
        {
            lifeManager.OnHeartLost += HandlerLostHeart;
            lifeManager.OnHeartGained += HandlerGainedHeart;
        }
        
        powerUpsStack = FindObjectOfType<PowerUpsStack>();
        if (powerUpsStack != null)
        {
            powerUpsStack.OnPowerUpChanged += HandlerUpdatePowerUpUI;
        }
        
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnLevelFinished += HandlerUnsubscribeEvents;
        }
    }

    void Start()
    {
        defaultPowerUpSprite = powerUpUI.sprite;

        if (lifeManager != null)
        {
            StartCoroutine(CheckMaxHealth());
        }
    }

    private IEnumerator CheckMaxHealth()
    {
        yield return new WaitForSeconds(0.1f);
        
        heartsMaxAmount = lifeManager.GetMaxLife;
        
        InstantiateHearts();
    }
    
    private void InstantiateHearts()
    {
        float offsetX = 100f;
        for (int i = 0; i < heartsMaxAmount; i++)
        {
            Image newHeart = Instantiate(heartUI, heartParent);
            
            Vector3 heartPosition = new Vector3(i * offsetX, 0f, 0f);
            newHeart.transform.localPosition = heartPosition;
            
            heartStack.Push(newHeart);
        }
    }

    private void HandlerLostHeart()
    {
        if (heartStack.Count > 0)
        {
            Image heartToRemove = heartStack.Pop();
            lostHeartPositions.Add(heartToRemove.transform.localPosition); // Guarda la posición del corazón perdido
            heartToRemove.gameObject.SetActive(false);
            Destroy(heartToRemove.gameObject);
        }
    }
    
    private void HandlerGainedHeart()
    {
        Vector3 lastHeartPosition = lostHeartPositions[lostHeartPositions.Count - 1];
        lostHeartPositions.RemoveAt(lostHeartPositions.Count - 1);
        
        Image newHeart = Instantiate(heartUI, heartParent);
        newHeart.gameObject.SetActive(true);
        newHeart.transform.localPosition = lastHeartPosition;
        heartStack.Push(newHeart);
    }

    private void HandlerUpdateWeaponUI(Sprite uiSprite)
    {
        weaponUI.sprite = uiSprite;
    }

    private void HandlerUpdatePowerUpUI(Sprite uiSprite)
    {
        if (uiSprite)
        {
            powerUpUI.sprite = uiSprite;
        }
        else
        {
            powerUpUI.sprite = defaultPowerUpSprite;
        }
    }

    private void HandlerUnsubscribeEvents()
    {
        if (weaponScript != null)
        {
            weaponScript.OnSpriteChanged -= HandlerUpdateWeaponUI;
        }
        
        if (lifeManager != null)
        {
            lifeManager.OnHeartLost -= HandlerLostHeart;
            lifeManager.OnHeartGained -= HandlerGainedHeart;
        }
        
        powerUpsStack = FindObjectOfType<PowerUpsStack>();
        if (powerUpsStack != null)
        {
            powerUpsStack.OnPowerUpChanged -= HandlerUpdatePowerUpUI;
        }
        
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnLevelFinished -= HandlerUnsubscribeEvents;
        }
    }
}
