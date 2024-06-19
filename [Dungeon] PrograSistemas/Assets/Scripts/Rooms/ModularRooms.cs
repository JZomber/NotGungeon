using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class ModularRooms : MonoBehaviour
{
    [SerializeField] private Exit[] exits = new Exit[4];

    [SerializeField] private List<GameObject> doorsList = new List<GameObject>();

    [SerializeField] private RoomConfig roomConfig;
    
    private Vector2 spawnPosition;

    public static event Action<ModularRooms> OnPlayerEnteredRoom;
    //public static event Action<ModularRooms> OnRoomUnlocked; //Si tiene que pasar algo cuando se desbloquea una sala.

    [SerializeField] private Collider2D roomTrigger;
    [SerializeField] private bool unlocked;

    [SerializeField] private GameObject easyRoomDesign;
    [SerializeField] private GameObject hardRoomDesign;
    
    private enum Difficulty { Easy, Hard }
    private Difficulty selectedDifficulty;
    
    private int difficultyPoints;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            UpdateExits();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isPlaying)
        {
            UpdateExits();
            SelectDifficulty();
        }
    }

    public void UnlockRoom()
    {
        unlocked = true;
        UpdateExits();
        roomTrigger.enabled = !unlocked;
        //OnRoomUnlocked?.Invoke(this); //Si tiene que pasar algo cuando se desbloquea una sala.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !unlocked)
        {
            OnPlayerEnteredRoom?.Invoke(this);
        }
    }

    private void SelectDifficulty()
    {
        int randomValue = Random.Range(0, 2);
        selectedDifficulty = (randomValue == 0) ? Difficulty.Easy : Difficulty.Hard;

        if (selectedDifficulty == Difficulty.Easy)
        {
            difficultyPoints = roomConfig.easyDiffPoints;
            Debug.Log($"SALA {this} | DIFICULTAD {selectedDifficulty} | PUNTOS {difficultyPoints}");
            hardRoomDesign.SetActive(false);
            easyRoomDesign.SetActive(true);
        }
        else
        {
            difficultyPoints = roomConfig.hardDiffPoints;
            Debug.Log($"SALA {this} | DIFICULTAD {selectedDifficulty} | PUNTOS {difficultyPoints}");
            
            hardRoomDesign.SetActive(true);
            easyRoomDesign.SetActive(false);
        }
    }

    private void HandlerUpdateExits() //Utilizado para evitar llamadas al mismo frame que se actualiza el editor
    {
        EditorApplication.update -= HandlerUpdateExits;
        UpdateExits();
    }

    private void UpdateExits()
    {
        foreach (Exit exit in exits)
        {
            if (exit != null && exit.exitObject != null && exit.wallObject != null)
            {
                exit.exitObject.SetActive(exit.available);
                exit.wallObject.SetActive(!exit.available);

                foreach (GameObject door in doorsList)
                {
                    if (door != null)
                    {
                        door.SetActive(!unlocked);
                    }
                }

                if (exit.teleportTrigger != null && exit.teleportPosition != null)
                {
                    TeleportTrigger teleportTrigger = exit.teleportTrigger.GetComponent<TeleportTrigger>();
                    
                    teleportTrigger.Initialize(exit.teleportPosition);
                }
            }
        }
    }
    
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            EditorApplication.update += HandlerUpdateExits;
        }
    }
    
    [Serializable]
    public class Exit
    {
        public Direction direction;
        public bool available;
        public GameObject exitObject;
        public GameObject wallObject;
        public Collider2D teleportTrigger;
        public Transform teleportPosition;
    }
    
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
