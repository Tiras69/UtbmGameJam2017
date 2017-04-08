using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEvent : MonoBehaviour {

    public GameManager game;
    public GameController control;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.ClickValider();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            this.ClickRefuser();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.ClickAccentuer();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.ClickDiminuer();
        }
    }

    public void ClickValider()
    {
        
        Debug.Log("V");
    }

    public void ClickAccentuer()
    {
        Debug.Log("A");
    }

    public void ClickDiminuer()
    {
        Debug.Log("D");
    }

    public void ClickRefuser()
    {
        Debug.Log("R");
    }
}
