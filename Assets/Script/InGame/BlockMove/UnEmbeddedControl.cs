/* 制作日 2024/03/01
*　製作者 ニシガキ
*　最終更新日 2024/03/01
*/

using UnityEngine;
using System.Collections;
 
/// <summary>
/// めり込んでいる距離を求めるためのクラス
/// </summary>
public static class UnEmbeddedControl 
{
    /// <summary>
    /// めり込んでいる距離を求めるメソッド
    /// </summary>
    /// <param name="dropData">接触しているドロップのデータ</param>
    /// <param name="hitCollider">当たっているコライダー</param>
    /// <returns>めり込んでいる距離</returns>
    public static float UnEmbedding(DropData dropData, Collider hitCollider)
    {
        // ドロップの中心点と最も近いコライダー上の座標（接触点の座標）
        Vector3 closestPosition = hitCollider.ClosestPoint(dropData._originPosition);

        // 接触点の座標との距離
        float distance = Vector3.Distance(closestPosition, dropData._originPosition);

        // ドロップの半径と接触点の座標との距離の差（めり込んでいる距離）
        float enbeddingValue = dropData._radius - distance;

        // 求めた距離を返す
        return enbeddingValue;
    }
}