using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

//manages different scene switching in the game
public class SceneSwitch : MonoBehaviour
{
    
    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);

        Debug.Log("reloaded scene " + currentSceneName);
    }


    public void GoToNextScene()
    {
        Debug.Log("load next scene :   " + SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void GoToMainScene()
    {
        SceneManager.LoadScene("NEW");
    }


    public void GoToThisScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void LeaveGame()
    {
        Application.Quit();
    }

    public void CheckIfGoodScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            GoToMainScene();
        }
        else
        {
            GoToNextScene();
        }
    }

}
