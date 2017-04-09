using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenuHelper : MonoBehaviour {

	public void GoToMainMenu()
    {
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}
