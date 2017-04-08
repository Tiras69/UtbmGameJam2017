using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
  [System.Serializable]
  public class SoundEvent
  {
    public string tag;
    public AudioClip audio;
    public AudioMixerGroup mixer;
  }

  [SerializeField]
  private SoundEvent[] soundEvents;

  private Dictionary<string, SoundEvent> m_soundEventDict;

  /* For easy access */
  public SoundEvent this[string key]
  {
    get
    {
      return m_soundEventDict[key];
    }
  }

  /* Must be called before any sound could be played */
  public void OnEnable()
  {
    m_soundEventDict = new Dictionary<string, SoundEvent>();
    foreach(SoundEvent soundEvent in soundEvents)
    {
      m_soundEventDict[soundEvent.tag] = soundEvent;
    }
  }
  
}
