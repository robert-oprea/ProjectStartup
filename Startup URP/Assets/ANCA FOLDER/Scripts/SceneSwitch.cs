using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//manages different scene switching in the game
public class SceneSwitch : MonoBehaviour
{
    public void LoadLevel(string level)
    {

        Debug.Log("Loaded level: " + level);

        SceneManager.LoadScene(level);

    }

    public void LoadGame(string game)
    {

        Debug.Log("Loaded game: " + game);
        SceneManager.LoadScene(game);

    }

    public void LoadMenu(string menu)
    {

        Debug.Log("Loaded menu: " + menu);
        SceneManager.LoadScene(menu);

    }

    public void LoadReward(string reward)
    {

        Debug.Log("Loaded reward: " + reward);
        SceneManager.LoadScene(reward);

    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
