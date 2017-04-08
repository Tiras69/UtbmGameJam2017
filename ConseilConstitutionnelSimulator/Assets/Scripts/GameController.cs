using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public Text title;
    public Text law;

    public List<string> titles;
    public List<string> laws;

    // Use this for initialization
    void Start () {

        this.titles.Add("test");
        this.laws.Add("test");


        this.newEvent();

    }
	
	// Update is called once per frame
	void Update () {
 
    }

    void newEvent()
    {
        // inclusive , exclusive
        int random = Random.Range(0, titles.Count);

        this.title.text = titles[random];
        this.law.text = laws[random];

    }
}
