using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private bool soundActivated = true;

    public bool isSoundActivated()
    {
        return this.soundActivated;
    }

    public void setSoundActivated(bool on)
    {
        this.soundActivated = on;
    }

    //public void disableSound()
    //{
    //    this.soundActivated = false;
    //}

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

    public void resume()
    {
        this.LoadByName("WinScene");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
