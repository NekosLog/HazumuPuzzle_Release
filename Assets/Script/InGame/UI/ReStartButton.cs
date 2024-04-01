/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ReStartButton : MonoBehaviour 
{
    [SerializeField, Tooltip("実行可能時の色")]
    Color _canExecuteColor = default;

    [SerializeField, Tooltip("実行不能時の色")]
    Color _notExecuteColor = default;

    // 自分のレンダラー
    private Image _myImage = default;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (StateManager.nowGameStateProperty.Value == E_GameState.InGame_Ready || StateManager.nowGameStateProperty.Value == E_GameState.InGame_Droping)
        {
            _myImage.color = _canExecuteColor;
        }
        else
        {
            _myImage.color = _notExecuteColor;
        }
    }
}