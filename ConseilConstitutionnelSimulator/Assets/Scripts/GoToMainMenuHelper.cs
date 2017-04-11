using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenuHelper : MonoBehaviour, IPausable{

    private bool m_isPaused = true;
    public void Start()
    {
        GameManager.Instance.OnPause += OnPauseCallBack;
        GameManager.Instance.OnResume += OnResumeCallBack;
    }

	public void GoToMainMenu()
    {
        if (!m_isPaused)
        {
            GameManager.Instance.DestroySaveFile();
            GameManager.Instance.resetOnNewLawLoaded();
            LevelManager.Instance.LoadLevel("MainMenu", null);
        }
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
