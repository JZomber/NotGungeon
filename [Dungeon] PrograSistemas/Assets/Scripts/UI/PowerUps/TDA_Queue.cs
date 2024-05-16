using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PowerUps
{
    public class TDA_Queue : MonoBehaviour
    {
        public Image[] powerUpsImg; //Array de imagenes
        private GameObject currentImg; //Imagen actual
        
        private QueueTdaCola<GameObject> powerUpsQueue = new QueueTdaCola<GameObject>(); //Cola de powerUps
        private int maxSize = 3; //Tamaño del array
        public GameObject currentPowerUp;
        
        public Transform powerUpsParent; //Punto donde aparecen en UI
        public GameObject player; //Referencia al jugador
        
        // Start is called before the first frame update
        void Start()
        {
            powerUpsQueue.InitQueue(maxSize); //Inicializa la lista
        }

        public void AddPowerUp(GameObject obj)
        {
            powerUpsQueue.Acolar(obj); //Agrego la referencia del objeto a la lista
            UpdatePowerUpDisplay();
            obj.SetActive(false);
        }

        public void RemovePowerUp() //Quito el primer objeto que entró a la lista
        {
            powerUpsQueue.DesAcolar();
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
            GameObject firstPowerUp = powerUpsQueue.First(); //Obtengo la referencia al primer objeto en entrar
            currentPowerUp = firstPowerUp;
                
            for (int i = 0; i < powerUpsImg.Length; i++)
            {
                if (firstPowerUp.name == powerUpsImg[i].name) //Comparo el nombre del objeto con el de las imágenes
                {
                    Destroy(currentImg);
                    InstantiatePowerUpUI(powerUpsImg[i].GameObject()); //Muestro la referencia de la imagen
                    break;
                }
            }
        }
    }
}
