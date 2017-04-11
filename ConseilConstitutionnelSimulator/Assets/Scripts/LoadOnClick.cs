using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);

        // If the button doesn't exists we don't want the player to click on it.
        if (!File.Exists(Application.streamingAssetsPath + "/PlayerSave.xml"))
        {
            btn.enabled = false;
            Text text = gameObject.GetComponentInChildren<Text>();
            if (text != null)
                text.color = Color.gray;
        }
    }

    void TaskOnClick()
    {
        GameManager.Instance.loadGameFile();
    }
}
