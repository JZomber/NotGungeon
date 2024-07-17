using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] public PowerUpData powerUpData;
    [SerializeField] private Sprite defaultIcon;
    private SpriteRenderer spriteRenderer;
    public event Action<PowerUpData> OnCollected;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (spriteRenderer != null && powerUpData != null)
        {
            spriteRenderer.sprite = powerUpData.GetPowerUpIcon;
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.sprite = defaultIcon;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Shield"))
        {
            OnCollected?.Invoke(powerUpData);
            gameObject.SetActive(false);
        }
    }
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        EditorApplication.delayCall += () => 
        {
            if (this != null) 
            {
                UpdateSprite();
            }
        };
    }
#endif
}
