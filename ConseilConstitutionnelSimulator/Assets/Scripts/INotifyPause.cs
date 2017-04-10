using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate PauseEventResult NotifyPauseHandler(PauseEventArgs _args);

public interface INotifyPause{

    event NotifyPauseHandler OnPause;
    event NotifyPauseHandler OnResume;
	
}

public class PauseEventArgs
{
    #region Fields
    private object m_sender;
    private string m_message;
    #endregion

    #region Properties
    public object Sender
    {
        get { return m_sender; }
    }

    public object Message
    {
        get { return m_message; }
    }
    #endregion

    #region Constructors
    public PauseEventArgs()
    {
        m_sender = null;
        m_message = "";
    }

    public PauseEventArgs(object _sender, string _message)
    {
        m_sender = _sender;
        m_message = _message;
    }
    #endregion

    #region Methods
    public override string ToString()
    {
        return "PauseEventArgs :\nSender : " + m_sender.ToString() + "\nMessage : " + m_message + "\n";
    }
    #endregion
}

public class PauseEventResult
{
    #region Fields
    private bool m_canPause;
    #endregion

    #region Properties
    public bool CanPause
    {
        get { return m_canPause; }
        set { m_canPause = value; }
    }
    #endregion

    #region Constructors
    public PauseEventResult()
    {
        m_canPause = true;
    }

    public PauseEventResult(bool _canPause)
    {
        m_canPause = _canPause;
    }
    #endregion
}