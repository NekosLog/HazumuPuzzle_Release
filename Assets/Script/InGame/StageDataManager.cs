/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using System.Collections;
 
public class StageDataManager : MonoBehaviour 
{
    [SerializeField, Tooltip("ステージデータ")]
    private StageDataObject _stageDatas = default;

	private StageData _nowStageData = default;

	public StageData nowStageData
    {
		get { return _nowStageData; }
    }

    private void Awake()
    {
        SetStageData(1);
        return;
    }

    public void SetStageData(int stageNumber)
    {
        foreach(StageData stageData in _stageDatas.StageDataList)
        {
            if(stageData.stageNumber == stageNumber)
            {
                _nowStageData = stageData;
            }
        }
    }
}