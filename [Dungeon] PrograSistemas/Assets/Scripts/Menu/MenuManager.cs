using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator transition;
    
    public void LoadMenu()
    {
        StartCoroutine(MenuScreen("Menu"));
    }
    public void LoadNextLevel() //Carga el primer nivel
    {
        StartCoroutine(LoadLevel(1));
        
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void LoadTutorial()
    {
        StartCoroutine(TutorialLevel("Tutorial"));
    }
    
    IEnumerator MenuScreen(string str) //Carga la pantalla "Men�"
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(str);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator TutorialLevel(string str)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(str);
    }
    
    public void GameQuit() // Quita el juego
    {
        Application.Quit();
    }
}
