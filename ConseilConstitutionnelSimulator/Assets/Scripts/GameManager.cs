using System;
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

public enum LoseCondition
{
    NONE,
    BANKRUPT,
    JAIL,
    REVOLUTION
}

public class GameManager : Singleton<GameManager> {

    public LoseCondition loseCondition = LoseCondition.NONE;

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
        gouvOpinionSemestre=0;
        populaceOpinionSemestre=0;
        personalMoneySemestre=0;
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
    private int gouvOpinionSemestre;
    private int populaceOpinionSemestre;
    private int personalMoneySemestre;
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

    public delegate void SetButtonStateHandler(bool _isActive);
    public event SetButtonStateHandler OnNewLawLoaded;
    public void resetOnNewLawLoaded()
    {
        OnNewLawLoaded = null;
    }
    public void FireOnNewLawLoaded(bool _isActive)
    {
        OnNewLawLoaded(_isActive);
    }

    private Image report;


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
                UnityEngine.Debug.Log("Slider Missing");
        }
        foreach (Image s in FindObjectsOfType<Image>())
        {
            if (s.name == "Report")
            {
                report = s;
                report.enabled = false;
                report.GetComponentInChildren<Text>().enabled = false;
                report.GetComponentInChildren<Button>().enabled = false;
                report.GetComponentInChildren<Button>().image.enabled = false;
                report.GetComponentInChildren<Button>().GetComponentInChildren<Text>().enabled = false;
            }
               
        }
        m_currentGameState = GameState.GameState_BEGINSEMESTER;
        // Here we will add the first avaible laws for a start game.
        // But for debug purpose it's the entire law database.
        AddInitialLaws();

        m_currentLaw = m_currentGameSessionLaws.ElementAt( UnityEngine.Random.Range(0, m_currentGameSessionLaws.Count));
    }

    private void AddInitialLaws()
    {
        m_currentGameSessionLaws.AddLast(FindLawById(0));
        m_currentGameSessionLaws.AddLast(FindLawById(5));
        m_currentGameSessionLaws.AddLast(FindLawById(10));
        m_currentGameSessionLaws.AddLast(FindLawById(15));
        m_currentGameSessionLaws.AddLast(FindLawById(20));
        m_currentGameSessionLaws.AddLast(FindLawById(25));
        m_currentGameSessionLaws.AddLast(FindLawById(28));
        m_currentGameSessionLaws.AddLast(FindLawById(33));
        m_currentGameSessionLaws.AddLast(FindLawById(38));
        m_currentGameSessionLaws.AddLast(FindLawById(43));
        m_currentGameSessionLaws.AddLast(FindLawById(48));
        m_currentGameSessionLaws.AddLast(FindLawById(53));
        m_currentGameSessionLaws.AddLast(FindLawById(57));
        m_currentGameSessionLaws.AddLast(FindLawById(61));
        m_currentGameSessionLaws.AddLast(FindLawById(66));
        m_currentGameSessionLaws.AddLast(FindLawById(70));
        m_currentGameSessionLaws.AddLast(FindLawById(75));
        m_currentGameSessionLaws.AddLast(FindLawById(79));
        m_currentGameSessionLaws.AddLast(FindLawById(81));
        m_currentGameSessionLaws.AddLast(FindLawById(82));
        m_currentGameSessionLaws.AddLast(FindLawById(85));
        m_currentGameSessionLaws.AddLast(FindLawById(90));
        m_currentGameSessionLaws.AddLast(FindLawById(95));
        m_currentGameSessionLaws.AddLast(FindLawById(97));
        m_currentGameSessionLaws.AddLast(FindLawById(99));
        m_currentGameSessionLaws.AddLast(FindLawById(104));
        m_currentGameSessionLaws.AddLast(FindLawById(109));
        m_currentGameSessionLaws.AddLast(FindLawById(111));
        m_currentGameSessionLaws.AddLast(FindLawById(113));
        m_currentGameSessionLaws.AddLast(FindLawById(117));
        m_currentGameSessionLaws.AddLast(FindLawById(118));
        m_currentGameSessionLaws.AddLast(FindLawById(120));
        m_currentGameSessionLaws.AddLast(FindLawById(123));
        m_currentGameSessionLaws.AddLast(FindLawById(126));
        m_currentGameSessionLaws.AddLast(FindLawById(127));
        m_currentGameSessionLaws.AddLast(FindLawById(129));
        m_currentGameSessionLaws.AddLast(FindLawById(133));
        m_currentGameSessionLaws.AddLast(FindLawById(138));
        m_currentGameSessionLaws.AddLast(FindLawById(139));
        m_currentGameSessionLaws.AddLast(FindLawById(142));
        m_currentGameSessionLaws.AddLast(FindLawById(147));
        m_currentGameSessionLaws.AddLast(FindLawById(151));
        m_currentGameSessionLaws.AddLast(FindLawById(156));
        m_currentGameSessionLaws.AddLast(FindLawById(160));
        m_currentGameSessionLaws.AddLast(FindLawById(163));
        m_currentGameSessionLaws.AddLast(FindLawById(167));
        m_currentGameSessionLaws.AddLast(FindLawById(172));
        m_currentGameSessionLaws.AddLast(FindLawById(177));
        m_currentGameSessionLaws.AddLast(FindLawById(182));
        m_currentGameSessionLaws.AddLast(FindLawById(187));
        m_currentGameSessionLaws.AddLast(FindLawById(188));
        m_currentGameSessionLaws.AddLast(FindLawById(192));
        m_currentGameSessionLaws.AddLast(FindLawById(195));
        m_currentGameSessionLaws.AddLast(FindLawById(200));
        m_currentGameSessionLaws.AddLast(FindLawById(205));
        m_currentGameSessionLaws.AddLast(FindLawById(210));
        m_currentGameSessionLaws.AddLast(FindLawById(215));
        m_currentGameSessionLaws.AddLast(FindLawById(220));
        m_currentGameSessionLaws.AddLast(FindLawById(224));
        m_currentGameSessionLaws.AddLast(FindLawById(228));
        m_currentGameSessionLaws.AddLast(FindLawById(233));
        m_currentGameSessionLaws.AddLast(FindLawById(237));
        m_currentGameSessionLaws.AddLast(FindLawById(242));
        m_currentGameSessionLaws.AddLast(FindLawById(246));
        m_currentGameSessionLaws.AddLast(FindLawById(248));
        m_currentGameSessionLaws.AddLast(FindLawById(249));
        m_currentGameSessionLaws.AddLast(FindLawById(252));
        m_currentGameSessionLaws.AddLast(FindLawById(257));
        m_currentGameSessionLaws.AddLast(FindLawById(262));
        m_currentGameSessionLaws.AddLast(FindLawById(264));
        m_currentGameSessionLaws.AddLast(FindLawById(266));
        m_currentGameSessionLaws.AddLast(FindLawById(271));
        m_currentGameSessionLaws.AddLast(FindLawById(276));
        m_currentGameSessionLaws.AddLast(FindLawById(278));
        m_currentGameSessionLaws.AddLast(FindLawById(280));
        m_currentGameSessionLaws.AddLast(FindLawById(284));
        m_currentGameSessionLaws.AddLast(FindLawById(285));
        m_currentGameSessionLaws.AddLast(FindLawById(287));
        m_currentGameSessionLaws.AddLast(FindLawById(290));
        m_currentGameSessionLaws.AddLast(FindLawById(293));
        m_currentGameSessionLaws.AddLast(FindLawById(294));
        m_currentGameSessionLaws.AddLast(FindLawById(296));
        m_currentGameSessionLaws.AddLast(FindLawById(300));
        m_currentGameSessionLaws.AddLast(FindLawById(305));
        m_currentGameSessionLaws.AddLast(FindLawById(306));
        m_currentGameSessionLaws.AddLast(FindLawById(309));
        m_currentGameSessionLaws.AddLast(FindLawById(314));
        m_currentGameSessionLaws.AddLast(FindLawById(318));
        m_currentGameSessionLaws.AddLast(FindLawById(323));
        m_currentGameSessionLaws.AddLast(FindLawById(327));
        m_currentGameSessionLaws.AddLast(FindLawById(330));
        m_currentGameSessionLaws.AddLast(FindLawById(334));
        m_currentGameSessionLaws.AddLast(FindLawById(339));
        m_currentGameSessionLaws.AddLast(FindLawById(344));
        m_currentGameSessionLaws.AddLast(FindLawById(349));
        m_currentGameSessionLaws.AddLast(FindLawById(354));
        m_currentGameSessionLaws.AddLast(FindLawById(355));
        m_currentGameSessionLaws.AddLast(FindLawById(359));
        m_currentGameSessionLaws.AddLast(FindLawById(362));
        m_currentGameSessionLaws.AddLast(FindLawById(367));
        m_currentGameSessionLaws.AddLast(FindLawById(372));
        m_currentGameSessionLaws.AddLast(FindLawById(377));
        m_currentGameSessionLaws.AddLast(FindLawById(382));
        m_currentGameSessionLaws.AddLast(FindLawById(387));
        m_currentGameSessionLaws.AddLast(FindLawById(391));
        m_currentGameSessionLaws.AddLast(FindLawById(395));
        m_currentGameSessionLaws.AddLast(FindLawById(400));
        m_currentGameSessionLaws.AddLast(FindLawById(404));
        m_currentGameSessionLaws.AddLast(FindLawById(409));
        m_currentGameSessionLaws.AddLast(FindLawById(413));
        m_currentGameSessionLaws.AddLast(FindLawById(415));
        m_currentGameSessionLaws.AddLast(FindLawById(416));
        m_currentGameSessionLaws.AddLast(FindLawById(419));
        m_currentGameSessionLaws.AddLast(FindLawById(424));
        m_currentGameSessionLaws.AddLast(FindLawById(429));
        m_currentGameSessionLaws.AddLast(FindLawById(431));
        m_currentGameSessionLaws.AddLast(FindLawById(433));
        m_currentGameSessionLaws.AddLast(FindLawById(438));
        m_currentGameSessionLaws.AddLast(FindLawById(443));
        m_currentGameSessionLaws.AddLast(FindLawById(445));
        m_currentGameSessionLaws.AddLast(FindLawById(447));
        m_currentGameSessionLaws.AddLast(FindLawById(451));
        m_currentGameSessionLaws.AddLast(FindLawById(452));
        m_currentGameSessionLaws.AddLast(FindLawById(454));
        m_currentGameSessionLaws.AddLast(FindLawById(457));
        m_currentGameSessionLaws.AddLast(FindLawById(460));
        m_currentGameSessionLaws.AddLast(FindLawById(461));
        m_currentGameSessionLaws.AddLast(FindLawById(463));
        m_currentGameSessionLaws.AddLast(FindLawById(467));
        m_currentGameSessionLaws.AddLast(FindLawById(472));
        m_currentGameSessionLaws.AddLast(FindLawById(473));
        m_currentGameSessionLaws.AddLast(FindLawById(476));
        m_currentGameSessionLaws.AddLast(FindLawById(481));
        m_currentGameSessionLaws.AddLast(FindLawById(485));
        m_currentGameSessionLaws.AddLast(FindLawById(490));
        m_currentGameSessionLaws.AddLast(FindLawById(494));
        m_currentGameSessionLaws.AddLast(FindLawById(497));

    }

    public Law GetCurrentLaw()
    {
        return m_currentLaw;
    }

    public void GoToNextMonth()
    {
       CurrentMonthInSemester++;
       m_currentGameSessionLaws.Remove(m_currentLaw);
       if (m_currentGameSessionLaws.Count > 0)
       {
            if (CurrentMonthInSemester == 6)
            {
                StartSemesterReport();    
                CurrentMonthInSemester = 0;
            }
            m_currentLaw = m_currentGameSessionLaws.ElementAt(UnityEngine.Random.Range(0, m_currentGameSessionLaws.Count));
       }else
            UnityEngine.Debug.Log("No Law Left");

    }

    public void StartSemesterReport()
    {
        m_currentGameState = GameState.GameState_SEMESTERREPORT;

        Text reportText = report.GetComponentInChildren<Text>();
        reportText.text = "Rapport Semestriel : \n \n \n Opinion du gouvernement : " + m_governmentOpinion + "\n Opinion du peuple : " + m_populaceOpinion + "\n Relevé bancaire : " + m_personalMoney + "\n \n \n Changement de l'opinion du gouvernement ce semestre : " + gouvOpinionSemestre + "\n Changement de l'opinion du peuple ce semestre : " + populaceOpinionSemestre + "\n Changement du relevé bancaire ce semestre " + personalMoneySemestre;
        report.enabled = true;
        report.GetComponentInChildren<Text>().enabled = true;
        report.GetComponentInChildren<Button>().enabled = true;
        report.GetComponentInChildren<Button>().image.enabled = true;
        report.GetComponentInChildren<Button>().GetComponentInChildren<Text>().enabled = true;
    }
    public void EndSemesterReport()
    {
        if( m_personalMoney > 3000000)
        {
            LevelManager.Instance.LoadLevel("WinScene");
        }
        report.enabled = false;
        report.GetComponentInChildren<Text>().enabled = false;
        report.GetComponentInChildren<Button>().image.enabled = false;
        report.GetComponentInChildren<Button>().GetComponentInChildren<Text>().enabled = false;
        gouvOpinionSemestre = 0;
        populaceOpinionSemestre = 0;
        personalMoneySemestre = 0;
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
                    gouvOpinionSemestre += _value;
                    if (m_governmentOpinion <= 0)
                    {
                        UnityEngine.Debug.Log("GOuv");
                        //SceneManager.LoadScene("LoseScene");
                    }
                }
                break;

            case GameProperty.GameProperty_PERSONALMONEY:
                {
                    m_personalMoney += _value;
                    personalMoneySemestre += _value;
                    if (m_personalMoney <= 0)
                    {
                        UnityEngine.Debug.Log("Money");
                        //SceneManager.LoadScene("LoseScene");
                    }
                }
                break;

            case GameProperty.GameProperty_POPULACEOPINION:
                {
                    m_populaceOpinion += _value;
                    populaceOpinionSemestre += _value;
                    if (m_populaceOpinion <= 0)
                    {
                        UnityEngine.Debug.Log("peuple");
                        //SceneManager.LoadScene("LoseScene");
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

    public void loadGameFile()
    {
        //---------------------------------------------------
        //
        //  FIND A SOLUTION WITHOUT EDITOR FEATURES !!!
        //
        //---------------------------------------------------

        // Get files with the xml extension

        string[] file = Directory.GetFiles(Application.streamingAssetsPath + "/../../", "*.xml");

        try
        {
            LoadAndSave loadAndSave = XmlSerializerHelper<LoadAndSave>.DeserializeXmlFile(file[0]);
            
            this.loadGame(loadAndSave);
        }
        catch (Exception e)
        {
            // Trigger a breakpoint if Visual is attached to UNITY
            #if UNITY_EDITOR
            if (Debugger.IsAttached)
                Debugger.Break();
            #endif
            UnityEngine.Debug.Log(e.Message);
        }
        
        StartGameSession();
        
    }

    private void loadGame(LoadAndSave loadAndSave)
    {

        this.m_governmentOpinion = loadAndSave.GovernementOpinion;
        this.m_populaceOpinion = loadAndSave.PopulaceOpinion;
        this.m_personalMoney = loadAndSave.MoneyValue;
        this.m_economy = loadAndSave.EconomyValue;
        this.m_employement = loadAndSave.EmploymentValue;
        this.m_religion = loadAndSave.ReligionValue;

        foreach (int id in loadAndSave.ListCurrentLawId)
        {
            this.m_currentGameSessionLaws.AddLast(this.FindLawById(id));
        }

        this.m_currentLaw = this.FindLawById(loadAndSave.CurrentLawId);
        this.m_currentMonthInSemester = loadAndSave.CurrentMonthInSemester;

        economy.GetComponent<ManageJauge>().ChangeValue(m_economy);
        emploi.GetComponent<ManageJauge>().ChangeValue(m_employement);
        religion.GetComponent<ManageJauge>().ChangeValue(m_religion);

    }

    public void saveGame(Button buttonSave)
    {
        LoadAndSave loadAndSave = new LoadAndSave();

        foreach (Law law in this.m_currentGameSessionLaws)
        {
            loadAndSave.ListCurrentLawId.Add(law.Id);
        }

        loadAndSave.CurrentLawId = this.m_currentLaw.Id;

        loadAndSave.GovernementOpinion = this.m_governmentOpinion;
        loadAndSave.PopulaceOpinion = this.m_populaceOpinion;
        loadAndSave.MoneyValue = this.m_personalMoney;
        loadAndSave.EconomyValue = this.m_economy;
        loadAndSave.EmploymentValue = this.m_employement;
        loadAndSave.ReligionValue = this.m_religion;

        loadAndSave.CurrentMonthInSemester = this.m_currentMonthInSemester;

        XmlSerializerHelper<LoadAndSave>.SerializeXmlFile("save.xml", loadAndSave);

        buttonSave.GetComponentInChildren<Text>().text = "Game Saved";
    }

    public void AddLawToPool(int _id, bool _isAddedLawsAreModified)
    {
        Law law = FindLawById(_id);
        if (law != null)
        {
            if (_isAddedLawsAreModified)
                law.IsAModifiedLaw = true;
            m_currentGameSessionLaws.AddLast(law);
        }
#if UNITY_EDITOR
        else
        {

            UnityEngine.Debug.Log("law " + _id + "doesn't exists");
        }
#endif
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
