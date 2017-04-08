using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    GameState_MENU,
    GameState_BEGINSEMESTER,
    GameState_DECIDELAW,
    GameState_SEMESTERREPORT,
    GameState_ENDSEMESTER
}

public class GameManager : Singleton<GameManager> {

    #region constructors
    protected GameManager()
    {
        m_currentGameState = GameState.GameState_MENU;
        m_allGameLaws = new List<Law>(100);
        m_currentGameSessionLaws = new List<Law>(100);
        CurrentMonthInSemester = 0;
    }
    #endregion

    /// <summary>
    /// Those fields must never be modify outside this manager
    /// all modification must be done in methods.
    /// </summary>
    #region fields
    private GameState m_currentGameState;

    private int m_governmentOpinion;
    private int m_populaceOpinion;
    private int m_personalMoney;
    private int m_economy;
    private int m_employement;
    private int m_religion;
    List<Law> m_allGameLaws;
    List<Law> m_currentGameSessionLaws;
    private int m_currentMonthInSemester;

    public Slider economy;
    public Slider emploi;
    public Slider religion;

    

    public int GovernmentOpinion
    {
        get
        {
            return m_governmentOpinion;
        }

        set
        {
            m_governmentOpinion = value;
        }
    }

    public int PopulaceOpinion
    {
        get
        {
            return m_populaceOpinion;
        }

        set
        {
            m_populaceOpinion = value;
        }
    }

    public int PersonalMoney
    {
        get
        {
            return m_personalMoney;
        }

        set
        {
            m_personalMoney = value;
        }
    }

    public int Economy
    {
        get
        {
            return m_economy;
        }

        set
        {
            m_economy = value;
        }
    }

    public int Employement
    {
        get
        {
            return m_employement;
        }

        set
        {
            m_employement = value;
        }
    }

    public int Religion
    {
        get
        {
            return m_religion;
        }

        set
        {
            m_religion = value;
        }
    }

    public int CurrentMonthInSemester
    {
        get
        {
            return m_currentMonthInSemester;
        }

        set
        {
            m_currentMonthInSemester = value;
        }
    }

    #endregion

    #region Methods
    public void LoadAllLaws()
    {

    }

    public Law GetNextLaw()
    {
        if (CurrentMonthInSemester < 6)
        {
            m_currentGameState = GameState.GameState_DECIDELAW;
            int nextIndex = Random.Range(0, m_currentGameSessionLaws.Count - 1);
            CurrentMonthInSemester++;
            return m_currentGameSessionLaws[nextIndex];
        }
        else
        {
            StartSemesterReport();
            CurrentMonthInSemester = 0;
        }
        return null;
    }

    public void StartSemesterReport()
    {
        m_currentGameState = GameState.GameState_SEMESTERREPORT;
    }

    #endregion

 

}
