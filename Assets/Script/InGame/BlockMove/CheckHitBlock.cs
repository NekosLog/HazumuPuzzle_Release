/* 制作日 2024/02/26
*　製作者 ニシガキ
*　最終更新日 2024/03/04
*/

using UnityEngine;
using System.Collections;
 
/// <summary>
/// 接触を感知して反射ベクトルの計算を実行するクラス
/// </summary>
public class CheckHitBlock : MonoBehaviour
{
    [SerializeField, Tooltip("判定の半径　ドロップの半径と同じにする")]
    private float _radius = 0.5f;

    // ブロックのレイヤー
    private LayerMask _blockLayer = 1 << 6;

    // 判定に使うドロップのデータ
    private DropData _myDropData = default;

    // 自分が持つDropController
    private DropController _dropController = default;

    private void Awake()
    {
        // ドロップデータの初期設定　半径を設定する
        _myDropData = new DropData(Vector3.zero, _radius);

        // 自身が持つDropControllerを代入
        _dropController = gameObject.GetComponent<DropController>();
    }

    private void Update()
    {
        // 逆再生時以外
        if (TimeManager.getTimeScale >= 0)
        {
            // 接触したコライダーを代入する変数
            Collider[] hitBlock = new Collider[1];

            // ブロックと接触していないかを判定
            Physics.OverlapSphereNonAlloc(transform.position, _radius, hitBlock, _blockLayer);

            // 接触いないかどうか
            if (hitBlock[0] == null)
            {
                // 接触していない時はreturn
                return;
            }

            // ドロップの中心点を設定
            _myDropData._originPosition = transform.position;

            // 接触部にめり込んでいる距離を取得
            float enbeddingValue = UnEmbeddedControl.UnEmbedding(_myDropData, hitBlock[0]);

            // めり込みを解消させる
            //_dropController.UnEmbedding(enbeddingValue);

            // 接触部の法線ベクトルを取得
            Vector3 normalVector = NormalCalculator.GetNormal(_myDropData, hitBlock[0].gameObject.GetComponent<BlockData>());

            // 値を渡して反射処理を実行
            _dropController.Reflect(normalVector);
        }
    }
}