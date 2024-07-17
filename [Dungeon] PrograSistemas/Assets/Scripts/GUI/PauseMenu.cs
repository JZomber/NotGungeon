using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    public event Action OnMenuRequest;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        
        continueButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(LoadMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        OnMenuRequest?.Invoke();
    }
}
