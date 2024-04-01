/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;

public class TranslateBlock : MonoBehaviour, IBlockAction
{
    [Header("移動する場所を設定")]
    [SerializeField, Tooltip("最初のパターン番号")]
    private int _startPattern = 1;
    [Header("パターン")]
    [SerializeField]
    private Vector3[] _translatePattern = new Vector3[3];

    private int _nowPattern = default;

    private Vector3 _startPosition = default;

    // 現在のパターン番号が一番大きいかどうか
    public bool _isMaxPattern { get { return _nowPattern >= _translatePattern.Length; } }

    // 現在のパターン番号が一番小さいかどうか
    public bool _isMinPattern { get { return _nowPattern <= 1; } }

    public void StartSetting()
    {
        _nowPattern = _startPattern;
        _startPosition = transform.position;
        BlockAction(0);
    }

    private void Awake()
    {
        StartSetting();
    }

    public void BlockAction(int input)
    {
        int nextPattern = _nowPattern + input;

        if (nextPattern < 1 || nextPattern > _translatePattern.Length)
        {
            return;
        }

        transform.position = _startPosition + _translatePattern[nextPattern-1];

        _nowPattern = nextPattern;
    }
}