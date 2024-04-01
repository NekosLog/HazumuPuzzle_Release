/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class DropController : MonoBehaviour, IResetDropPosition
{
    // 移動ベクトル
    private Vector3 _moveVector = default;

    // 移動ベクトルの基準値　最初の法線
    private Vector3 _moveDirection = default;

    // 掛かった重力量の合計
    private Vector3 _gravityValue = default;

    // 重力　下方向への加速度
    private float _gravity = -9f;

    // 移動速度の上限
    private const float TERMINAL_SPEED = 7f;

    // 移動距離を記録する変数
    private float _movedLength = default;

    // 移動履歴の管理クラス
    private DropMoveHistory _history = default;

    private void Awake()
    {
        // 移動履歴の管理クラスをインスタンス
        _history = new DropMoveHistory(transform.position);
    }

    private void Update()
    {

        // 移動した分移動距離を加算する
        _movedLength += _moveVector.magnitude * TimeManager._deltaTime;

        // 重力量を加算する
        _gravityValue += Vector3.up * _gravity * TimeManager._deltaTime;

        // 基準値と重力量から移動ベクトルを合成する
        _moveVector = _moveDirection + _gravityValue;

        // 前の反射地点まで戻った場合
        if (_movedLength < 0)
        {
            // 初期地点まで戻った場合
            if (_history._getNextIndex == DropMoveHistory.START_INDEX)
            {
                // 時間を停止
                TimeManager.StopTime();

                // 移動履歴を削除
                _history.ClearHistory();

                Debug.Log("初期位置に着いた");
            }

            // 前の反射情報がある場合
            else
            {
                Debug.Log("前の反射を行う");

                // 前の反射時の移動情報を取得する
                DropMoveLog beforeLog = _history.GetBeforeLog();

                Debug.Log($"読み込んだ反射データ→入射：{beforeLog.inDirection} , 接触位置：{beforeLog.reflectPosition} , 移動距離：{beforeLog.movedLength}");

                // 移動情報を代入する
                SetMoveLog(beforeLog);
            }
        }

        // 限界速度を超えていないか
        if (_moveVector.magnitude > TERMINAL_SPEED)
        {
            // 移動ベクトルの大きさを上限にする
            _moveVector = _moveVector.normalized * TERMINAL_SPEED;
        }

        // 移動ベクトルに向かって移動する
        transform.position += _moveVector * TimeManager._deltaTime;
    }

    /// <summary>
    /// ドロップの移動方向を反射した方向に変えるメソッド
    /// </summary>
    /// <param name="hitNormal">接触部の法線</param>
    public void Reflect(Vector3 hitNormal)
    {
        // 次の移動情報が既にあるかどうか
        if (_history._isSetNextIndex)
        {
            // 入射角と法線の内積
            float innerProduct = Vector3.Dot(_moveVector, hitNormal);

            // 内積の正負を判定　負数の場合は反射を行わない
            if (innerProduct < 0)
            {
                // 次の移動情報を設定
                SetMoveLog(_history.GetNextLog());
                // 反射ベクトルを算出して返す
                _moveDirection = _moveVector - 2 * innerProduct * hitNormal;
            }
        }
        else
        {
            // 反射までの軌道を記録
            _history.AddHistory(_moveDirection, _gravityValue, transform.position, _movedLength);

            // 反射後のベクトル基準値を算出
            _moveDirection = Reflection.GetReflectVector(_moveVector, hitNormal);
        }

        // 反射後のベクトルを代入
        _moveVector = _moveDirection;

        // 重力量をリセット
        _gravityValue = Vector3.zero;

        // 移動距離をリセットする
        _movedLength = 0f;
    }

    /// <summary>
    /// ドロップのめり込みを解消するメソッド
    /// </summary>
    /// <param name="embeddingValue">めり込んでいる距離</param>
    //public void UnEmbedding(float embeddingValue)
    //{
    //    // めり込んでいる分戻る
    //    transform.position += -_moveVector * embeddingValue;
    //}

    public void ResetDropPosition()
    {
        // 初期位置を取得
        DropMoveLog firstLog = _history.ClearHistory();

        // 移動情報を代入する
        SetMoveLog(firstLog);
    }

    private void SetMoveLog(DropMoveLog moveLog)
    {
        // 移動情報を代入する
        _moveDirection = moveLog.inDirection;
        _gravityValue = moveLog.gravityValue;
        transform.position = moveLog.reflectPosition;
        _movedLength = moveLog.movedLength;
        Debug.Log("移動情報を元に移動");
    }
}