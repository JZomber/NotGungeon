using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private LevelData levelData;
    
    public IEnumerator VictoryScreen(float delay) //Pantalla de victoria
    {
        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.StopMusic();
        // }

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.PlayVictorySound();
        // }

        yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de cargar la escena

        SceneManager.LoadScene(levelData.VictoryScene);
    }

    public IEnumerator DefeatScreen(float delay) //Pantalla de derrota
    {
        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.StopMusic();
        // }

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        // if (BackgroundMusicManager.Instance != null)
        // {
        //     BackgroundMusicManager.Instance.PlayDefeatSound();
        // }

        yield return new WaitForSeconds(0.5f); // Espera medio segundo antes de cargar la escena

        SceneManager.LoadScene(levelData.DefeatScene);
    }
}