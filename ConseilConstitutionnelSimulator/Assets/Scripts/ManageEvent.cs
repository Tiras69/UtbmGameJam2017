using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEvent : MonoBehaviour {

    private Law m_currentLaw;
    public GameController control;

    // Use this for initialization
    void Start () {
        GameManager.Instance.LoadAllLaws();
        GameManager.Instance.StartGameSession();
        m_currentLaw = GameManager.Instance.GetCurrentLaw();

        UpdateText();

    }
	
    public void UpdateText()
    {
        m_currentLaw = GameManager.Instance.GetCurrentLaw();
        
        // We took the inverse value because its only when its a modified value 
        // We want to change the state.
        GameManager.Instance.FireOnNewLawLoaded(!m_currentLaw.IsAModifiedLaw);

        control.title.text = m_currentLaw.Title;
        control.law.text = m_currentLaw.Description;
    }
    

	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.ClickAccentuer();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            this.ClickDiminuer();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.ClickValider();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.ClickRefuser();
        }*/
    }

    public void ClickValider()
    {
        UpdateGameSession(m_currentLaw.YesLawsToAdd, m_currentLaw.YesPropertyModifiers, false);
    }

    public void ClickAccentuer()
    {
        UpdateGameSession(m_currentLaw.MaximizeLawsToAdd, m_currentLaw.MaximizePropertyModifiers, true);
    }

    public void ClickDiminuer()
    {
        UpdateGameSession(m_currentLaw.MinimizeLawsToAdd, m_currentLaw.MinimizePropertyModifiers, true);
    }

    public void ClickRefuser()
    {
        UpdateGameSession(m_currentLaw.NoLawsToAdd, m_currentLaw.NoPropertyModifiers, false);

    }
    public void ClickOk()
    {
        GameManager.Instance.EndSemesterReport();
    }

    private void UpdateGameSession(List<int> _lawIds, List<PropertyModifier> _modifiers, bool _isAddedLawsAreModified)
    {
        foreach (int id in _lawIds)
            GameManager.Instance.AddLawToPool(id, _isAddedLawsAreModified);
        foreach (PropertyModifier prop in _modifiers)
            GameManager.Instance.ModifyGameProperty(prop.Property, prop.Value);

        GameManager.Instance.GoToNextMonth();
        UpdateText();
    }
}
