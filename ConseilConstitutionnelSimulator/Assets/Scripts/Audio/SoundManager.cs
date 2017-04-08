using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
  [SerializeField]
  private AudioSource Music;

  [SerializeField]
  private AudioSource Sound;

  private SoundPool m_soundPool;

  public void OnEnable()
  {
    if (Music == null)
    {
      Music = gameObject.AddComponent<AudioSource>();
      Music.loop = false;
    }

    if (Sound == null)
    {
      Sound = gameObject.AddComponent<AudioSource>();
    }

    m_soundPool = new SoundPool(gameObject, Sound);
  }

  /// <summary>
  /// Play a new sound.
  /// There can be as many concurrent sound as needed
  /// </summary>
  /// <param name="soundEvent">the new sound</param>
  public void PlaySound(SoundController.SoundEvent soundEvent)
  {
    AudioSource currentAudioSource = m_soundPool.GetAudioSource();
    currentAudioSource.clip = soundEvent.audio;

    if(soundEvent.mixer != null)
    {
      currentAudioSource.outputAudioMixerGroup = soundEvent.mixer;
    }
    else
    {
      currentAudioSource.outputAudioMixerGroup = null;
    }
    currentAudioSource.Play();
  }

  /// <summary>
  /// Play a music.
  /// If another music was playing, stop it
  /// </summary>
  /// <param name="soundEvent">The new music</param>
  public void PlayMusic(SoundController.SoundEvent soundEvent)
  {
    if (Music.isPlaying)
    {
      Music.Stop();
    }
    Music.clip = soundEvent.audio;

    if (soundEvent.mixer != null)
    {
      Music.outputAudioMixerGroup = soundEvent.mixer;
    }
    else
    {
      Music.outputAudioMixerGroup = null;
    }

    Music.Play();
  }

  /// <summary>
  /// Fade the music to another music
  /// If no music exist, the new music is faded in
  /// </summary>
  /// <param name="soundEvent">The new music</param>
  /// <param name="fadeTime">Fading time</param>
  public void FadeMusicTo(SoundController.SoundEvent soundEvent, float fadeTime)
  {
    StartCoroutine(FadeMusicCoroutine(soundEvent, fadeTime));
  }

  /// <summary>
  /// Fade the music volume
  /// </summary>
  /// <param name="volume">The target volume</param>
  /// <param name="fadeTime">Fading time</param>
  public void FadeVolumeTo(float volume, float fadeTime)
  {
    StartCoroutine(FadeVolumeCoroutine(volume, fadeTime));
  }

  /// <summary>
  /// Set the music volume
  /// </summary>
  /// <param name="volume">new volume. must be between [0, 1]</param>
  public void SetVolume(float volume)
  {
    Music.volume = volume;
  }

  private IEnumerator FadeVolumeCoroutine(float volume, float fadeTime)
  {
    float sumTime = 0;
    float oldVolume = Music.volume;
    while(sumTime <= fadeTime)
    {
      Music.volume = Mathf.Lerp(oldVolume, volume, sumTime / fadeTime);

      sumTime += Time.deltaTime;
      yield return null;
    }
    Music.volume = volume;
  }

  private IEnumerator FadeMusicCoroutine(SoundController.SoundEvent soundEvent, float fadeTime)
  {
    float sumTime = 0;
    float volume = Music.volume;

    if(Music.clip != null && Music.isPlaying)
    while (sumTime <= fadeTime)
    {
      Music.volume = Mathf.Lerp(volume, 0, sumTime / fadeTime);

      sumTime += Time.deltaTime;
      yield return null;
    }

    PlayMusic(soundEvent);
    sumTime = 0;

    while (sumTime <= fadeTime)
    {
      Music.volume = Mathf.Lerp(0, volume, sumTime / fadeTime);

      sumTime += Time.deltaTime;
      yield return null;
    }
    Music.volume = volume;
  }

}
