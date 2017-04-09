using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SetButtonStateCallBack : MonoBehaviour {

    private Button m_button;
    private Image m_image;
    //private Text m_text;

	// Use this for initialization
	void Start () {
        m_button = this.GetComponent<Button>();
        m_image = this.GetComponent<Image>();
        //m_text = this.GetComponentInChildren<Text>();

        GameManager.Instance.OnNewLawLoaded += ButtonStateCallBack;
	}
	
    private void ButtonStateCallBack(bool _isActive)
    {
        m_button.enabled = _isActive;
        m_image.enabled = _isActive;
        //m_text.enabled = _isActive;
    }
}
