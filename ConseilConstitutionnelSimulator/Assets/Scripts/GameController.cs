﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public Text title;
    public Text law;
    private Law currentLaw;

    public Law getLaw()
  
        {
            return currentLaw;
        }

      

    // Use this for initialization
    void Start () {
        newEvent();
    }
	
	// Update is called once per frame
	void Update () {
 
    }

    void newEvent()
    {

        this.title.text = currentLaw.Title;
        this.law.text = currentLaw.Description;

    }
}
