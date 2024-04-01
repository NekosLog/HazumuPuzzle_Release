/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class CheckOut : MonoBehaviour 
{
	[SerializeField, Tooltip("場外判定の高さ")]
	private float _outLineHeight = default;

    [SerializeField, Tooltip("ステージ情報")]
    private StageDataManager _stageDataManager = default;

	private void DropOut()
    {
        TimeManager.StopTime();
        transform.position = _stageDataManager.nowStageData.dropStartPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.AddComponent<TestStart>();
    }

    private void Update()
    {
        if (transform.position.y <= _outLineHeight)
        {
            DropOut();
        }
    }
}