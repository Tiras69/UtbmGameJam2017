using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

  public FadeScreenAutoRef fadeScreen;

  public void LoadLevel(string levelName, LoadAndSave _saveGame)
  {
    fadeScreen.FadeOut();
    GameManager.Instance.FireOnPause();
    StartCoroutine(DelayedLoadLevel(levelName, _saveGame));
  }

  private IEnumerator DelayedLoadLevel(string levelString, LoadAndSave _saveGame)
  {
    yield return new WaitForSeconds(1.1f);
    //Application.LoadLevel(levelString);
    if (levelString == "MainScene")
    {
        GameManager.Instance.LoadAllLaws();
        GameManager.Instance.StartGameSession(_saveGame);
    }
    SceneManager.LoadScene(levelString);
    PlayMusicOnLoad(levelString);
    
    
  }

    private void PlayMusicOnLoad(string _levelString)
    {
        SoundController _controller = FindObjectOfType<SoundController>();
        switch (_levelString)
        {
            case "MainMenu":
                {
                    SoundManager.Instance.PlayMusic(_controller["ecranTitre"]);
                }
                break;

            case "MainScene":
                {
                    SoundManager.Instance.PlayMusic(_controller["inGame"]);
                }
                break;

            case "LoseScene":
                {
                    SoundManager.Instance.PlayMusic(_controller["loseMenu"]);
                }
                break;

            case "WinScene":
                {
                    SoundManager.Instance.PlayMusic(_controller["WinMenu"]);
                }
                break;

            default:
                {

                }
                break;
        }
    }

}
