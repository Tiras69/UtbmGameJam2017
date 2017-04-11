using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour, IPausable {

    private bool soundActivated = true;
    private bool m_isPaused = true;


    public void Start()
    {
        GameManager.Instance.OnPause += OnPauseCallBack;
        GameManager.Instance.OnResume += OnResumeCallBack;
    }

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
        if (!m_isPaused)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit ();
            #endif
        }
    }

    public void LoadByName(string sceneName)
    {
        if( !m_isPaused)
            LevelManager.Instance.LoadLevel(sceneName, null);
    }

    public void Resume()
    {
        this.LoadByName("WinScene");
    }

    public PauseEventResult OnPauseCallBack(PauseEventArgs _args)
    {
        m_isPaused = true;
        return null;
    }

    public PauseEventResult OnResumeCallBack(PauseEventArgs _args)
    {
        m_isPaused = false;
        return null;
    }
}
