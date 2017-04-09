using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPrefabInitializer : MonoBehaviour {

    SoundController m_controller;

	// Use this for initialization
	void Start () {


        if (FindObjectsOfType<SoundController>().Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);

            m_controller = this.GetComponent<SoundController>();

            SoundManager.Instance.PlayMusic(m_controller["ecranTitre"]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
