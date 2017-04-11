using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* HOW TO USE !!!!
 * 1) poser le prefab dans la scène
 * 2) glisser dans la liste "activeObject" tout les GameObject qui doivent
 *    etre désactivé quand le jeux est en pause
 * 3) glisser dans pauseCanvas le canvas de pause (o'rly ?) disponnible en prefab
 * 4) Se retaper la config de chaque bouton dans le canvas de pause
 */
public class PauseUIManager : Singleton<PauseUIManager>, IPausable
{
    [SerializeField]
    private GameObject[] activeObjects;

    [SerializeField]
    private GameObject pauseCanvas;

    private bool[] canvaStates;

    private bool isActive = false;

    private bool m_isPaused = true;

    void Start()
    {
        GameManager.Instance.OnPause += OnPauseCallBack;
        GameManager.Instance.OnResume += OnResumeCallBack;
    }

    void OnEnable()
    {
        pauseCanvas.SetActive(false);
        canvaStates = new bool[activeObjects.Length];
        for (uint i = 0; i < activeObjects.Length; ++i)
        {
            canvaStates[i] = activeObjects[i].activeInHierarchy;
        }
    }

    void Update()
    {
        if (!m_isPaused && Input.GetKeyUp(KeyCode.Escape))
        {
            Show();
        }
    }

    public bool isSoundActivated()
    {
        return SoundManager.Instance.IsActive;
    }

    public void setSoundActivated(bool on)
    {
        SoundManager.Instance.IsActive = on;
    }

    public void Quit()
    {
        GameManager.Instance.saveGame(null);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void LoadByName(string sceneName)
    {
        GameManager.Instance.saveGame(null);
        GameManager.Instance.resetOnNewLawLoaded();
        LevelManager.Instance.LoadLevel(sceneName, null);
    }

    public void Show()
    {
        GameManager.Instance.FireOnPause();
        //foreach (GameObject activeObject in activeObjects)
        //{
        //    activeObject.SetActive(false);
        //}
        pauseCanvas.SetActive(true);
        isActive = true;
    }

    public void Hide()
    {
        GameManager.Instance.FireOnResume();
        pauseCanvas.SetActive(false);
        for (uint i = 0; i < activeObjects.Length; ++i)
        {
            activeObjects[i].SetActive(canvaStates[i]);
        }
        isActive = false;
    }

    public void Toggle()
    {
        if (isActive)
        {
            Hide();
        }
        else
        {
            Show();
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
