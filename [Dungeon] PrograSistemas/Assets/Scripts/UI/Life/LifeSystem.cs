using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    private static LifeSystem instance;
    public static LifeSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LifeSystem>();
                if (instance == null)
                {
                    Debug.LogError("No se encontró el componente LifeSystem en la escena.");
                }
            }
            return instance;
        }
    }

    public int maxLife = 3; // Cantidad máxima de vida
    private int currentLife; // Vida actual
    public Image heartPrefab; // Prefab del corazón
    public Transform heartParent; // Padre para los corazones
    private Stack_TDAPila<Image> hearts = new Stack_TDAPila<Image>(); // Pila de imágenes de los corazones
    private List<Vector3> lostHeartPositions = new List<Vector3>(); // Lista de posiciones de corazones perdidos
    public GameObject player; // Referencia al jugador
    private LevelManager lvlManager;

    void Start()
    {
        lvlManager = FindObjectOfType<LevelManager>();
        
        currentLife = maxLife; // Configura la vida actual al máximo al inicio
        hearts.InicializarPila(maxLife); // Inicializa la pila con el tamaño máximo de vida
        InstantiateHearts(); // Crea y muestra los corazones
    }

    void InstantiateHearts()
    {
        float offsetX = 100f; // Espacio horizontal entre los corazones (tamaño del corazón)
        for (int i = 0; i < maxLife; i++)
        {
            // Crear una nueva instancia del prefab de corazón
            Image newHeart = Instantiate(heartPrefab, heartParent);

            // Ajustar la posición horizontal del corazón
            Vector3 heartPosition = new Vector3(i * offsetX, 0f, 0f);
            newHeart.transform.localPosition = heartPosition;

            // Agregar el corazón a la pila de corazones
            hearts.Apilar(newHeart);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount; // Reduce la vida actual según el daño recibido

        // Oculta y elimina el corazón perdido
        if (currentLife < maxLife)
        {
            Image heartToRemove = hearts.Desapilar();
            lostHeartPositions.Add(heartToRemove.transform.localPosition); // Guarda la posición del corazón perdido
            heartToRemove.gameObject.SetActive(false);
            Destroy(heartToRemove.gameObject);
        }

        if (currentLife <= 0)
        {
            //Debug.LogError("Player is already dead!");

            player.GetComponent<PlayerMov>().isDead = true;
            player.GetComponent<Animator>().SetTrigger("isDead");
            player.GetComponent<PlayerShoot>().canShoot = false;

            StartCoroutine(lvlManager.DefeatScreen(1f)); //Llamo a la función y le paso un delay antes de ejecutarse
        }
    }

    // Restaura la vida del jugador
    public void HealPlayer(int healAmount, GameObject obj)
    {
        for (int i = 0; i < healAmount; i++)
        {
            if (currentLife < maxLife)
            {
                // Obtén la última posición perdida y remuévela de la lista
                Vector3 lastHeartPosition = lostHeartPositions[lostHeartPositions.Count - 1];
                lostHeartPositions.RemoveAt(lostHeartPositions.Count - 1);

                // Crea un nuevo corazón y lo agrega a la pila
                Image newHeart = Instantiate(heartPrefab, heartParent);
                newHeart.gameObject.SetActive(true);
                newHeart.transform.localPosition = lastHeartPosition;
                hearts.Apilar(newHeart);
                currentLife++;
                obj.SetActive(false); // Desactivo el objeto
            }
        }
    }
}

