using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject questMenu;
    public GameObject mainHUD;
    public GameObject mainMenuOpen;
    public GameObject settingsMenu;
    public GameObject profile;
    // Start is called before the first frame update
    void Start()
    {
        // Add an EventTrigger component to your quest menu panel
        EventTrigger eventTrigger = mainMenuOpen.AddComponent<EventTrigger>();

        // Add a PointerClick event to the EventTrigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPanelClick(); });

        // Add the entry to the EventTrigger's triggers list
        eventTrigger.triggers.Add(entry);
    }

    void OnPanelClick()
    {
        // Close the quest menu overlay
        mainMenuOpen.SetActive(false);
    }


    public void MainMenu()
    {
        mainMenuOpen.SetActive(true);
        
    }
    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Profilemenu()
    {
        profile.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoBack()
    {
        settingsMenu.SetActive(false);
        profile.SetActive(false);
        questMenu.SetActive(false);
         Time.timeScale = 1f;
    }
    
    public void QuestMenu()
    {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    

}
