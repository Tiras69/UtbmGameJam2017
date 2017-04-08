﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        m_allGameLaws = new List<Law>(100);                 // Set to 100 to avoid memory allocations.
        m_currentGameSessionLaws = new LinkedList<Law>();
        CurrentMonthInSemester = 0;
        m_governmentOpinion = 50;
        m_populaceOpinion = 50;
        m_personalMoney = 50;
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
    LinkedList<Law> m_currentGameSessionLaws;
    Law m_currentLaw;
    private int m_currentMonthInSemester;

    public Slider economy;
    public Slider emploi;
    public Slider religion;

    #endregion

    #region Properties
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
        UnityEngine.Debug.Log(Application.dataPath);

        // Get files with the xml extension
        string[] files = Directory.GetFiles(Application.streamingAssetsPath+"/Laws", "*.xml");

        for (int i = 0; i < files.Length; i++)
        {
            try {
                Law tmplaw = XmlSerializerHelper<Law>.DeserializeXmlFile(files[i]);
                m_allGameLaws.Add(tmplaw);
            }
            catch( Exception e)
            {
                // Trigger a breakpoint if Visual is attached to UNITY
                #if UNITY_EDITOR
                if (Debugger.IsAttached)
                    Debugger.Break();
                #endif
                UnityEngine.Debug.Log(e.Message);
            }

        }

    }

    public void StartGameSession()
    {
        foreach(Slider s in FindObjectsOfType<Slider>()){
            if (s.name == "Economie")
                economy = s;
            else if (s.name == "Emploi")
                emploi = s;
            else if (s.name == "Religion")
                religion = s;
            else
                UnityEngine.Debug.Log("fds");
        }

        m_currentGameState = GameState.GameState_BEGINSEMESTER;
        // Here we will add the first avaible laws for a start game.
        // But for debug purpose it's the entire law database.
        foreach (Law law in m_allGameLaws)
            m_currentGameSessionLaws.AddLast( law );

        m_currentLaw = m_currentGameSessionLaws.ElementAt( UnityEngine.Random.Range(0, m_currentGameSessionLaws.Count - 1 ) );
    }

    public Law GetCurrentLaw()
    {
        return m_currentLaw;
    }

    public void GoToNextMonth()
    {
       CurrentMonthInSemester++;
        if (CurrentMonthInSemester == 6)
        {
            StartSemesterReport();
            m_currentLaw = m_currentGameSessionLaws.ElementAt(UnityEngine.Random.Range(0, m_currentGameSessionLaws.Count - 1));
        } else
            m_currentLaw = m_currentGameSessionLaws.ElementAt(UnityEngine.Random.Range(0, m_currentGameSessionLaws.Count - 1));
    }

    public void StartSemesterReport()
    {
        m_currentGameState = GameState.GameState_SEMESTERREPORT;
    }

    public void ModifyGameProperty(GameProperty _property, int _value)
    {

        switch (_property)
        {
            case GameProperty.GameProperty_ECONOMY:
                {
                    m_economy += _value;
                    economy.GetComponent<ManageJauge>().ChangeValue(_value);
                }
                break;

            case GameProperty.GameProperty_EMPLOYMENT:
                {
                    m_employement += _value;
                    emploi.GetComponent<ManageJauge>().ChangeValue(_value);
                }
                break;

            case GameProperty.GameProperty_GOVERNMENTOPINION:
                {
                    m_governmentOpinion += _value;

                    if (m_governmentOpinion <= 0)
                    {
                        SceneManager.LoadScene("LoseScene");
                    }
                }
                break;

            case GameProperty.GameProperty_PERSONALMONEY:
                {
                    m_personalMoney += _value;

                    if (m_personalMoney <= 0)
                    {
                        SceneManager.LoadScene("LoseScene");
                    }
                }
                break;

            case GameProperty.GameProperty_POPULACEOPINION:
                {
                    m_populaceOpinion += _value;

                    if (m_populaceOpinion <= 0)
                    {
                        SceneManager.LoadScene("LoseScene");
                    }
                }
                break;

            case GameProperty.GameProperty_RELIGION:
                {
                    m_religion += _value;
                    religion.GetComponent<ManageJauge>().ChangeValue(_value);
                }
                break;

            default:
                {
                    UnityEngine.Debug.Log("Game Property not yet supported");
                }
                break;
        }
    }

    public void AddLawToPool(int _id)
    {
        Law law = FindLawById(_id);
        if( law != null)
            m_currentGameSessionLaws.AddLast( law );
        else
            UnityEngine.Debug.Log("law " + _id + "doesn't exists");
    }

    private Law FindLawById(int _id)
    {
        for (int i = 0; i < m_allGameLaws.Count; i++)
            if (m_allGameLaws[i].Id == _id)
                return m_allGameLaws[i];
        return null;
    }

    #endregion

 

}
