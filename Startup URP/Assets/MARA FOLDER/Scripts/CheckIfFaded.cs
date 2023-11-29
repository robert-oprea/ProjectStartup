using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfFaded : MonoBehaviour
{

    [SerializeField]
    private SceneSwitch sceneSwitch = null;

    public bool faded1 = false;
    
    public bool faded2 = false; 
    
    public bool faded3 = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(faded1 && faded2 && faded3)
        {
            sceneSwitch.LoadNextLevel();

        }
    }
}
