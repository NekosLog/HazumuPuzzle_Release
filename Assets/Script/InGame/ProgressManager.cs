/* 制作日
*　製作者
*　最終更新日
*/

using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
public class ProgressManager : MonoBehaviour 
{
#if UNITY_EDITOR
	[SerializeField, Tooltip("開始するシーン名")]
	private E_SceneName _sceneName = default;

    [SerializeField, Tooltip("開始する時のゲームステータス")]
    private E_GameState _state = default;

#endif
    [SerializeField, Tooltip("AudioSource")]
    public AudioSource _audio = default;

    [SerializeField, Tooltip("タイトルBGM")]
    private AudioClip _TitleBGM = default;

    [SerializeField, Tooltip("インゲームBGM")]
    private AudioClip _InGameBGM = default;

    [SerializeField, Tooltip("ボタンSE")]
    public AudioClip _ButtonSE = default;

    [SerializeField, Tooltip("ゴールSE")]
    public AudioClip _GoalSE = default;

    private enum E_NowBGM
    {
        Title,
        InGame
    }

    private E_NowBGM _nowBGM = default;

    // 現在再生中のシーン
    private Scene _nowScene = default;

    // シングルトン用の自分
    private static ProgressManager _myInstance = default;

    private void Awake()
    {
        // シングルトンの実装
        if (_myInstance == null)
        {
            // 自分を代入して保持する
            _myInstance = this;

            StateManager.nowGameStateProperty.Subscribe(state => { BGMSet(state); }).AddTo(this);
        }
        else
        {
            // すでに保持しているからリターン
            Debug.LogError("ProgressManagerが重複した");
            return;
        }


#if UNITY_EDITOR
        // デバッグ用　指定したシーンから始めれるように
        LoadScene(_sceneName);

        // デバッグ用　指定したゲームステータスから始められるように
        StateManager.SetGameState(_state);
#else
        // タイトルシーンをロード
        LoadScene(E_SceneName.TitleScene);

        // ゲームステータスをタイトルに設定
        StateManager.SetGameState(E_GameState.Title);
#endif
    }

    /// <summary>
    /// 新しいシーンをロードするメソッド
    /// </summary>
    /// <param name="sceneName">ロードしたいシーン</param>
    public void LoadScene(E_SceneName sceneName)
    {
        // 新しいシーンを生成
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Additive);

        // 前のシーンが存在する場合
        if (_nowScene.name != null)
        {
            // 前のシーンのルートを取得
            GameObject oldSceneRootObject = _nowScene.GetRootGameObjects()[0];

            // 前のルートを非表示　シーンのアンロードに描画しないようにする
            oldSceneRootObject.SetActive(false);

            // 前のシーンをアンロード
            SceneManager.UnloadSceneAsync(_nowScene,UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
        
        // 新しいシーンを登録
        _nowScene = SceneManager.GetSceneByName(sceneName.ToString());
        print(_nowScene.name);
    }

    /// <summary>
    /// ProgressManagerを取得するためのプロパティ
    /// </summary>
    public static ProgressManager Instance
    {
        // ProgressManagerを返す
        get { return _myInstance; }
    }

    public void BGMSet(E_GameState state)
    {
        switch (state)
        {
            case E_GameState.Title:
                if (_nowBGM != E_NowBGM.Title)
                {
                    _nowBGM = E_NowBGM.Title;
                    _audio.Stop();
                    _audio.clip = _TitleBGM ;
                    _audio.Play();
                }
                break;

            case E_GameState.LevelSelect:
                if (_nowBGM != E_NowBGM.Title)
                {
                    _nowBGM = E_NowBGM.Title;
                    _audio.Stop();
                    _audio.clip = _TitleBGM;
                    _audio.Play();
                }
                break;

            case E_GameState.InGame_Ready:
                if (_nowBGM != E_NowBGM.InGame)
                {
                    _nowBGM = E_NowBGM.InGame;
                    _audio.Stop();
                    _audio.clip = _InGameBGM;
                    _audio.Play();
                }
                break;

            case E_GameState.InGame_Droping:
                if (_nowBGM != E_NowBGM.InGame)
                {
                    _nowBGM = E_NowBGM.InGame;
                    _audio.Stop();
                    _audio.clip = _InGameBGM;
                    _audio.Play();
                }
                break;

            case E_GameState.InGame_Clear:
                if (_nowBGM != E_NowBGM.InGame)
                {
                    _nowBGM = E_NowBGM.InGame;
                    _audio.Stop();
                    _audio.clip = _InGameBGM;
                    _audio.Play();
                }
                break;
        }
    }

    

}