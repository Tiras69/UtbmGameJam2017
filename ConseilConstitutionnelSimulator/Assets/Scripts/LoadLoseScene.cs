using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLoseScene : MonoBehaviour
{
    public Text defeatText;
    public Image defeatImage;
    
    // Use this for initialization
    void Start()
    {
        // TODO get defeat type from GameManager
        //GameManager.I.getDefeatType();
        // TODO depending the defeat type, assign the attributes (or remove them and the function) 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void loadDefeatScene(Sprite defeatSprite, string defeatDescription)
    {
        if (defeatSprite != null)
            this.defeatImage.sprite = defeatSprite;
        if (defeatDescription != null)
            this.defeatText.text = defeatDescription;
    }
}