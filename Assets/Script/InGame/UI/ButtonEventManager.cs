/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class ButtonEventManager : MonoBehaviour 
{
    [SerializeField, Tooltip("マネージャーオブジェクト")]
    private GameObject _manager = default;

    [SerializeField, Tooltip("ドロップオブジェクト")]
    private GameObject _drop = default;

	public void OnRight()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Ready)
        {
            _manager.GetComponent<BlockMove>().RightEvent();
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnLeft()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Ready)
        {
            _manager.GetComponent<BlockMove>().LeftEvent();
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnPlay()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Ready)
        {
            TimeManager.StartTime();
            StateManager.SetGameState(E_GameState.InGame_Droping);
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
        else if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Droping)
        {
            TimeManager.StartTime();
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnRePlay()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Droping)
        {
            TimeManager.RewindTime();
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnStop()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Droping)
        {
            TimeManager.StopTime();
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnReStart()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Droping)
        {
            _drop.GetComponent<DropController>().ResetDropPosition();
            TimeManager.StopTime();
            StateManager.SetGameState(E_GameState.InGame_Ready);
            ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
        }
    }

    public void OnBackToTitle()
    {
        StateManager.SetGameState(E_GameState.Title);
        ProgressManager.Instance.LoadScene(E_SceneName.TitleScene);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }
}