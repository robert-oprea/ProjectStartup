using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {

    public static event Action<Story> OnCreateStory;

	[SerializeField]
	private TextAsset inkJSONAsset = null;
	private Story story;

	SceneSwitch sceneSwitch;

    private void Start()
    {
         sceneSwitch = GetComponent<SceneSwitch>();
    }

    void Awake()
	{
		// Remove the default message
		RemoveChildren();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory()
	{
		// Instead of creating a new Story with a fixed JSONAsset, use the assigned JSON file
		if (inkJSONAsset != null)
		{
			story = new Story(inkJSONAsset.text);
		}
		else
		{
			Debug.LogError("No JSON file assigned to the NPC!");
			return;
		}

		// Trigger the event to inform other scripts about the new story
		if (OnCreateStory != null)
		{
			OnCreateStory(story);
		}

		RefreshView();
	}

	public Story GetStory()
    {
		return story;
    }

	public void SetStoryJSON(TextAsset jsonAsset)
    {
		inkJSONAsset = jsonAsset;
    }

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if(story.currentChoices.Count > 0) {

			for (int i = 0; i < story.currentChoices.Count; i++) {

				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);

					/*if(!story.canContinue && i == story.currentChoices.Count - 1)
                    {
						if(OnLastChoiceMade != null)
                        {
							OnLastChoiceMade();
                        }
                    }*/
				});
			}
		}
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("Start");
            choice.onClick.AddListener(delegate
            {
                RemoveChildren();
                StartCoroutine(cameraSwitcher.SwitchBackToMainCamera());


				//Debug.Log("pressed");
            });
        }
    }

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		if (choice.tags != null && choice.tags.Count > 0)  // TODO: Deal with multiple tags??
		{
			Debug.Log("Stuff happens for tag " + choice.tags[0]);
			string[] words = choice.tags[0].Split(':');
			if (words.Length == 2)
			{
				string command = words[0];
				string value = words[1];

                switch (command)
                {
                    case "startQuest":
                        var qm = FindObjectOfType<QuestManager>();
                        if (qm != null)
                        {
                            qm.StartQuest(value);
                        }
                        break;

                    case "juan":
						Debug.Log("chose juan option");
						sceneSwitch.GoToThisScene("Fruit SCENE 1");
                        break;

					case "bootcamp":
						Debug.Log("entered bootcamp");
						sceneSwitch.GoToThisScene("DragDrop SCENE");
						break;

                    default:
                        Debug.Log("WARNING: Command not recognized: " + command);
                        break;
                }
            }
			else
			{
				Debug.Log("WARNING: Tag is not in command:value form");
			}
		}
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}
	


	// Creates a textbox showing the the line of text
	void CreateContentView (string text) {
		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = true;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	public void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

	/*[SerializeField]
	private TextAsset inkJSONAsset = null;
	public Story story;*/

	[SerializeField]
	private Canvas canvas = null;

	// UI Prefabs
	[SerializeField]
	private Text textPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;

	[SerializeField]
	private CameraSwitcher cameraSwitcher = null;

	/*[SerializeField]
	private SceneSwitch sceneSwitch = null;*/
}
