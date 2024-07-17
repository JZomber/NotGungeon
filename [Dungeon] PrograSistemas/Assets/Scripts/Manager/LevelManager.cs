using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private LevelData levelData;
    
    private LifeManager lifeManager;
    private PlayerMov playerMov;
    private PauseMenu pauseMenu;

    public event Action OnLevelFinished;

    private void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        if (lifeManager != null)
        {
            lifeManager.OnPlayerDeath += HandlerDefeatScreen;
        }

        playerMov = FindObjectOfType<PlayerMov>();
        if (playerMov != null)
        {
            playerMov.OnPlayerVictory += HandlerVictoryScreen;
        }

        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.OnMenuRequest += HandlerMenuScreen;
        }
    }

    private void HandlerVictoryScreen() //Victory screen
    {
        StartCoroutine(LoadVictoryScreen(1.5f));
        
        OnLevelFinished?.Invoke();

        UnsubscribeEvents();
    }

    private void HandlerDefeatScreen() //Defeat Screen
    {
        StartCoroutine(LoadDefeatScreen(1.5f));
        
        OnLevelFinished?.Invoke();

        UnsubscribeEvents();
    }

    private void HandlerMenuScreen()
    {
        StartCoroutine(LoadMenuScreen(1.5f));
        
        OnLevelFinished?.Invoke();
        
        UnsubscribeEvents();
    }
    
    private IEnumerator LoadMenuScreen(float delay)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(levelData.MenuScene);
    }
    
    private IEnumerator LoadVictoryScreen(float delay)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(levelData.VictoryScene);
    }

    private IEnumerator LoadDefeatScreen(float delay)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay);
        
        SceneManager.LoadScene(levelData.DefeatScene);
    }

    private void UnsubscribeEvents()
    {
        lifeManager = FindObjectOfType<LifeManager>();
        if (lifeManager != null)
        {
            lifeManager.OnPlayerDeath -= HandlerDefeatScreen;
        }

        playerMov = FindObjectOfType<PlayerMov>();
        if (playerMov != null)
        {
            playerMov.OnPlayerVictory -= HandlerVictoryScreen;
        }
        
        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.OnMenuRequest += HandlerMenuScreen;
        }
    }
}