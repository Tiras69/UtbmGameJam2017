using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

  public FadeScreenAutoRef fadeScreen;

  public void LoadLevel(string levelName)
  {
    fadeScreen.FadeOut();
    StartCoroutine(DelayedLoadLevel(levelName));
  }

  private IEnumerator DelayedLoadLevel(string levelString)
  {
    yield return new WaitForSeconds(3.0f);
    //Application.LoadLevel(levelString);
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

            default:
                {

                }
                break;
        }
    }

}
