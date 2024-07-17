using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PowerUps
{
    public class PowerUpsStack : MonoBehaviour
    {
        private Stack<PowerUpData> powerUpsStack = new Stack<PowerUpData>(); //Cola de powerUps
        private int maxSize = 3; // Tamaño del array
        public PowerUpData currentPowerUp;
        
        private LoadCharacterData loadCharacterData;
        private LevelManager levelManager;

        public event Action<Sprite> OnPowerUpChanged;

        private void Awake()
        {
            loadCharacterData = FindObjectOfType<LoadCharacterData>();
            if (loadCharacterData != null)
            {
                loadCharacterData.OnSetStarterPowerUp += AddStarterPowerUp;
            }

            levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.OnLevelFinished += HandlerUnsubscribeEvents;
            }
        }

        public PowerUpData CheckCurrentPowerUp()
        {
            if (powerUpsStack.Count == 0)
            {
                return null;
            }
            return currentPowerUp;
        }

        private void AddStarterPowerUp(PowerUpData powerUp)
        {
            powerUpsStack.Push(powerUp);
            CheckUpdateList();
        }

        public void AddPowerUp(PowerUpData powerUp, GameObject obj)
        {
            if (powerUpsStack.Count >= maxSize)
            {
                Debug.Log("La cola está llena");
                return;
            }
            
            powerUpsStack.Push(powerUp);
            obj.SetActive(false);
            CheckUpdateList();
        }

        public void RemovePowerUp()
        {
            if (powerUpsStack.Count == 0)
            {
                return;
            }
            powerUpsStack.Pop();
            CheckUpdateList();
        }

        private void CheckUpdateList()
        {
            if (powerUpsStack.Count == 0)
            {
                currentPowerUp = null;
                OnPowerUpChanged?.Invoke(null);
            }
            else
            {
                currentPowerUp = powerUpsStack.Peek();
                OnPowerUpChanged?.Invoke(currentPowerUp.GetPowerUpIcon);
            }
        }

        private void HandlerUnsubscribeEvents()
        {
            loadCharacterData = FindObjectOfType<LoadCharacterData>();
            if (loadCharacterData != null)
            {
                loadCharacterData.OnSetStarterPowerUp -= AddStarterPowerUp;
            }

            levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.OnLevelFinished -= HandlerUnsubscribeEvents;
            }
        }
    }
}
