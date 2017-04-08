using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool {
  List<AudioSource> m_sources;
  AudioSource m_original;
  GameObject m_root;

  public SoundPool(GameObject root, AudioSource original)
  {
    m_original = original;
    m_root = root;
    m_sources = new List<AudioSource>();
  }

  private AudioSource AddAudioSource()
  {
    AudioSource newAudioSource = m_root.AddComponent<AudioSource>();

    // Black reflector magic. Copy each field of an audio source
    System.Type type = m_original.GetType();
    System.Reflection.FieldInfo[] fields = type.GetFields();
    foreach (System.Reflection.FieldInfo field in fields)
    {
      field.SetValue(newAudioSource, field.GetValue(m_original));
    }

    m_sources.Add(newAudioSource);
    return newAudioSource;
  }

  /// <summary>
  /// Return the first non playing audio source from the pool
  /// </summary>
  /// <returns>A non playing audio source</returns>
  public AudioSource GetAudioSource()
  {
    foreach(AudioSource source in m_sources)
    {
      if (!source.isPlaying)
      {
        return source;
      }
    }
    return AddAudioSource();
  }
}
