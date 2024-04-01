/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;

public class ExtendBlock : MonoBehaviour, IBlockAction
{
    [Header("伸縮する値を設定")]
    [SerializeField, Tooltip("最初のパターン番号")]
    private int _startPattern = 1;
    [Header("パターン")]
    [SerializeField]
    private float[] _extendPattern = new float[3];

    private int _nowPattern = default;

    private Vector3 _startScale = default;
    private Vector3 _startPosition = default;

    // 現在のパターン番号が一番大きいかどうか
    public bool _isMaxPattern { get { return _nowPattern >= _extendPattern.Length; } }

    // 現在のパターン番号が一番小さいかどうか
    public bool _isMinPattern { get { return _nowPattern <= 1; } }

    public void StartSetting()
    {
        _nowPattern = _startPattern;
        _startScale = transform.localScale;
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

        if (nextPattern < 1 || nextPattern > _extendPattern.Length)
        {
            return;
        }

        Vector3 nextScale = _startScale;
        nextScale.x = _startScale.x + (_startScale.x * Mathf.Abs(_extendPattern[nextPattern-1]));
        Vector3 extendedValue = transform.right * (_startScale.x * _extendPattern[nextPattern-1] / 2);
        transform.position = _startPosition + extendedValue;
        transform.localScale = nextScale;

        _nowPattern = nextPattern;
    }
}