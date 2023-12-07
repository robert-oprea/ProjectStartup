using UnityEngine;
using UnityEngine.Video;

public class StartMenuAnimation : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public GameObject MainMenu;


    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Play the video
        videoPlayer.Play();

        videoPlayer.loopPointReached += OnVideoEnd;
}

    void OnVideoEnd(VideoPlayer vp)
    {
        // Pause the video at the last frame
        vp.Pause();

        // Activate the main menu
        if (MainMenu != null)
        {
            MainMenu.SetActive(true);
        }
    }

    
}
    

