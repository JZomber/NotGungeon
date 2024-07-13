using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PowerUps
{
    public class PowerUpsStack : MonoBehaviour
    {
        public Image[] powerUpsImg; //Array de imagenes
        private GameObject currentImg; //Imagen actual

        private Stack<GameObject> powerUpsStack = new Stack<GameObject>(); //Cola de powerUps
        private int maxSize = 3; // Tamaño del array
        public GameObject currentPowerUp;
        
        public Transform powerUpsParent; //Punto donde aparecen en UI
        
        // Start is called before the first frame update
        void Start()
        {
        }
        
        public GameObject CheckCurrentPowerUp()
        {
            if (powerUpsStack.Count == 0)
            {
                return null;
            }

            return currentPowerUp;
        }

        public void AddPowerUp(GameObject obj)
        {
            if (powerUpsStack.Count >= maxSize)
            {
                Debug.Log("La cola está llena");
                return;
            }
            
            powerUpsStack.Push(obj); //Agrego la referencia del objeto a la lista
            UpdatePowerUpDisplay();
            obj.SetActive(false);
        }

        public void RemovePowerUp() //Quito el primer objeto que entr� a la lista
        {
            if (powerUpsStack.Count == 0)
            {
                return;
            }
            
            powerUpsStack.Pop();
            Destroy(currentImg);
            currentPowerUp = null;
            UpdatePowerUpDisplay();
        }

        private void InstantiatePowerUpUI(GameObject img)
        {
            currentImg = Instantiate(img, powerUpsParent); //Instancio la imagen del primer objeto en la lista
        }

        private void UpdatePowerUpDisplay()
        {
            if (powerUpsStack.Count == 0)
            {
                currentPowerUp = null;
                return;
            }

            currentPowerUp = powerUpsStack.Peek();
            for (int i = 0; i < powerUpsImg.Length; i++)
            {
                if (currentPowerUp != null && currentPowerUp.name == powerUpsImg[i].name) //Comparo el nombre del objeto con el de las imagenes
                {
                    Destroy(currentImg);
                    InstantiatePowerUpUI(powerUpsImg[i].gameObject); //Muestro la referencia de la imagen
                    break;
                }
            }
        }
    }
}
