using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ManageJauge : MonoBehaviour, IPausable {
    public Slider ValueSlider;  //reference for slider
    private bool m_isPaused;
	// Use this for initialization
	void Start () {
        m_isPaused = true;
        GameManager.Instance.OnPause += OnPauseCallBack;
        GameManager.Instance.OnResume += OnResumeCallBack;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
 
    public void ChangeValue(int val)
    {
        ValueSlider.value += val;
        if (ValueSlider.value <= 0)
        {
            if (!m_isPaused)
            {
                GameManager.Instance.InitNewGame();
                LevelManager.Instance.LoadLevel("LoseScene", null);
            }
        }
    }

    public PauseEventResult OnPauseCallBack(PauseEventArgs _args)
    {
        m_isPaused = true;
        return null;
    }

    public PauseEventResult OnResumeCallBack(PauseEventArgs _args)
    {
        m_isPaused = m_isPaused = false;
        return null;
    }
}

