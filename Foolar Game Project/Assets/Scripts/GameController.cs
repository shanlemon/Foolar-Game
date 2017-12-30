using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static bool settingsOpen;
    public GameObject settings;

    void Start() {
        settingsOpen = false;
    }
    
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            settingsOpen = !settingsOpen;
            settings.SetActive(settingsOpen);

            if (settingsOpen) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        
            
        }
	}

    public void goBack() {
        settingsOpen = !settingsOpen;
        settings.SetActive(settingsOpen);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
