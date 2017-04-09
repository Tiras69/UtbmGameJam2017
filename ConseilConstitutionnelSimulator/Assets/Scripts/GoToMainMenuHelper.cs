using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenuHelper : MonoBehaviour {

	public void GoToMainMenu()
    {
        GameManager.Instance.resetOnNewLawLoaded();
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}
