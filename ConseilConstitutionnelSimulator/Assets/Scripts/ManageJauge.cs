using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageJauge : MonoBehaviour {
    public Slider ValueSlider;  //reference for slider
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
        
	}
 
    public void ChangeValue(int val)
    {
        ValueSlider.value += val;
        if (ValueSlider.value <= 0)
        {
            LevelManager.Instance.LoadLevel("LoseScene");
        }
    }
   
}

