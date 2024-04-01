/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 移動情報をまとめた構造体
/// </summary>
public struct DropMoveLog
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="inDirection">入射角</param>
    /// <param name="reflectPosition">反射時の位置</param>
    /// <param name="movedLength">移動してきた距離</param>
    public DropMoveLog(Vector3 inDirection, Vector3 gravityValue, Vector3 reflectPosition, float movedLength)
    {
        // 初期値代入
        this.inDirection = inDirection;
        this.gravityValue = gravityValue;
        this.reflectPosition = reflectPosition;
        this.movedLength = movedLength;
    }

    // 入射ベクトルの基準値
    public Vector3 inDirection;

    // 入社ベクトルの重力量
    public Vector3 gravityValue;

    // 反射時の位置
    public Vector3 reflectPosition;

    // 移動してきた距離
    public float movedLength;
}
 
/// <summary>
/// 移動履歴を管理するクラス
/// </summary>
public class DropMoveHistory 
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DropMoveHistory(Vector3 firstPosition)
    {
        // リストの初期化
        _dropMoveLogs = new List<DropMoveLog>();

        // リストの初期値設定
        _dropMoveLogs.Add(new DropMoveLog(Vector3.zero, Vector3.zero, firstPosition, 0f));

        // インデックスの初期設定
        _nextIndex = 0;
    }

    // 移動履歴のリスト
    public List<DropMoveLog> _dropMoveLogs = new List<DropMoveLog>();

    // 移動開始のインデックス
    public const int START_INDEX = 0;

    // 次に使用する移動履歴のインデックス
    public int _nextIndex = default;

    /// <summary>
    /// 次に使用するインデックスがリストの最後の要素かどうかを渡すプロパティ
    /// </summary>
    public bool _isSetNextIndex { get { return _nextIndex < (_dropMoveLogs.Count - 1); } }

    /// <summary>
    /// 次に使用する移動情報のインデックスを渡すプロパティ
    /// </summary>
    public int _getNextIndex { get { return _nextIndex; } }

    /// <summary>
    /// 移動情報をリストに追加するメソッド
    /// </summary>
    /// <param name="inDirection">入射角</param>
    /// <param name="reflectPosition">反射時の位置</param>
    /// <param name="movedLength">移動してきた距離</param>
    public void AddHistory(Vector3 inDirection, Vector3 gravityValue, Vector3 reflectPosition, float movedLength)
    {
        // 移動情報を作成
        DropMoveLog addLog = new DropMoveLog(inDirection, gravityValue, reflectPosition, movedLength);

        // 移動情報をリストに追加
        _dropMoveLogs.Add(addLog);

        // インデックスを加算
        _nextIndex += 1;
    }

    public DropMoveLog GetBeforeLog()
    {
        // 前の移動情報を格納
        DropMoveLog beforeLog = _dropMoveLogs[_nextIndex];

        // インデックスを一つ戻す
        _nextIndex -= 1;

        // 移動情報を返す
        return beforeLog;
    }

    public DropMoveLog GetNextLog()
    {
        // インデックスを加算
        _nextIndex += 1;

        // 移動情報を渡す
        return _dropMoveLogs[_nextIndex];
    }

    public DropMoveLog ClearHistory()
    {
        // 初期位置を退避
        DropMoveLog returnLog = _dropMoveLogs[START_INDEX];

        // リストの初期化
        _dropMoveLogs = new List<DropMoveLog>();

        // リストの初期位置設定
        _dropMoveLogs.Add(returnLog);

        // インデックスの初期化
        _nextIndex = 0;

        // 初期位置を返す
        return returnLog;
    }
}