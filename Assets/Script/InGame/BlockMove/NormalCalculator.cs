/* 制作日 2024/02/26
*　製作者 ニシガキ
*　最終更新日 2024/03/04
*/

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 反射法線を計算するためのクラス
/// </summary>
public static class NormalCalculator 
{
    /// <summary>
    /// 反射法線を計算して返すメソッド
    /// </summary>
    /// <param name="dropData">接触したドロップのデータ</param>
    /// <param name="blockData">接触しているブロックのデータ</param>
    /// <returns></returns>
    public static Vector3 GetNormal(DropData dropData, BlockData blockData)
    {
        /*
         [最も近い頂点] から [ドロップの中心点] を結んだ法線と、
         [最も近い頂点を含む３面の法線] との角度が９０度を超えていない法線の本数に応じて、
        接触部がブロックのどこなのかを判定して法線を返す

        １．面　…１本の場合は面に接触している
                  接触している面の法線を返す

        ２．辺　…２本の場合は辺に接触している
                　接触している辺の最近接点からドロップの中心点を結んだ線分を返す

        ３．頂点…３本の場合は頂点に接触している
                  接触している頂点からドロップの中心点を結んだ線分を返す
         */


        // ブロックデータから頂点座標を持つ変数
        Vector3[] vertexPositions = blockData.GetVertexPositions();

        // 各頂点との距離を格納する変数
        float[] distances = new float[BlockData.VERTEX_COUNT];

        // 最も近い頂点の番号を格納する変数　最初は仮に０を代入
        int nearest = 0;

        // 各頂点との距離を測り、最も近い頂点の番号を取得
        for (int i = 0; i < BlockData.VERTEX_COUNT; i++)
        {
            // 距離を代入
            distances[i] = Vector3.Distance(dropData._originPosition, vertexPositions[i]);

            // 最も近かった場合は頂点の番号を登録
            if (distances[i] < distances[nearest])
            {
                nearest = i;
            }
        }

        // 最も近い頂点を含む３面を取得
        E_NormalDirection[] directionList = blockData.GetAdjacent(nearest);

        // 最も近い頂点からドロップの中心点を結んだベクトル
        Vector3 nearVertexVector = dropData._originPosition - vertexPositions[nearest];

        // 条件を満たした法線を格納するリスト
        List<E_NormalDirection> clearDirectionList = new List<E_NormalDirection>();

        // ３面の法線それぞれで判定を行う
        foreach (E_NormalDirection normalDirection in directionList)
        {
            // 面の法線と最も近い頂点からドロップの中心点を結んだベクトルとの角度が９０度を超えていないか
            if (Vector3.Angle(nearVertexVector, blockData.GetNormal(normalDirection)) <= 90f)
            {
                // 条件を満たした法線をリストに追加
                clearDirectionList.Add(normalDirection);
            }
        }

        // 反射法線を格納する変数
        Vector3 normal = default;

        // 条件を満たした法線の数から接触点を判別
        switch (clearDirectionList.Count)
        {
            // 面
            case 1:
                // 面の法線を代入
                normal = blockData.GetNormal(clearDirectionList[0]);
                break;

            // 辺
            case 2:
                // 辺のベクトル
                Vector3 edgeVector = blockData.GetEdgeVector(clearDirectionList, nearest);

                // 辺のベクトルと接している球の中心点とのベクトル
                float edgeDot = Vector3.Dot(nearVertexVector, edgeVector.normalized);

                // 始点の頂点と接点の距離
                float hitPositionDistance = edgeDot / edgeVector.magnitude;

                // 始点から辺のベクトルに向かって距離分離れた位置を取る
                Vector3 hitEdgePosition = vertexPositions[nearest] + edgeVector * hitPositionDistance;

                // 接点と中心点から法線ベクトルを生成して代入
                normal = (dropData._originPosition - hitEdgePosition).normalized;
                break;

            // 頂点
            case 3:
                // 頂点から中心点へのベクトルを代入
                normal = nearVertexVector;
                break;
        }

        // 求めた法線を返す
        return normal;
    }
}