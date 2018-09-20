using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour {

    LevelManager levelManager;
    //SoundManager soundManager;

    
	// Use this for initialization
	void Start () 
    {
        levelManager = FindObjectOfType<LevelManager>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        ScanForKeyStroke();
	}

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelManager.TogglePauseMenu();
        }
    }
}
