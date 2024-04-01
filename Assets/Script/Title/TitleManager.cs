/* 制作日 2024/01/13
*　製作者 ニシガキ
*　最終更新日 2024/02/09
*/

using System;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ProgressManager
    private ProgressManager _progressManager = default;

    [SerializeField, Tooltip("タイトルのキャンバス")]
    private GameObject _titleCanvas = default;

    [SerializeField, Tooltip("レベルセレクトのキャンバス")]
    private GameObject _levelSelectCanvas = default;



    /// <summary>
    /// 初期化
    /// </summary>
    private void Awake()
    {
        // ProgressManagerを取得
        _progressManager = ProgressManager.Instance;

        // ゲームステータスを購読してUIを管理
        StateManager.nowGameStateProperty.Subscribe(state => { ChengeUI(state); }).AddTo(this);

        // 最初のキャンバス設定
        ChengeUI(StateManager.nowGameStateProperty.Value);
    }

    /// <summary>
    /// 「はじめる」を押したときの処理
    /// </summary>
    public void OnStart()
    {
        // ゲームステータスをレベルセレクトに変更
        StateManager.SetGameState(E_GameState.LevelSelect);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「おわる」を押したときの処理
    /// </summary>
    public void OnExit()
    {
        // ゲームを終了
        Application.Quit();
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「レベル１」を押したときの処理
    /// </summary>
    public void OnLevel1()
    {
        // ゲームステータスを準備中に変更してレベル１へ移動
        StateManager.SetGameState(E_GameState.InGame_Ready);
        _progressManager.LoadScene(E_SceneName.Level1Scene);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「レベル２」を押したときの処理
    /// </summary>
    public void OnLevel2()
    {
        // ゲームステータスを準備中に変更してレベル２へ移動
        StateManager.SetGameState(E_GameState.InGame_Ready);
        _progressManager.LoadScene(E_SceneName.Level2Scene);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「レベル３」を押したときの処理
    /// </summary>
    public void OnLevel3()
    {
        // ゲームステータスを準備中に変更してレベル３へ移動
        StateManager.SetGameState(E_GameState.InGame_Ready);
        _progressManager.LoadScene(E_SceneName.Level3Scene);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「レベル４」を押したときの処理
    /// </summary>
    public void OnLevel4()
    {
        // ゲームステータスを準備中に変更してレベル４へ移動
        StateManager.SetGameState(E_GameState.InGame_Ready);
        _progressManager.LoadScene(E_SceneName.Level4Scene);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 「もどる」を押したときの処理
    /// </summary>
    public void OnBack()
    {
        // ゲームステータスをタイトルに変更
        StateManager.SetGameState(E_GameState.Title);
        ProgressManager.Instance._audio.PlayOneShot(ProgressManager.Instance._ButtonSE);
    }

    /// <summary>
    /// 表示するUIを管理するクラス
    /// </summary>
    /// <param name="state">ゲームステータス</param>
    private void ChengeUI(E_GameState state)
    {
        switch (state)
        {
            case E_GameState.Title:
                _titleCanvas.SetActive(true);
                _levelSelectCanvas.SetActive(false);
                break;


            case E_GameState.LevelSelect:
                _titleCanvas.SetActive(false);
                _levelSelectCanvas.SetActive(true);
                break;
        }
    }
}