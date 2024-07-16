using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject disableAbleButton;
    [SerializeField] private GameObject menuButton;

    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == levelData.MenuScene)
        {
            if (levelData.CurrenLevelIndex > 0)
            {
                disableAbleButton.SetActive(true); // Continue button
            }
        }
        
        // End of the game || From Tutorial to Victory
        if (activeScene.name == levelData.VictoryScene)
        {
            if (levelData.CurrenLevelIndex == 3 || levelData.IsTutorialRun)
            {
                disableAbleButton.SetActive(false); // Next button
                menuButton.SetActive(true); // Menu button
            }
        }
        
        // From Tutorial to Defeat
        if (activeScene.name == levelData.DefeatScene)
        {
            if (levelData.IsTutorialRun)
            {
                disableAbleButton.SetActive(false); // Restart button
                menuButton.SetActive(true); // Menu button
            }
        }
    }

    public void LoadMenu()
    {
        StartCoroutine(MenuScreen());
    }

    public void NewStart()
    {
        StartCoroutine(LoadNewStart());
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(ReplayCurrentLevel());
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNewLevel());
    }
    
    public void LoadTutorial()
    {
        StartCoroutine(TutorialLevel());
    }
    
    private IEnumerator MenuScreen()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(levelData.MenuScene);
    }
    
    private IEnumerator ReplayCurrentLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelData.LoadCurrentLevel());
    }

    private IEnumerator LoadNewLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelData.LoadNextLevel());
    }
    
    private IEnumerator LoadNewStart()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelData.NewGameplay());
    }

    private IEnumerator TutorialLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(levelData.LoadTutorial());
    }
    
    public void GameQuit()
    {
        Application.Quit();
    }
}
