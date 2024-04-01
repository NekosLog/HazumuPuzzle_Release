/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;

public class RotateBlock : MonoBehaviour, IBlockAction
{
    [Header("回転する値を設定")]
    [SerializeField, Tooltip("最初のパターン番号")]
    private int _startPattern = 1;
    [Header("パターン")]
    [SerializeField]
    private float[] _rotatePattern = new float[3];

    // 現在のパターン番号が一番大きいかどうか
    public bool _isMaxPattern { get { return _nowPattern >= _rotatePattern.Length; } }

    // 現在のパターン番号が一番小さいかどうか
    public bool _isMinPattern { get { return _nowPattern <= 1; } }


    private int _nowPattern = default;

    private Vector3 _startRotate = default;

    public void StartSetting()
    {
        _nowPattern = _startPattern;
        _startRotate = transform.rotation.eulerAngles;
        BlockAction(0);
    }

    private void Awake()
    {
        StartSetting();
    }

    public void BlockAction(int input)
    {
        int nextPattern = _nowPattern + input;

        if (nextPattern < 1 || nextPattern > _rotatePattern.Length)
        {
            return;
        }

        Vector3 nextRotation = _startRotate;
        nextRotation.z += _rotatePattern[nextPattern-1];

        transform.rotation = Quaternion.Euler(nextRotation);

        _nowPattern = nextPattern;
    }


}