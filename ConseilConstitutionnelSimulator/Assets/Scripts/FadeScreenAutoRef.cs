using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreenAutoRef : MonoBehaviour
{

  private Image fadeScreen;
  private Color _transparentColor;
  private Color _opaqueColor;
  private float _fadeTime;
  private float _fadeTimer;

  // Use this for initialization
  void Start()
  {
    LevelManager.Instance.fadeScreen = this;
    fadeScreen = GetComponent<Image>();
    _fadeTime = 1.0f;
    FadeIn();
  }

  public void FadeIn()
  {
    _opaqueColor = new Color(fadeScreen.color.r,
                                fadeScreen.color.g,
                                fadeScreen.color.b,
                                1.0f);

    _transparentColor = new Color(fadeScreen.color.r,
                                    fadeScreen.color.g,
                                    fadeScreen.color.b,
                                    0.0f);

    StopAllCoroutines();
    _fadeTimer = 0.0f;
    StartCoroutine(FadeInDelayed());
  }


  private IEnumerator FadeInDelayed()
  {
    while (_fadeTimer < _fadeTime)
    {
      _fadeTimer = _fadeTimer + Time.deltaTime;
      fadeScreen.color = Color.Lerp(_opaqueColor, _transparentColor, _fadeTimer / _fadeTime);
      yield return new WaitForEndOfFrame();
    }
    GameManager.Instance.FireOnResume();
  }

  public void FadeOut()
  {
    _opaqueColor = new Color(fadeScreen.color.r,
                                fadeScreen.color.g,
                                fadeScreen.color.b,
                                1.0f);

    _transparentColor = new Color(fadeScreen.color.r,
                                    fadeScreen.color.g,
                                    fadeScreen.color.b,
                                    0.0f);

    StopAllCoroutines();
    _fadeTimer = 0.0f;
    StartCoroutine(FadeOutDelayed());
  }


  private IEnumerator FadeOutDelayed()
  {
    while (_fadeTimer < _fadeTime)
    {
            yield return new WaitForEndOfFrame();
            _fadeTimer = _fadeTimer + Time.deltaTime;
            fadeScreen.color = Color.Lerp(_transparentColor, _opaqueColor, _fadeTimer / _fadeTime);
      
    }
        fadeScreen.color = _opaqueColor;
    }

}
