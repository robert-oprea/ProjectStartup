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
    public GameObject journal;
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
    public void GoBack()
    {
        settingsMenu.SetActive(false);
        profile.SetActive(false);
        questMenu.SetActive(false);
        mainMenuOpen.SetActive(false);
        journal.SetActive(false);
        Time.timeScale = 1f;
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
    public void JournalMenu()
    {
        profile.SetActive(false);
        journal.SetActive(true);
        mainMenuOpen.SetActive(false);

        Time.timeScale = 0f;
    }
    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Profilemenu()
    {
        profile.SetActive(true);
        mainMenuOpen.SetActive(false);
        Time.timeScale = 0f;
    }
    
    public void QuestMenu()
    {
        questMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}