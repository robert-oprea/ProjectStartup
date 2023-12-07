using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject openVideoPlayer;    // Reference to the "OpenVideoPlayer" GameObject
    public GameObject closeVideoPlayer;   // Reference to the "CloseVideoPlayer" GameObject
    public GameObject flipOptionsPlayer;
    public GameObject backVideoPlayer;
    public GameObject optionsMenu;
    public GameObject mainMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }
    public void OpenOptions()
    {
        // Deactivate the MainMenu GameObject
        gameObject.SetActive(false);
        // Activate the RawImage associated with OptionsVideoPlayer if it's available
        if (flipOptionsPlayer != null)
        {
            RawImage optionsRawImage = flipOptionsPlayer.GetComponentInChildren<RawImage>();
            if (optionsRawImage != null)
            {
                optionsRawImage.gameObject.SetActive(true);
            }

            openVideoPlayer.SetActive(false);
            closeVideoPlayer.SetActive(false);
            backVideoPlayer.SetActive(false);
             flipOptionsPlayer.SetActive(true);

             VideoPlayer flipOptionsPlayerComponent = flipOptionsPlayer.GetComponent<VideoPlayer>();
            if (flipOptionsPlayerComponent != null)
            {
                VideoClip quitVideoClip = Resources.Load<VideoClip>("BookPageFlipR");
                if (quitVideoClip != null)
                {
                    flipOptionsPlayerComponent.clip = quitVideoClip;
                    flipOptionsPlayerComponent.Play();
                }
                flipOptionsPlayerComponent.loopPointReached += FlipRightDone;
                
            }
        }
    }
    void FlipRightDone(VideoPlayer vp)
    {
        vp.loopPointReached -= FlipRightDone;
    // Activate the Options menu GameObject
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(true);
        }
    }
     public void GoBack()
    {
        // Deactivate the MainMenu GameObject
        gameObject.SetActive(false);
        // Activate the RawImage associated with OptionsVideoPlayer if it's available
        if (backVideoPlayer != null)
        {
            RawImage optionsRawImage = backVideoPlayer.GetComponentInChildren<RawImage>();
            if (optionsRawImage != null)
            {
                optionsRawImage.gameObject.SetActive(true);
            }

            flipOptionsPlayer.SetActive(false);
            openVideoPlayer.SetActive(false);
            closeVideoPlayer.SetActive(false);
            backVideoPlayer.SetActive(true);

             VideoPlayer backVideoPlayerComponent = backVideoPlayer.GetComponent<VideoPlayer>();
            if (backVideoPlayerComponent != null)
            {
                VideoClip quitVideoClip = Resources.Load<VideoClip>("BookPageFlipR");
                if (quitVideoClip != null)
                {
                    backVideoPlayerComponent.clip = quitVideoClip;
                    backVideoPlayerComponent.Play();
                }
                backVideoPlayerComponent.loopPointReached += FlipLeftDone;
            }
        }
    }

    void FlipLeftDone(VideoPlayer vp)
    {
        vp.loopPointReached -= FlipLeftDone;
    // Activate the Options menu GameObject
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }

    }
    public void QuitGame()
    {
        {
        // Deactivate the MainMenu GameObject
        gameObject.SetActive(false);
        // Activate the RawImage associated with CloseVideoPlayer if it's available
        if (closeVideoPlayer != null)
        {
            RawImage closeRawImage = closeVideoPlayer.GetComponentInChildren<RawImage>();
            if (closeRawImage != null)
            {
                closeRawImage.gameObject.SetActive(true);
            }

            backVideoPlayer.SetActive(false);
            flipOptionsPlayer.SetActive(false);
            openVideoPlayer.SetActive(false);
            closeVideoPlayer.SetActive(true);

            // Play the video on the CloseVideoPlayer if it's available
            VideoPlayer closeVideoPlayerComponent = closeVideoPlayer.GetComponent<VideoPlayer>();
            if (closeVideoPlayerComponent != null)
            {
                VideoClip quitVideoClip = Resources.Load<VideoClip>("BookClosing");
                if (quitVideoClip != null)
                {
                    closeVideoPlayerComponent.clip = quitVideoClip;
                    closeVideoPlayerComponent.Play();
                }
                closeVideoPlayerComponent.loopPointReached += OnBookClosingEnd;
                
            }
        }
    }
    }
     void OnBookClosingEnd(VideoPlayer vp)
    {
        // Unsubscribe from the event to avoid multiple calls
        vp.loopPointReached -= OnBookClosingEnd;

        // Quit the game when the animation is done
        Debug.Log("quit");
        Application.Quit();
    }
}
