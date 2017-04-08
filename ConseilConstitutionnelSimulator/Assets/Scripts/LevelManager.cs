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

  }
}
