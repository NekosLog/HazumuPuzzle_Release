/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
/// <summary>
/// 反射後のベクトルを求めるクラス
/// </summary>
public static class Reflection 
{
    /// <summary>
    /// 移動ベクトルと接触点の法線から反射ベクトルを求めるメソッド
    /// </summary>
    /// <param name="inDirection">入射角　ドロップの移動ベクトル</param>
    /// <param name="inNormal">接触点の法線</param>
    /// <returns>反射ベクトル</returns>
	public static Vector3 GetReflectVector(Vector3 inDirection, Vector3 inNormal)
    {
        // 入射角と法線の内積
        float innerProduct = Vector3.Dot(inDirection,inNormal);

        // 内積の正負を判定　負数の場合は反射を行わない
        if (innerProduct < 0)
        {
            // 反射ベクトルを算出して返す
            return inDirection - 2 * innerProduct * inNormal;
        }
        else
        {
            // 反射を行わずに返す
            return inDirection;
        }
    }
}