using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SceneSwitch : MonoBehaviour
{
    public void LoadLevel(string level)
    {

        Debug.Log("Jogo to load: " + level);

        SceneManager.LoadScene(level);

    }

    public void LoadNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Jogo to load: " + SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame(string game)
    {

        Debug.Log("Jogo to load: " + game);
        SceneManager.LoadScene(game);

    }

    public void LoadMenu(string menu)
    {

        Debug.Log("Jogo to load: " + menu);
        SceneManager.LoadScene(menu);

    }

    public void LoadReward(string reward)
    {

        Debug.Log("Jogo to load: " + reward);
        SceneManager.LoadScene(reward);

    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
