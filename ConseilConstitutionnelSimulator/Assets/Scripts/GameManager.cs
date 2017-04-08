using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        m_currentMonthInSemester = 0;
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

    private Random m_randomNumberGenerator = new Random();

    #endregion

    #region Methods
    public void LoadAllLaws()
    {

    }

    public Law GetNextLaw()
    {
        if (m_currentMonthInSemester < 6)
        {
            m_currentGameState = GameState.GameState_DECIDELAW;
            int nextIndex = m_randomNumberGenerator.Next(0, m_currentGameSessionLaws.Count - 1);
            m_currentMonthInSemester++;
            return m_currentGameSessionLaws[nextIndex];
        }
        else
        {
            StartSemesterReport();
            m_currentMonthInSemester = 0;
        }
        return null;
    }

    public void StartSemesterReport()
    {
        m_currentGameState = GameState.GameState_SEMESTERREPORT;
    }

    #endregion



}
