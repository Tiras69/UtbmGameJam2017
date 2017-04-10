using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPausable{

    PauseEventResult OnPauseCallBack(PauseEventArgs _args);

    PauseEventResult OnResumeCallBack(PauseEventArgs _args);

}
