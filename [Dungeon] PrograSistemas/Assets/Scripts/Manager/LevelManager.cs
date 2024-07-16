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
    }

    public void HandlerVictoryScreen() //Pantalla de victoria
    {
        StartCoroutine(LoadVictoryScreen(1.5f));
        
        OnLevelFinished?.Invoke();

        UnsubscribeEvents();

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.StopMusic();
        // }

        //yield return new WaitForSeconds(1f);

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.PlayVictorySound();
        // }
    }

    private void HandlerDefeatScreen() //Pantalla de derrota
    {
        StartCoroutine(LoadDefeatScreen(1.5f));
        
        OnLevelFinished?.Invoke();

        UnsubscribeEvents();

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.StopMusic();
        // }

        //yield return new WaitForSeconds(1f);

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.PlayDefeatSound();
        // }
    }
    
    private IEnumerator LoadVictoryScreen(float delay)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay); // Espera medio segundo antes de cargar la escena
        
        SceneManager.LoadScene(levelData.VictoryScene);
    }

    private IEnumerator LoadDefeatScreen(float delay)
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(delay); // Espera medio segundo antes de cargar la escena
        
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
    }
}