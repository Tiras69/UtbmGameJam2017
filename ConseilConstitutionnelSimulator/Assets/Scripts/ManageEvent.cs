using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEvent : MonoBehaviour {

    private Law m_currentLaw;

    // Use this for initialization
    void Start () {
        GameManager.Instance.LoadAllLaws();
        GameManager.Instance.StartGameSession();
        m_currentLaw = GameManager.Instance.GetCurrentLaw();
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
        UpdateGameSession(m_currentLaw.YesLawsToAdd, m_currentLaw.YesPropertyModifiers);
    }

    public void ClickAccentuer()
    {
        UpdateGameSession(m_currentLaw.MaximizeLawsToAdd, m_currentLaw.MaximizePropertyModifiers);
    }

    public void ClickDiminuer()
    {
        UpdateGameSession(m_currentLaw.MinimizeLawsToAdd, m_currentLaw.MinimizePropertyModifiers);
    }

    public void ClickRefuser()
    {
        UpdateGameSession(m_currentLaw.NoLawsToAdd, m_currentLaw.NoPropertyModifiers);
    }

    private void UpdateGameSession(List<int> _lawIds, List<PropertyModifier> _modifiers)
    {
        foreach (int id in _lawIds)
            GameManager.Instance.AddLawToPool(id);
        foreach (PropertyModifier prop in _modifiers)
            GameManager.Instance.ModifyGameProperty(prop.Property, prop.Value);
    }
}
