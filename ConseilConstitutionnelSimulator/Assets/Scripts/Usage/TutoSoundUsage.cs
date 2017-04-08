using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSoundUsage : MonoBehaviour {

  /* Toujours avoir cette chose.
   * Le SoundController contient les infos sur toutes les music jouable par l'entite
   * 
   * Faut rentré les info à la main dans l'inspecteur. Concretement c'est du glissé déposé
   * dans un tableau.
   * Chaque music peut en plus avoir un mixer particulier, au cas ou.
   */
  public SoundController sc;

  private float sumTime = 0;

	void Start () {
    sc = GetComponent<SoundController>();
    if(sc == null)
    {
      Debug.LogError("No SoundController detected !");
    }
    /* Pour lancer une music, suffit d'appeller le SoundManager.
     * Comme c'est un singleton, on appel nos méthode sur l'instance,
     * sobrement nommé I.
     */
   SoundManager.I.FadeMusicTo(sc["test_music"], 5);
  }
	
	void Update () {
    if (sumTime > 6)
    {
      SoundManager.I.PlaySound(sc["test_audio"]);
      sumTime = 0;
    }

    sumTime += Time.deltaTime;
  }
}
