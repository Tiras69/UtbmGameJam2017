using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour {

    private bool soundActivated = true;

    public bool isSoundActivated()
    {
        return this.soundActivated;
    }

    public void setSoundActivated(bool on)
    {
        this.soundActivated = on;
        SoundManager.Instance.enabled = on;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void LoadByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Resume()
    {
        this.LoadByName("WinScene");
    }

}
